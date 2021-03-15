namespace Filed.PaymentGateway.BusinessInterface
{
    using Filed.PaymentGateway.BusinessModel;

    /// <summary>
    /// Expensive payment gateway interface
    /// </summary>
    public interface IExpensivePaymentGateway
    {
        /// <summary>
        /// Processes the expensive payment.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <returns></returns>
        PaymentResponse ProcessExpensivePayment(PaymentInformationModel paymentInformation);
    }
}
