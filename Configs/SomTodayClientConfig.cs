#nullable enable
using System;
using System.Collections.Generic;
using System.Text;
using GuardAgainstLib;
using SomToday.NET.Authenticators;
using SomToday.NET.Cache;
using SomToday.NET.HttpWrappers;
using SomToday.NET.Paging;

namespace SomToday.NET.Configs
{
    public class SomTodayClientConfig
    {
        public IAuthenticator? Authenticator { get; private set; }
        public IHTTPClient HTTPClient { get; private set; }
        public IRetryHandler? RetryHandler { get; private set; }
        public IPaginator DefaultPaginator { get; private set; }
        public Cacher? Cacher { get; private set; }

        /// <summary>
        ///   This config spefies the internal parts of the SomTodayClient.
        /// </summary>
        /// <param name="authenticator"></param>
        /// <param name="httpClient"></param>
        /// <param name="retryHandler"></param>
        /// <param name="defaultPaginator"></param>
        /// <param name="cacher">Override <see cref="Cacher"/></param>
        public SomTodayClientConfig(
            IAuthenticator? authenticator,
            IHTTPClient httpClient,
            IRetryHandler? retryHandler,
            IPaginator defaultPaginator,
            Cacher? cacher
        )
        {
            Authenticator = authenticator;
            HTTPClient = httpClient;
            RetryHandler = retryHandler;
            DefaultPaginator = defaultPaginator;
            Cacher = cacher;
        }

        public SomTodayClientConfig WithToken(string token, string tokenType = "Bearer")
        {
            GuardAgainst.ArgumentBeingNullOrEmpty(token, nameof(token));

            return new SomTodayClientConfig(
                new TokenAuthenticator(token, tokenType),
                HTTPClient,
                RetryHandler,
                DefaultPaginator,
                Cacher
            );
        }

        public SomTodayClientConfig WithRetryHandler(IRetryHandler retryHandler)
        {
            return new SomTodayClientConfig(
                Authenticator,
                HTTPClient,
                retryHandler,
                DefaultPaginator,
                Cacher
            );
        }

        public SomTodayClientConfig WithAuthenticator(IAuthenticator authenticator)
        {
            GuardAgainst.ArgumentBeingNull(authenticator, nameof(authenticator));

            return new SomTodayClientConfig(
                authenticator,
                HTTPClient,
                RetryHandler,
                DefaultPaginator,
                Cacher
            );
        }

        public SomTodayClientConfig WithHttpClient(IHTTPClient httpClient)
        {
            GuardAgainst.ArgumentBeingNull(httpClient, nameof(httpClient));

            return new SomTodayClientConfig(
                Authenticator,
                httpClient,
                RetryHandler,
                DefaultPaginator,
                Cacher
            );
        }

        public SomTodayClientConfig WithDefaultPaginator(IPaginator defaultPaginator)
        {
            GuardAgainst.ArgumentBeingNull(defaultPaginator, nameof(defaultPaginator));

            return new SomTodayClientConfig(
                Authenticator,
                HTTPClient,
                RetryHandler,
                defaultPaginator,
                Cacher
            );
        }

        public static SomTodayClientConfig CreateDefault(string token, string tokenType = "Bearer")
        {
            return CreateDefault().WithAuthenticator(new TokenAuthenticator(token, tokenType));
        }

        public static SomTodayClientConfig CreateDefault()
        {
            return new SomTodayClientConfig(
                null,
                new NetHttpClient(),
                null,
                new SimplePaginator(),
                new StdCacher(TimeSpan.MaxValue)
            );
        }
    }
}
