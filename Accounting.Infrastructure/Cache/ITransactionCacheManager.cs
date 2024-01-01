namespace Accounting.Infrastructure.Cache
{
    public interface ITransactionCacheManager
    {
        Task<decimal> GetOrSetCurrentBalance(Func<Task<decimal>> calculateAndCacheFunc);
        void InvalidateCache();
    }
}
