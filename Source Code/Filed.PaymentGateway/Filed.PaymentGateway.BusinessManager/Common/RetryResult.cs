namespace Filed.PaymentGateway.BusinessManager.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Retry Result response
    /// </summary>
    public class RetryResult
    {
        /// <summary>
        /// The failure exceptions
        /// </summary>
        public readonly IEnumerable<Exception> failureExceptions;
        /// <summary>
        /// The failure count
        /// </summary>
        public readonly int failureCount;
        /// <summary>
        /// The action response
        /// </summary>
        public object actionResponse;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool isSuccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryResult"/> class.
        /// </summary>
        /// <param name="failureExceptions">The failure exceptions.</param>
        protected internal RetryResult(
            ICollection<Exception> failureExceptions)
        {
            this.failureExceptions = failureExceptions;
            failureCount = failureExceptions.Count;
            isSuccess = false;
        }
    }
}
