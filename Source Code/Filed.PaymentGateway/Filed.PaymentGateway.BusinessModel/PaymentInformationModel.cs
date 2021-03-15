namespace Filed.PaymentGateway.BusinessModel
{
    using System;

    /// <summary>
    /// Payment information model
    /// </summary>
    public class PaymentInformationModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the credit card number.
        /// </summary>
        /// <value>
        /// The credit card number.
        /// </value>
        public string CreditCardNumber { get; set; }
        /// <summary>
        /// Gets or sets the card holder.
        /// </summary>
        /// <value>
        /// The card holder.
        /// </value>
        public string CardHolder { get; set; }
        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>
        /// The expiration date.
        /// </value>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Gets or sets the security code.
        /// </summary>
        /// <value>
        /// The security code.
        /// </value>
        public string SecurityCode { get; set; }
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }
    }
}
