using System.Threading.Tasks;
using GuardAgainstLib;
using SomToday.NET.Api;
using SomToday.NET.Configs;

namespace SomToday.NET.Authenticators
{
    public class TokenAuthenticator : IAuthenticator
    {
        public TokenAuthenticator(string token, string tokenType)
        {
            Token = token;
            TokenType = tokenType;
        }

        public string Token { get; set; }

        public string TokenType { get; set; }

        public Task<(string HeaderName, string HeaderValue)> Apply()
            => Task.FromResult((TokenType, Token));
    }
}
