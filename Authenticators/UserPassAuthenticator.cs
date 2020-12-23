using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GuardAgainstLib;
using Newtonsoft.Json;
using Refit;
using SomToday.NET.Api;
using SomToday.NET.Clients;
using SomToday.NET.Configs;
using SomToday.NET.Exceptions;
using SomToday.NET.Models.Responses;

namespace SomToday.NET.Authenticators
{
    public class UserPassAuthenticator : IAuthenticator
    {
        private static IAuthClient? _authClient;
        private static IAuthClient AuthClient
            => _authClient ??= RestService.For<IAuthClient>("https://somtoday.nl");

        private readonly string _username;
        private readonly string _password;
        private readonly string _schoolUuid;

        private static OAuthResponse? _oAuthResponse;
        /// <summary>
        /// Fetches a new bearer & refresh token pair.
        /// </summary>
        /// <param name="username">Your school e-mail.</param>
        /// <param name="password">Your password</param>
        /// <param name="schoolUuid">UUID of your school, can be obtained through <see cref="SchoolSelector"/>
        /// </param>
        public UserPassAuthenticator(
            string username,
            string password,
            string schoolUuid)
        {
            GuardAgainst.ArgumentBeingNullOrEmpty(username, nameof(username));
            GuardAgainst.ArgumentBeingNullOrEmpty(password, nameof(password));

            _username = username;
            _password = password;
            _schoolUuid = schoolUuid;
        }

        public async Task<(string HeaderName, string HeaderValue)> Apply()
        {
            if (_oAuthResponse != null && _oAuthResponse.IsValid)
            {
                return ("Bearer", _oAuthResponse.AccessToken);
            }

            Dictionary<string, object>? data;
            if (_oAuthResponse != null && !_oAuthResponse.IsValid)
            {
                //Refresh
                data = new Dictionary<string, object>
                {
                    {"refresh_token", _oAuthResponse.RefreshToken},
                    {"grant_type", "password"},
                };
            }
            data = new Dictionary<string, object>
            {
                {"username", $@"{_schoolUuid}\{_username}"},
                {"password", _password},
                {"scope", "openid"},
                {"grant_type", "password"},
            };

            try
            {
                var postData = await AuthClient.GetToken(data);
                _oAuthResponse = postData;
                return ("Bearer", _oAuthResponse.AccessToken);
            }
            catch (ApiException x)
            {
                switch (x.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.Unauthorized:
                        var error = JsonConvert.DeserializeObject<OAuthError>(x.Content);
                        var numb = Regex.Match(error.ErrorDescription, @"\d+")?.Value;
                        if (int.TryParse(numb, out var retries))
                        {
                            throw new SomTodayUnauthorizedException(retries, error.ErrorDescription);
                        }

                        throw new SomTodayUnauthorizedException(-1, error.ErrorDescription);
                }
            }

            return (null, null);
        }
    }
}
