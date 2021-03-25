using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UserMap.Helpers
{
    /// <summary>
    /// Инкапсулирует создание запросов по укзананному URL.
    /// </summary>
    public static class WebHelper
    {
        private static readonly Timer disposeTimer;     //Служит для высвобождения httpClient'a через указанное время.
        private static readonly string UserAgent;
        private static readonly object locker;

        private static HttpClient httpClient;

        static WebHelper()
        {
            locker = new object();

            disposeTimer = new Timer(DisposeHTTPClient, null, Timeout.Infinite, Timeout.Infinite);

            //Нужно для того, чтобы сервера знали, как возвращать ответ на запрос
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:72.0) Chrome/51.0.2704.103 Safari/537.36";

            //Нужно для указания текущего протокола безопасности для возможности создания запроса
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Возвращает тело ответа по запросу на указанный URL с указанными параметрами.
        /// </summary>
        /// <param name="url">URL по которому будет делается запрос.</param>
        /// <param name="keyValues">Параметры запроса в виде &lt;название параметра, значение параметра&gt;</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException">Возникает, когд</exception>
        /// <returns>Тело ответа в виде строки.</returns>
        public static async Task<string> GetFromURL(string url, Dictionary<string, string> keyValues)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException("URL не может быть пустым.");
            if (keyValues == null)
                throw new ArgumentNullException("keyValues");

            if (httpClient == null)
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
                disposeTimer.Change(300000, Timeout.Infinite);
            }

            var querry = CreateQuerry(url, keyValues);
            var result = await httpClient.GetAsync(querry);
            string stringResult = string.Empty;

            System.Diagnostics.Debug.WriteLine(result);

            if (result.IsSuccessStatusCode)
            {
                stringResult = await result.Content.ReadAsStringAsync();
            }

            return stringResult;
        }

        private static Uri CreateQuerry(string url, Dictionary<string, string> keyValues)
        {
            StringBuilder querryBuilder = new StringBuilder(url);

            int startQuerryIndex = url.LastIndexOf('?');
            
            if (startQuerryIndex == -1)
            {
                querryBuilder.Append('?');
            }

            foreach (var keyValuePair in keyValues)
            {
                querryBuilder.Append(keyValuePair.Key);
                querryBuilder.Append('=');
                querryBuilder.Append(keyValuePair.Value);
                querryBuilder.Append('&');
            }

            querryBuilder.Remove(querryBuilder.Length - 1, 1);

            return new Uri(querryBuilder.ToString());
        }

        private static void DisposeHTTPClient(object state)
        {
            Monitor.Enter(locker);
            httpClient.Dispose();
            httpClient = null;
            disposeTimer.Change(Timeout.Infinite, Timeout.Infinite);
            Monitor.Exit(locker);
        }
    }
}
