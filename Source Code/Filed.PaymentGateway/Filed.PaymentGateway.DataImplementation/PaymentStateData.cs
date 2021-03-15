namespace Filed.PaymentGateway.DataImplementation
{
    using Filed.PaymentGateway.Data;
    using Filed.PaymentGateway.DataInterface;

    /// <summary>
    /// Payment state implementation
    /// </summary>
    /// <seealso cref="Filed.PaymentGateway.DataImplementation.GenericRepository{Filed.PaymentGateway.Data.PaymentState}" />
    /// <seealso cref="Filed.PaymentGateway.DataInterface.IPaymentState" />
    public class PaymentStateData : GenericRepository<PaymentState>, IPaymentState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentStateData"/> class.
        /// </summary>
        /// <param name="dBContext">The d b context.</param>
        public PaymentStateData(FiledDBContext dBContext) : base(dBContext)
        {

        }
    }
}
