namespace ILuffy.IOP.Retry
{
    using System;

    public class LinearRetry : IRetryPolicy
    {
        protected int retryTimes;
        protected TimeSpan retryInterval;

        public LinearRetry(int retryTimes, TimeSpan retryInterval)
        {
            this.retryTimes = retryTimes;
            this.retryInterval = retryInterval;
        }

        public bool ShouldRetry(RetryContext context)
        {
            context.RetryInterval = retryInterval;

            return context.CurrentRetryCount < retryTimes;
        }
    }
}
