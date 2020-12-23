using System.Collections.Generic;
using System.Net;

namespace SomToday.NET.HttpWrappers
{
    public interface IResponse
    {
        object? Body { get; }

        IReadOnlyDictionary<string, string> Headers { get; }

        HttpStatusCode StatusCode { get; }

        string? ContentType { get; }
    }
}