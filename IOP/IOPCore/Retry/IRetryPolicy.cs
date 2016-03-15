namespace ILuffy.IOP.Retry
{
    using System;

    public interface IRetryPolicy
    {
        bool ShouldRetry(RetryContext context);
    }
}
