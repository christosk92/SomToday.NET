﻿using System.Threading.Tasks;
using SomToday.NET.HttpWrappers;

namespace SomToday.NET.Configs
{
    /// <summary>
    ///   The Retry Handler will be directly called after the response is retrived and before errors and body are processed.
    /// </summary>
    public interface IRetryHandler
    {
        delegate Task<IResponse> RetryFunc(IRequest request);

        Task<IResponse> HandleRetry(IRequest request, IResponse response, RetryFunc retry);
    }
}
