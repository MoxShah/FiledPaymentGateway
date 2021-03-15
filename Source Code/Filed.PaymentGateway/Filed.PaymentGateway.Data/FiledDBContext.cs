namespace Filed.PaymentGateway.Data
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// DB Context
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class FiledDBContext : DbContext
    {
        /// <summary>
        /// Gets or sets the payment information.
        /// </summary>
        /// <value>
        /// The payment information.
        /// </value>
        public DbSet<PaymentInformation> PaymentInformation { get; set; }
        /// <summary>
        /// Gets or sets the state of the payment.
        /// </summary>
        /// <value>
        /// The state of the payment.
        /// </value>
        public DbSet<PaymentState> PaymentState { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FiledDBContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public FiledDBContext(DbContextOptions<FiledDBContext> options) : base(options)
        {

        }
    }
}
