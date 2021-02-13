using System;
using System.Collections.Generic;

namespace UserMap.Helpers
{
    /// <summary>
    /// Представляет кэш, в котром можно сохранять общие элементы для подальшего их использования.
    /// </summary>
    internal static class MapCache
    {
        private static readonly IDictionary<string, object> cache = new Dictionary<string, object>();

        /// <summary>
        /// Возвращает ссылку на элемент в памяти.
        /// </summary>
        /// <param name="key">Ключ элемента</param>
        /// <returns>Ссылку на элемент в памяти или <see langword="null"/>, если ключ отсутствует</returns>
        public static object GetItem(string key)
        {
            return cache[key];
        }
        /// <summary>
        /// Возвращает копию элементы, если объект реализует интерфейс <see cref="ICloneable"/>. Иначе возвращает ссылку на элемент
        /// </summary>
        /// <param name="key">Ключ элемента</param>
        /// <returns>Новая копия элементы по ключу или <see langword="null"/>, если ключ отсутствует</returns>
        public static object GetItemClone(string key)
        {
            object item = null;

            item = cache[key];

            if (item is ICloneable cloneable)
            {
                item = cloneable.Clone();
            }

            return item;
        }

        /// <summary>
        /// Проверяет наличие указанного ключа.
        /// </summary>
        /// <param name="key"></param>
        /// <returns><see langword="true"/> если такой ключ существует. Иначе <see langword="false"/></returns>
        public static bool ContainsKey(string key)
        {
            return cache.ContainsKey(key);
        }

        /// <summary>
        /// Добавляет элемент в кэш.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="img"></param>
        public static void Add(string key, object img)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Ключ не может быть пустым.");
            }

            cache[key] = img;

#if DEBUG
            System.Diagnostics.Debug.WriteLine("{0}{0}Chache count: {1}{0}Key: {2}{0}{0}",
                                               Environment.NewLine,
                                               cache.Count.ToString(), key);
#endif
        }

        /// <summary>
        /// Удаляет элемент из кэша.
        /// </summary>
        /// <param name="key">Ключ элемента</param>
        /// <param name="dispose">Указывает, нужно ли высвобождать элемент, если его можно вывободить,
        /// после удаления его из кеша. По-умолочанию высвобождает/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public static bool Remove(string key, bool dispose = true)
        {
            object item = cache[key];

            if (dispose && item is IDisposable disposable)
            {
                disposable.Dispose();
            }

            return cache.Remove(key);
        }
        /// <summary>
        /// Чистит все элементы.
        /// </summary>
        public static void Clear()
        {
            cache.Clear();
        }
    }
}
