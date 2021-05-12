using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace Microsoft.BingAds
{
    public static class HttpClientCustomFactory
    {
        private static Func<HttpClient> _httpClientFactory = () => new HttpClient();
        private static Func<HttpMessageHandler> _httpMessageHandlerFactory;
        internal static Lazy<HttpClient> Client => new Lazy<HttpClient>(CreateHttpClientWithInfiniteTimeout);

        public static void ApplyEfficientHttpClientEndpointBehavior(KeyedCollection<Type, IEndpointBehavior> endpointBehaviors)
        {
            if (_httpMessageHandlerFactory != null)
                endpointBehaviors.Add(new EfficientHttpClientEndpointBehavior(_httpMessageHandlerFactory));
        }

        public static void ReplaceHttpClientFactory(Func<HttpClient> httpClientFactory, Func<HttpMessageHandler> httpMessageHandlerFactory = null)
        {
            _httpMessageHandlerFactory = httpMessageHandlerFactory;
            _httpClientFactory = httpClientFactory;
        }

        private static HttpClient CreateHttpClientWithInfiniteTimeout()
        {
            var client = _httpClientFactory();
            client.Timeout = Timeout.InfiniteTimeSpan;
            return client;
        }

        private class EfficientHttpClientEndpointBehavior : IEndpointBehavior
        {
            private readonly Func<HttpMessageHandler> _messageHandlerFactory;

            public EfficientHttpClientEndpointBehavior(Func<HttpMessageHandler> messageHandlerFactory)
            {
                _messageHandlerFactory = messageHandlerFactory;
            }

            public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
            {
                Func<HttpClientHandler, HttpMessageHandler> factory = clientHandler => _messageHandlerFactory();
                bindingParameters.Add(factory);
            }

            public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
            {
            }

            public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
            {
            }

            public void Validate(ServiceEndpoint endpoint)
            {
            }
        }
    }
}