namespace Filed.PaymentGateway.BusinessManager
{
    using Filed.PaymentGateway.BusinessInterface;
    using Filed.PaymentGateway.BusinessManager.Common;
    using Filed.PaymentGateway.BusinessModel;
    using Filed.PaymentGateway.DataInterface;
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Payment process manager
    /// </summary>
    /// <seealso cref="Filed.PaymentGateway.BusinessInterface.IPaymentProcessManager" />
    public class PaymentProcessManager : IPaymentProcessManager
    {
        /// <summary>
        /// The service provider
        /// </summary>
        IServiceProvider serviceProvider;
        /// <summary>
        /// The unit of work
        /// </summary>
        IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProcessManager"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public PaymentProcessManager(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
        {
            this.serviceProvider = serviceProvider;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Processes the payment.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <returns></returns>
        public PaymentResponse ProcessPayment(PaymentInformationModel paymentInformation)
        {
            PaymentResponse paymentResponse;
            ValidateRequest(paymentInformation);
            if (paymentInformation.Amount <= BusinessConstants.CHEAPPAYMENT_LIMIT)
            {
                var cheapPaymentProcessor = (ICheapPaymentGateway)serviceProvider.GetService(typeof(ICheapPaymentGateway));
                paymentResponse = cheapPaymentProcessor.ProcessCheapPayment(paymentInformation);
            }
            else if (paymentInformation.Amount <= BusinessConstants.EXPENSIVEPAYMENT_LIMIT)
            {
                var expensivePaymentProcessor = (IExpensivePaymentGateway)serviceProvider.GetService(typeof(IExpensivePaymentGateway));
                paymentResponse = expensivePaymentProcessor.ProcessExpensivePayment(paymentInformation);
            }
            else
            {
                var expensivePaymentProcessor = (IPremiumPaymentGateway)serviceProvider.GetService(typeof(IPremiumPaymentGateway));
                var retryResult = Retry.Do(() => expensivePaymentProcessor.ProcessPremiumPayment(paymentInformation), TimeSpan.FromSeconds(BusinessConstants.PREMIUM_RETRY_INTERVAL), BusinessConstants.PREMIUM_RETRY_COUNT);
                if (retryResult.isSuccess)
                {
                    paymentResponse = (PaymentResponse)retryResult.actionResponse;
                }
                else
                {
                    paymentResponse = new PaymentResponse
                    {
                        TransactionId = Guid.NewGuid(),
                        RetryCount = retryResult.failureCount,
                        PaymentDetail = paymentInformation,
                        IsSuccess = false
                    };
                }
            }
            var paymentInfoData = new Data.PaymentInformation()
            {
                Amount = paymentResponse.PaymentDetail.Amount,
                CardHolder = paymentResponse.PaymentDetail.CardHolder,
                CreditCardNumber = paymentResponse.PaymentDetail.CreditCardNumber,
                ExpirationDate = paymentResponse.PaymentDetail.ExpirationDate,
                SecurityCode = paymentResponse.PaymentDetail.SecurityCode,
            };
            unitOfWork.paymentInformation.Add(paymentInfoData);
            unitOfWork.Complete();
            paymentResponse.PaymentDetail.Id = paymentInfoData.Id;
            unitOfWork.paymentState.Add(new Data.PaymentState()
            {
                PaymentId = paymentInfoData.Id,
                State = paymentResponse.IsSuccess ? (int)PaymentState.Processed : (int)PaymentState.Failed
            });
            unitOfWork.Complete();
            return paymentResponse;
        }

        /// <summary>
        /// Validates the request.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <exception cref="InvalidOperationException">
        /// Credit card number is incorrect.
        /// or
        /// Invalid security code.
        /// or
        /// Your card is expired.
        /// or
        /// Amount is invalid.
        /// </exception>
        public void ValidateRequest(PaymentInformationModel paymentInformation)
        {
            var cardCheck = new Regex(@"^(1298|1267|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");
            var cvvCheck = new Regex(@"^\d{3}$");
            if (!cardCheck.Match(paymentInformation.CreditCardNumber).Success)
            {
                throw new InvalidOperationException("Credit card number is incorrect.");
            }
            if (!cvvCheck.Match(paymentInformation.SecurityCode).Success)
            { 
                throw new InvalidOperationException("Invalid security code.");
            }
            if (paymentInformation.ExpirationDate < DateTime.Now)
            {
                throw new InvalidOperationException("Your card is expired.");
            }
            if (paymentInformation.Amount <= 0)
            {
                throw new InvalidOperationException("Amount is invalid.");
            }

        }
    }
}
