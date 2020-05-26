using System;

namespace Renting.MasterServices.Core.Interfaces
{
    /// <summary>
    /// ICacheService
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Finds the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        T Find<T>(string key, Func<T> func);

        /// <summary>
        /// Finds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object Find(string key);

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Set(string key, object value);
    }
}
