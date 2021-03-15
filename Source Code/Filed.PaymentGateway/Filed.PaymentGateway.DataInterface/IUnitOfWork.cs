namespace Filed.PaymentGateway.DataInterface
{
    using System;

    /// <summary>
    /// Unit of work interface
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the payment information.
        /// </summary>
        /// <value>
        /// The payment information.
        /// </value>
        IPaymentInformation paymentInformation { get; }
        /// <summary>
        /// Gets the state of the payment.
        /// </summary>
        /// <value>
        /// The state of the payment.
        /// </value>
        IPaymentState paymentState { get; }
        /// <summary>
        /// Completes this instance.
        /// </summary>
        /// <returns></returns>
        int Complete();
    }
}
