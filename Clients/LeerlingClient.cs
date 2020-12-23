using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using SomToday.NET.Attributes;

namespace SomToday.NET.Clients
{
    [BaseUrl("https://api.somtoday.nl")]
    public interface ILeerlingClient
    {
        [Get("/rest/v1/leerlingen")]
        Task<HttpResponseMessage> GetCurrentLeerling();
    }
}
