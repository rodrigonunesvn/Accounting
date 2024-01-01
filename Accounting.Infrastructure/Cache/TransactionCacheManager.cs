using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Accounting.Infrastructure.Cache
{
    public class TransactionCacheManager : ITransactionCacheManager
    {
        private readonly IDistributedCache _cache;

        public TransactionCacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<decimal> GetOrSetCurrentBalance(Func<Task<decimal>> calculateAndCacheFunc)
        {
            var cachedBalance = await _cache.GetAsync("CurrentBalance");

            if (cachedBalance == null)
            {
                var currentBalance = await calculateAndCacheFunc();

                cachedBalance = Encoding.UTF8.GetBytes(currentBalance.ToString());

                var cacheEntryOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };

                await _cache.SetAsync("CurrentBalance", cachedBalance, cacheEntryOptions);
            }

            return decimal.Parse(Encoding.UTF8.GetString(cachedBalance));
        }

        public void InvalidateCache()
        {
            _cache.Remove("CurrentBalance");
        }
    }
}
