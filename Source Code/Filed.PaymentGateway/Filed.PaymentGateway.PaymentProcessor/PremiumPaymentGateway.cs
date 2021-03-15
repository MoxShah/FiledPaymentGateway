namespace Filed.PaymentGateway.PaymentProcessor
{
    using Filed.PaymentGateway.BusinessInterface;
    using Filed.PaymentGateway.BusinessModel;
    using System;

    /// <summary>
    /// Premium payment gateway implementation
    /// </summary>
    /// <seealso cref="Filed.PaymentGateway.BusinessInterface.IPremiumPaymentGateway" />
    public class PremiumPaymentGateway : IPremiumPaymentGateway
    {
        /// <summary>
        /// Processes the premium payment.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Exception added to showcase retry logic.</exception>
        public PaymentResponse ProcessPremiumPayment(PaymentInformationModel paymentInformation)
        {
            ////Exception added to showcase retry logic.
            if (paymentInformation.Amount == BusinessConstants.PREMIUM_FAILURE_AMOUNT)
            {
                throw new Exception("Exception added to showcase retry logic.");
            }
            return new PaymentResponse()
            {
                TransactionId = Guid.NewGuid(),
                IsSuccess = true,
                PaymentDetail = paymentInformation,
                PaymentType = PaymentMode.Premium,
                RetryCount = 1
            };
        }

    }
}
