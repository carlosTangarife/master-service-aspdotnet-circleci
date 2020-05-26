using log4net;
using Newtonsoft.Json;
using Renting.MasterServices.Core.Interfaces;
using StackExchange.Redis;
using System;

namespace Renting.MasterServices.Core.Services
{
    /// <summary>
    /// CacheService, service responsible for searching or putting data in a distributed cache such as Redis cache de azure
    /// </summary>
    /// <seealso cref="Renting.MasterServices.Core.Interfaces.ICacheService" />
    public class CacheService : ICacheService
    {
        private readonly IDatabase redisCache;
        private readonly ILog logger;
        private readonly int hoursToExpire;

        /// <summary>
        /// CacheService DI
        /// </summary>
        /// <param name="redisCache"></param>
        /// <param name="logger"></param>
        /// <param name="hoursToExpire"></param>
        public CacheService(IConnectionMultiplexer redisCache, ILog logger, int hoursToExpire)
        {
            this.redisCache = redisCache.GetDatabase();
            this.logger = logger;
            this.hoursToExpire = hoursToExpire;
        }


        /// <summary>
        /// Finds the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        public T Find<T>(string key, Func<T> func)
        {
            try
            {
                var valFromKey = redisCache.StringGet(key);
                if (valFromKey.IsNullOrEmpty)
                {
                    var response = func();
                    redisCache.StringSet(key, Serialize(response), new TimeSpan(hoursToExpire, 0, 0));
                    return response;
                }

                return Deserialize<T>(valFromKey);
            }
            catch (Exception ex)
            {
                logger.Warn("Error Buscando elemento en cache", ex);
                return func();
            }
        }

        /// <summary>
        /// Finds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object Find(string key)
        {
            try
            {
                var valFromKey = Deserialize<object>(redisCache.StringGet(key));
                return valFromKey;
            }
            catch (Exception ex)
            {
                logger.Warn("Error Buscando elemento en cache", ex);
                return null;
            }
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set(string key, object value)
        {
            try
            {
                redisCache.StringSet(key, Serialize(value), new TimeSpan(hoursToExpire, 0, 0));
            }
            catch (Exception ex)
            {
                logger.Warn("Error Guardando elemento en cache", ex);
            }
        }


        /// <summary>
        /// Deserializes the specified object string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectString">The object string.</param>
        /// <returns></returns>
        private static T Deserialize<T>(string objectString)
        {
            if (string.IsNullOrWhiteSpace(objectString))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(objectString);
        }

        /// <summary>
        /// Serializes the specified o.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        private static string Serialize<T>(T o)
        {
            if (Equals(o, default(T)))
            {
                return string.Empty;
            }

            return JsonConvert.SerializeObject(o);
        }
    }
}
