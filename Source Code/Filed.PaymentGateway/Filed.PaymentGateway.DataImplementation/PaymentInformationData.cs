namespace Filed.PaymentGateway.DataImplementation
{
    using Filed.PaymentGateway.Data;
    using Filed.PaymentGateway.DataInterface;

    /// <summary>
    /// Payment information implementation
    /// </summary>
    /// <seealso cref="Filed.PaymentGateway.DataImplementation.GenericRepository{Filed.PaymentGateway.Data.PaymentInformation}" />
    /// <seealso cref="Filed.PaymentGateway.DataInterface.IPaymentInformation" />
    public class PaymentInformationData : GenericRepository<PaymentInformation>, IPaymentInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentInformationData"/> class.
        /// </summary>
        /// <param name="dBContext">The d b context.</param>
        public PaymentInformationData(FiledDBContext dBContext):base(dBContext)
        {

        }
    }
}
