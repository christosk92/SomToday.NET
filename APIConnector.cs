#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SomToday.NET.Api;
using SomToday.NET.Attributes;
using SomToday.NET.Authenticators;
using SomToday.NET.Clients;
using SomToday.NET.Configs;
using SomToday.NET.HttpWrappers;

namespace SomToday.NET
{
    public class ApiConnector : IApiConnector
    {
        private readonly IAuthenticator? _authenticator;
        private readonly IHTTPClient _httpClient;
        private readonly IRetryHandler? _retryHandler;

        public event EventHandler<IResponse>? ResponseReceived;

        public ApiConnector(IAuthenticator authenticator) :
            this(authenticator,
                new NetHttpClient(), 
                null)
        {

        }

        public ApiConnector(
            IAuthenticator? authenticator,
            IHTTPClient httpClient,
            IRetryHandler? retryHandler)
        {
            _authenticator = authenticator;
            _httpClient = httpClient;
            _retryHandler = retryHandler;
            ResponseReceived += (sender, response) =>
            {
                LastResponse = response;
            };

            LeerlingClient = CreateAndRegister<ILeerlingClient>();
        }


        public Task<T> SendApiRequest<T>(Uri uri, 
            HttpMethod method, 
            IDictionary<string, string>? parameters = null, 
            object? body = null,
            IDictionary<string, string>? headers = null)
        {
            throw new NotImplementedException();
        }

        public void SetRequestTimeout(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public IResponse? LastResponse { get; private set; }
        public ILeerlingClient LeerlingClient { get; }

        private T CreateAndRegister<T>()
        {
            var type = typeof(T);
            var baseUrl = new Uri(ResolveBaseUrlFromAttribute(type));

            var c = new System.Net.Http.HttpClient(
                new AuthenticatedHttpClientHandler(_authenticator))
            {
                BaseAddress = baseUrl
            };
            c.DefaultRequestHeaders.Add("Accept", "application/json");
            var createdService = RestService.For<T>(c);
            return createdService;
        }

        private static string ResolveBaseUrlFromAttribute(MemberInfo type)
        {
            var attribute = Attribute.GetCustomAttributes(type);

            if (attribute.FirstOrDefault(x => x is BaseUrlAttribute) is BaseUrlAttribute baseUrlAttribute)
            {
                return baseUrlAttribute.BaseUrl;
            }

            throw new NotImplementedException("No BaseUrl attribute was defined");
        }
    }
}