namespace Filed.PaymentGateway.BusinessModel
{
    /// <summary>
    /// Business constatns
    /// </summary>
    public class BusinessConstants
    {
        /// <summary>
        /// The cheappayment limit
        /// </summary>
        public static decimal CHEAPPAYMENT_LIMIT = 20;
        /// <summary>
        /// The expensivepayment limit
        /// </summary>
        public static decimal EXPENSIVEPAYMENT_LIMIT = 500;
        /// <summary>
        /// The premiumpayment limit
        /// </summary>
        public static decimal PREMIUMPAYMENT_LIMIT = 501;
        /// <summary>
        /// The premium retry count
        /// </summary>
        public static int PREMIUM_RETRY_COUNT = 3;
        /// <summary>
        /// The premium retry interval
        /// </summary>
        public static int PREMIUM_RETRY_INTERVAL = 1;
        /// <summary>
        /// The premium failure amount
        /// </summary>
        public static decimal PREMIUM_FAILURE_AMOUNT = 999.99M;
    }

    /// <summary>
    /// Payment modes
    /// </summary>
    public enum PaymentMode
    {
        /// <summary>
        /// The cheap
        /// </summary>
        Cheap = 1,
        /// <summary>
        /// The expensive
        /// </summary>
        Expensive = 2,
        /// <summary>
        /// The premium
        /// </summary>
        Premium = 3
    }

    /// <summary>
    /// Payment States
    /// </summary>
    public enum PaymentState
    {
        /// <summary>
        /// The pending
        /// </summary>
        Pending = 1,
        /// <summary>
        /// The processed
        /// </summary>
        Processed = 2,
        /// <summary>
        /// The failed
        /// </summary>
        Failed = 3
    }
}