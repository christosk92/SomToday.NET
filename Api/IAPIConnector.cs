using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SomToday.NET.HttpWrappers;

namespace SomToday.NET.Api
{
    public interface IApiConnector : ISomTodayClient
    {
        // IAuthenticator Authenticator { get; }

        // IJSONSerializer JSONSerializer { get; }

        // IHTTPClient HTTPClient { get; }

        event EventHandler<IResponse>? ResponseReceived;

        Task<T> SendApiRequest<T>(
          Uri uri, HttpMethod method,
          IDictionary<string, string>? parameters = null,
          object? body = null,
          IDictionary<string, string>? headers = null);

        void SetRequestTimeout(TimeSpan timeout);
    }
}
