namespace Filed.PaymentGateway.PaymentProcessor
{
    using Filed.PaymentGateway.BusinessInterface;
    using Filed.PaymentGateway.BusinessModel;
    using System;

    /// <summary>
    /// Expensive payment gateway implementation
    /// </summary>
    /// <seealso cref="Filed.PaymentGateway.BusinessInterface.IExpensivePaymentGateway" />
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        /// <summary>
        /// Processes the expensive payment.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <returns></returns>
        public PaymentResponse ProcessExpensivePayment(PaymentInformationModel paymentInformation)
        {
            return new PaymentResponse()
            {
                TransactionId = Guid.NewGuid(),
                IsSuccess = true,
                PaymentDetail = paymentInformation,
                PaymentType = PaymentMode.Expensive,
                RetryCount = 1
            };
        }

    }
}
