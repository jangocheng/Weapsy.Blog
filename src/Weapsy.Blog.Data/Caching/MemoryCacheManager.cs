using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Weapsy.Blog.Data.Caching
{
    public class MemoryCacheManager : ICacheManager
	{
	    private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

	    public T Get<T>(string key)
		{
            return (T)_memoryCache.Get(key);
		}

	    public T GetOrCreate<T>(string key, int cacheTime, Func<T> acquire)
	    {
	        return _memoryCache.GetOrCreate(key, entry =>
	        {
	            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheTime);
                return acquire();
	        });
	    }

	    public async Task<T> GetOrCreateAsync<T>(string key, int cacheTime, Func<Task<T>> acquire)
	    {
	        return await _memoryCache.GetOrCreateAsync(key, async entry =>
	        {
	            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheTime);
                return await acquire();
            });
	    }

	    public void Set(string key, object data, int cacheTime)
		{
			if (data == null)
				return;

            if (IsSet(key))
                return;

            var memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheTime));

            _memoryCache.Set(key, data, memoryCacheEntryOptions);
		}

        public bool IsSet(string key)
        {
            return _memoryCache.Get(key) != null;
        }

        public void Remove(string key)
		{
            _memoryCache.Remove(key);
		}

        public void RemoveByPattern(string pattern)
		{
            //var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //var keysToRemove = new List<string>();

            //foreach (var item in Cache)
            //{
            //	if (regex.IsMatch(item.Key))
            //	{
            //		keysToRemove.Add(item.Key);
            //	}
            //}

            //foreach (string key in keysToRemove)
            //{
            //	Remove(key);
            //}

            throw new NotImplementedException();
        }

		public void Clear()
		{
            //foreach (var item in Cache)
            //{
            //	Remove(item.Key);
            //}

            throw new NotImplementedException();
        }
    }
}