namespace Filed.PaymentGateway.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Payment state
    /// </summary>
    public class PaymentState
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public int PaymentId { get; set; }
        /// <summary>
        /// Gets or sets the payment information.
        /// </summary>
        /// <value>
        /// The payment information.
        /// </value>
        [ForeignKey("PaymentId")]
        public virtual PaymentInformation PaymentInformation { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public int State { get; set; }
    }
}
