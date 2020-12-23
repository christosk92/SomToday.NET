using System.Threading.Tasks;
using SomToday.NET.Api;
using SomToday.NET.Configs;

namespace SomToday.NET.Authenticators
{
    public interface IAuthenticator
    {
        Task<(string HeaderName, string HeaderValue)> Apply();
    }
}
