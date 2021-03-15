namespace Filed.PaymentGateway.PaymentProcessor
{
    using Filed.PaymentGateway.BusinessInterface;
    using Filed.PaymentGateway.BusinessModel;
    using System;

    /// <summary>
    /// Cheap payment gateway implementation
    /// </summary>
    /// <seealso cref="Filed.PaymentGateway.BusinessInterface.ICheapPaymentGateway" />
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        /// <summary>
        /// Processes the cheap payment.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <returns></returns>
        public PaymentResponse ProcessCheapPayment(PaymentInformationModel paymentInformation)
        {
            return new PaymentResponse()
            {
                TransactionId = Guid.NewGuid(),
                IsSuccess = true,
                PaymentDetail = paymentInformation,
                PaymentType = PaymentMode.Cheap,
                RetryCount = 1
            };
        }
    }
}
