#nullable enable
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GuardAgainstLib;
using Refit;
using SomToday.NET.Api;
using SomToday.NET.Attributes;
using SomToday.NET.Authenticators;
using SomToday.NET.Clients;
using SomToday.NET.Configs;
using SomToday.NET.HttpWrappers;

namespace SomToday.NET
{
    public class SomTodayClient : ISomTodayClient
    {
        private readonly IApiConnector _apiConnector;
        public SomTodayClient(IToken token)
            :
            this(SomTodayClientConfig.CreateDefault(token?.AccessToken ?? throw new ArgumentNullException(nameof(token)),
                token.TokenType))
        {

        }

        public SomTodayClient(string token, string tokenType = "Bearer") :
            this(SomTodayClientConfig.CreateDefault(token, tokenType))
        {

        }

        public SomTodayClient(SomTodayClientConfig config)
        {
            GuardAgainst.ArgumentBeingNull(config, nameof(config));
            if (config.Authenticator == null)
            {
                throw new NullReferenceException("Authenticator in config is null. " +
                                                 "Please supply it via `WithAuthenticator` or `WithToken`");
            }

            _apiConnector = new ApiConnector(
                config.Authenticator,
                config.HTTPClient,
                config.RetryHandler);
        }

        public IResponse? LastResponse => _apiConnector.LastResponse;
        public ILeerlingClient LeerlingClient => _apiConnector.LeerlingClient;
    }
}
