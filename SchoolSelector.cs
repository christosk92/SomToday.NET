using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using SomToday.NET.Clients;
using SomToday.NET.Models.Responses;

namespace SomToday.NET
{
    public static class SchoolSelector
    {
        private static IGeneralClient? _generalClient;
        private static List<Instellingen>? _instellingenCache;
        public static async Task<List<Instellingen>> GetSchools(bool useCache = true)
        {
            _generalClient ??= 
                RestService.For<IGeneralClient>("https://servers.somtoday.nl");
            if (useCache)
            {
                if (_instellingenCache != null) return _instellingenCache;
            }

            var data = await _generalClient.GetInstellingen();
            _instellingenCache = data[0].Instellingen;
            return _instellingenCache;
        }
    }
}
