using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using GuardAgainstLib;

namespace SomToday.NET.HttpWrappers
{
    public struct Response : IResponse
    {
        public Response(IDictionary<string, string> headers,
            object? body,
            HttpStatusCode statusCode,
            string? contentType)
        {
            GuardAgainst.ArgumentBeingNull(headers, nameof(headers));

            Headers = new ReadOnlyDictionary<string, string>(headers);
            Body = body;
            StatusCode = statusCode;
            ContentType = contentType;
        }

        public object? Body { get; set; }

        public IReadOnlyDictionary<string, string> Headers { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string? ContentType { get; set; }
    }
}
