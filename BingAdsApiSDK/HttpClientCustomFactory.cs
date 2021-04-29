using System;
using System.Net.Http;
using System.Threading;

namespace Microsoft.BingAds
{
    public static class HttpClientCustomFactory
    {
        private static Func<HttpClient> _httpClientFactory = () => new HttpClient();
        internal static Lazy<HttpClient> Client => new Lazy<HttpClient>(CreateHttpClientWithInfiniteTimeout);

        public static void ReplaceHttpClientFactory(Func<HttpClient> httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private static HttpClient CreateHttpClientWithInfiniteTimeout()
        {
            var client = _httpClientFactory();
            client.Timeout = Timeout.InfiniteTimeSpan;
            return client;
        }
    }
}