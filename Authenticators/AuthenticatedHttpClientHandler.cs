using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SomToday.NET.Authenticators
{
    internal class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly IAuthenticator _authenticator;

        public AuthenticatedHttpClientHandler(IAuthenticator authenticator)
        {
            this._authenticator = authenticator 
                                  ?? throw new ArgumentNullException(nameof(authenticator));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var (headerName, headerValue) = await _authenticator.Apply().ConfigureAwait(false);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(headerName,
                headerValue);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}