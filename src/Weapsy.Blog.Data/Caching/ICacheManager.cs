using System;
using System.Threading.Tasks;

namespace Weapsy.Blog.Data.Caching
{
    public interface ICacheManager
	{
		T Get<T>(string key);
		T GetOrCreate<T>(string key, int cacheTime, Func<T> acquire);
	    Task<T> GetOrCreateAsync<T>(string key, int cacheTime, Func<Task<T>> acquire);
        void Set(string key, object data, int cacheTime);
		bool IsSet(string key);
		void Remove(string key);
        void RemoveByPattern(string pattern);
		void Clear();
	}
}