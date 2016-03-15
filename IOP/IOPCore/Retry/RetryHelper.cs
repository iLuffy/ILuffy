namespace ILuffy.IOP.Retry
{
    using System;
    using System.Threading;
    public static class RetryHelper
    {

        public static void Retry(Action action, IRetryPolicy policy)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            if (policy == null)
            {
                throw new ArgumentNullException("policy");
            }

            var retryContext = new RetryContext();
            while (true)
            {
                try
                {
                    retryContext.CurrentRetryCount++;
                    action();
                    break;
                }
                catch (Exception ex)
                {
                    retryContext.LastException = ex;
                    if (policy.ShouldRetry(retryContext))
                    {
                        Thread.Sleep(retryContext.RetryInterval);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public static TResult Retry<TResult>(Func<TResult> action, IRetryPolicy policy)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            if (policy == null)
            {
                throw new ArgumentNullException("policy");
            }

            var retryContext = new RetryContext();
            while (true)
            {
                try
                {
                    retryContext.CurrentRetryCount++;
                    return action();
                }
                catch (Exception ex)
                {
                    retryContext.LastException = ex;
                    if (policy.ShouldRetry(retryContext))
                    {
                        Thread.Sleep(retryContext.RetryInterval);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

    }
}
