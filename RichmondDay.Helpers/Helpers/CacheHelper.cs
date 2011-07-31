using System;
using System.Web.Caching;

namespace RichmondDay.Helpers {
    /// <summary>
    /// Generic class to manage adding, removing and retrieving objects from cache.
    /// </summary>
    public class CacheHelper {
        private Cache _cache;

        public CacheHelper(Cache cache) {
            _cache = cache;
        }

        /// <summary>
        /// Adds the value to the cache and set's the expiry to the value passed in 
        /// the expiration argument.
        /// </summary>
        /// <param name="keyName">The key used to identify this value in the cache</param>
        /// <param name="value">The object to store in cache</param>
        /// <param name="expiration">How long until the cache expires</param>
        public void Add(string keyName, object value, DateTime expiration) {
            if (expiration == null)
                throw new ArgumentException("Experation argument cannot be null");

            if (_cache.Get(keyName) != null)
                _cache.Remove(keyName);

            _cache.Add(keyName, value, null, expiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }


        /// <summary>
        /// Adds the value to the cache and set's the expiry to 1 hour
        /// </summary>
        /// <param name="keyName">The key used to identify this value in the cache</param>
        /// <param name="value">The object to store in cache</param>
        public void Add(string keyName, object value) {
            if (_cache.Get(keyName) != null)
                _cache.Remove(keyName);

            _cache.Add(keyName, value, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// Returns the value in the cache associated with the keyName argument.
        /// </summary>
        /// <param name="keyName">The cache key to find the value for</param>
        /// <returns>The value in the cache, or null if not found.</returns>
        public object Get(string keyName) {
            try {
                return _cache.Get(keyName);
            } catch { return null; }
        }

        /// <summary>
        /// Removes the cache entry for the keyName argument.
        /// </summary>
        /// <param name="keyName">The cache key to remove from the cache.</param>
        /// <returns>True if removed, false if not.</returns>
        public bool Remove(string keyName) {
            try {
                _cache.Remove(keyName);
                return true;
            } catch { return false; }
        }
    }
}
