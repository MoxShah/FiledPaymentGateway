namespace Filed.PaymentGateway.BusinessModel
{
    /// <summary>
    /// Payment state model
    /// </summary>
    public class PaymentStateModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public int PaymentId { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public int State { get; set; }
        /// <summary>
        /// Gets or sets the payment information.
        /// </summary>
        /// <value>
        /// The payment information.
        /// </value>
        public virtual PaymentInformationModel PaymentInformation { get; set; }
    }
}
