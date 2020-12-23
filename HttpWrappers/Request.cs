using System;
using System.Collections.Generic;
using System.Net.Http;
using SomToday.NET.Configs;

namespace SomToday.NET.HttpWrappers
{
    public struct Request : IRequest
    {
        public Request(
            Uri baseAddress,
            Uri endpoint,
            HttpMethod method,
            IDictionary<string, string> headers,
            IDictionary<string, string> parameters,
            object? body)
        {
            Headers = headers;
            Parameters = parameters;
            BaseAddress = baseAddress;
            Endpoint = endpoint;
            Method = method;
            Body = body;
        }

        public Uri BaseAddress { get; set; }

        public Uri Endpoint { get; set; }

        public IDictionary<string, string> Headers { get; }

        public IDictionary<string, string> Parameters { get; }

        public HttpMethod Method { get; set; }

        public object? Body { get; set; }
    }
}
