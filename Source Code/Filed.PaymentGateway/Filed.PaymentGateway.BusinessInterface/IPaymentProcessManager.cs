namespace Filed.PaymentGateway.BusinessInterface
{
    using Filed.PaymentGateway.BusinessModel;

    /// <summary>
    /// Payment process manager interface
    /// </summary>
    public interface IPaymentProcessManager
    {
        /// <summary>
        /// Processes the payment.
        /// </summary>
        /// <param name="paymentRequest">The payment request.</param>
        /// <returns></returns>
        PaymentResponse ProcessPayment(PaymentInformationModel paymentRequest);
    }
}
