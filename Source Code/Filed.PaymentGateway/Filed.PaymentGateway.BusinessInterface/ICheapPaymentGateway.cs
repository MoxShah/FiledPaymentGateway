namespace Filed.PaymentGateway.BusinessInterface
{
    using Filed.PaymentGateway.BusinessModel;

    /// <summary>
    /// Cheap payment gateway interface
    /// </summary>
    public interface ICheapPaymentGateway
    {
        /// <summary>
        /// Processes the cheap payment.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <returns></returns>
        PaymentResponse ProcessCheapPayment(PaymentInformationModel paymentInformation);
    }
}
