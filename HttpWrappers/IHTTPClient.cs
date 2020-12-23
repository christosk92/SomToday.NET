using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SomToday.NET.Configs;

namespace SomToday.NET.HttpWrappers
{
    public interface IHTTPClient : IDisposable
    {
        Task<IResponse> DoRequest(IRequest request);
        void SetRequestTimeout(TimeSpan timeout);
    }
}
