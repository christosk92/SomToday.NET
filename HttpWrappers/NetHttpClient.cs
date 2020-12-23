using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GuardAgainstLib;
using SomToday.NET.Configs;
using SomToday.NET.Extensions;

namespace SomToday.NET.HttpWrappers
{
    public class NetHttpClient : IHTTPClient
    {
        private readonly HttpMessageHandler? _httpMessageHandler;
        private readonly HttpClient _httpClient;

        public NetHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public NetHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public NetHttpClient(IProxyConfig proxyConfig)
        {
            GuardAgainst.ArgumentBeingNull(proxyConfig, nameof(proxyConfig));

            _httpMessageHandler = CreateMessageHandler(proxyConfig);
            _httpClient = new HttpClient(_httpMessageHandler);
        }

        public async Task<IResponse> DoRequest(IRequest request)
        {
            GuardAgainst.ArgumentBeingNull(request, nameof(request));

            using var requestMsg = BuildRequestMessage(request);
            var responseMsg = await _httpClient
                .SendAsync(requestMsg, HttpCompletionOption.ResponseContentRead)
                .ConfigureAwait(false);

            return await BuildResponse(responseMsg).ConfigureAwait(false);
        }

        private static async Task<IResponse> BuildResponse(HttpResponseMessage responseMsg)
        {
            GuardAgainst.ArgumentBeingNull(responseMsg, nameof(responseMsg));

            // We only support text stuff for now
            using var content = responseMsg.Content;
            var headers = responseMsg.Headers.ToDictionary(header => header.Key, header => header.Value.First());
            var body = await responseMsg.Content.ReadAsStringAsync().ConfigureAwait(false);
            var contentType = content.Headers?.ContentType?.MediaType;

            return new Response(headers,
                body,
                responseMsg.StatusCode,
                contentType);
        }

        private static HttpRequestMessage BuildRequestMessage(IRequest request)
        {
            GuardAgainst.ArgumentBeingNull(request, nameof(request));

            var fullUri = new Uri(request.BaseAddress, request.Endpoint).ApplyParameters(request.Parameters);
            var requestMsg = new HttpRequestMessage(request.Method, fullUri);
            foreach (var header in request.Headers)
            {
                requestMsg.Headers.Add(header.Key, header.Value);
            }

            requestMsg.Content = request.Body switch
            {
                HttpContent body => body,
                string body => new StringContent(body, Encoding.UTF8, "application/json"),
                Stream body => new StreamContent(body),
                _ => requestMsg.Content
            };

            return requestMsg;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
                _httpMessageHandler?.Dispose();
            }
        }

        public void SetRequestTimeout(TimeSpan timeout)
        {
            _httpClient.Timeout = timeout;
        }

        private static HttpMessageHandler CreateMessageHandler(IProxyConfig proxyConfig)
        {
            var proxy = new WebProxy
            {
                Address = new UriBuilder(proxyConfig.Host) { Port = proxyConfig.Port }.Uri,
                UseDefaultCredentials = true,
                BypassProxyOnLocal = proxyConfig.BypassProxyOnLocal
            };

            if (!string.IsNullOrEmpty(proxyConfig.User) || !string.IsNullOrEmpty(proxyConfig.Password))
            {
                proxy.UseDefaultCredentials = false;
                proxy.Credentials = new NetworkCredential(proxyConfig.User, proxyConfig.Password);
            }

            var httpClientHandler = new HttpClientHandler
            {
                PreAuthenticate = proxy.UseDefaultCredentials,
                UseDefaultCredentials = proxy.UseDefaultCredentials,
                UseProxy = true,
                Proxy = proxy,
            };
            if (proxyConfig.SkipSSLCheck)
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            }

            return httpClientHandler;
        }
    }
}
