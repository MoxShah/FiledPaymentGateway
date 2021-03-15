namespace Filed.PaymentGateway.BusinessInterface
{
    using Filed.PaymentGateway.BusinessModel;

    /// <summary>
    /// Premium payment gateway interface
    /// </summary>
    public interface IPremiumPaymentGateway
    {
        /// <summary>
        /// Processes the premium payment.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <returns></returns>
        PaymentResponse ProcessPremiumPayment(PaymentInformationModel paymentInformation);
    }
}
