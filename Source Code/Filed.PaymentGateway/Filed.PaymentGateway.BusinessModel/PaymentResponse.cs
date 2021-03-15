namespace Filed.PaymentGateway.BusinessModel
{
    using System;

    /// <summary>
    /// Payment response model
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public Guid TransactionId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        /// <value>
        /// The retry count.
        /// </value>
        public int RetryCount { get; set; }
        /// <summary>
        /// Gets or sets the type of the payment.
        /// </summary>
        /// <value>
        /// The type of the payment.
        /// </value>
        public PaymentMode PaymentType { get; set; }
        /// <summary>
        /// Gets or sets the payment detail.
        /// </summary>
        /// <value>
        /// The payment detail.
        /// </value>
        public PaymentInformationModel PaymentDetail { get; set; }
    }
}
