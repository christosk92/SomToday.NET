using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using SomToday.NET.Models.Responses;

namespace SomToday.NET.Clients
{
    internal interface IGeneralClient
    {
        [Get("/organisaties.json")]
        Task<List<SomTodaySchools>> GetInstellingen();
    }
}
