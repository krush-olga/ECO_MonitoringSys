using System;
using System.Collections.Generic;
using System.Drawing;

namespace Maps.Helpers
{
    public static class ImageCache
    {
        private static IDictionary<string, Bitmap> imagesCache = new Dictionary<string, Bitmap>();

        /// <summary>
        /// Возвращает ссылку на изображение в памяти. При использовании Dispose все использующие текущее  
        /// изображение получат ошибку, так как изображение будет высвобождено.
        /// </summary>
        /// <param name="key">Ключ изображения</param>
        /// <returns>Ссылку на изображение в памяти или <see langword="null"/>, если ключ отсутствует</returns>
        public static Bitmap GetImage(string key)
        {
            if (imagesCache.ContainsKey(key))
            {
                return imagesCache[key];
            }

            return null;
        }
        /// <summary>
        /// Возвращает копию изображения.
        /// </summary>
        /// <param name="key">Ключ изображения</param>
        /// <returns>Новая копия изображения по ключу или <see langword="null"/>, если ключ отсутствует</returns>
        public static Bitmap GetImageClone(string key)
        {
            if (imagesCache.ContainsKey(key))
            {
                return (Bitmap)imagesCache[key].Clone();
            }

            return null;
        }

        public static bool ContainsKey(string key)
        {
            return imagesCache.ContainsKey(key);
        }

        public static void Add(string key, Bitmap img)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Ключ не может быть пустым или отсутствовать.");
            }

            imagesCache[key] = img;

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"{System.Environment.NewLine}{System.Environment.NewLine}" +
                                               $"Images cache count: {imagesCache.Count}{System.Environment.NewLine}Key: {key}" +
                                               $"{System.Environment.NewLine}{System.Environment.NewLine}");
#endif
        }
        /// <summary>
        /// Удаляет элемент из кеша.
        /// </summary>
        /// <param name="key">Ключ изображения</param>
        /// <param name="dispose">Указывает, нужно ли высвобождать изображение после удаления его из кеша. По-умолочанию высвобождает/></param>
        public static void Remove(string key, bool dispose = true)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Ключ не может быть пустым или отсутствовать.");
            }
            
            if (dispose && imagesCache.ContainsKey(key))
            {
                imagesCache[key].Dispose();
            }

            imagesCache.Remove(key);
        }
        /// <summary>
        /// Чистит все изображения с последующим их высвобождением.
        /// </summary>
        public static void Clear()
        {
            MapHelper.DisposeElements(imagesCache.Values);
            imagesCache.Clear();
        }
    }
}
