using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SomToday.NET.Configs
{
    public interface IRequest
    {
        Uri BaseAddress { get; }

        Uri Endpoint { get; }

        IDictionary<string, string> Headers { get; }

        IDictionary<string, string> Parameters { get; }

        HttpMethod Method { get; }

        object? Body { get; set; }
    }
}
