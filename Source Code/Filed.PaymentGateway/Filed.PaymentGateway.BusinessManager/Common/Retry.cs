namespace Filed.PaymentGateway.BusinessManager.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Retry
    /// </summary>
    public static class Retry
    {
        /// <summary>
        /// Does the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="retryInterval">The retry interval.</param>
        /// <param name="maxAttemptCount">The maximum attempt count.</param>
        public static void Do(Action action, TimeSpan retryInterval, int maxAttemptCount = 3)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, maxAttemptCount);
        }

        /// <summary>
        /// Does the specified action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <param name="retryInterval">The retry interval.</param>
        /// <param name="maxAttemptCount">The maximum attempt count.</param>
        /// <returns></returns>
        public static RetryResult Do<T>(Func<T> action, TimeSpan retryInterval, int maxAttemptCount = 3)
        {
            var exceptions = new List<Exception>();
            for (int attempted = 0; attempted < maxAttemptCount; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }
                    var actionResponse = action();
                    var retryResult = new RetryResult(exceptions);
                    retryResult.actionResponse = actionResponse;
                    retryResult.isSuccess = true;
                    return retryResult;
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            return new RetryResult(exceptions);
        }
    }
}
