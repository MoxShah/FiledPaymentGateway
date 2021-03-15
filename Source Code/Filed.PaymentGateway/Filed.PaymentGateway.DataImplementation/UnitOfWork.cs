namespace Filed.PaymentGateway.DataImplementation
{
    using Filed.PaymentGateway.Data;
    using Filed.PaymentGateway.DataInterface;
    using System;

    /// <summary>
    /// Unit of work implementation
    /// </summary>
    /// <seealso cref="Filed.PaymentGateway.DataInterface.IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The d b context
        /// </summary>
        private readonly FiledDBContext dBContext;
        /// <summary>
        /// Gets the payment information.
        /// </summary>
        /// <value>
        /// The payment information.
        /// </value>
        public IPaymentInformation paymentInformation { get; }
        /// <summary>
        /// Gets the state of the payment.
        /// </summary>
        /// <value>
        /// The state of the payment.
        /// </value>
        public IPaymentState paymentState { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dBContext">The d b context.</param>
        /// <param name="paymentInformation">The payment information.</param>
        /// <param name="paymentState">State of the payment.</param>
        public UnitOfWork(FiledDBContext dBContext, IPaymentInformation paymentInformation, IPaymentState paymentState)
        {
            this.dBContext = dBContext;
            this.paymentInformation = paymentInformation;
            this.paymentState = paymentState;
        }


        /// <summary>
        /// Completes this instance.
        /// </summary>
        /// <returns></returns>
        public int Complete()
        {
            return dBContext.SaveChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dBContext.Dispose();
            }
        }
    }
}
