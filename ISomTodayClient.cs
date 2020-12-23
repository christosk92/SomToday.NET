using System;
using System.Collections.Generic;
using System.Text;
using SomToday.NET.Clients;
using SomToday.NET.HttpWrappers;

namespace SomToday.NET
{
    public interface ISomTodayClient
    {
        /// <summary>
        /// Returns the last response received by an API call.
        /// </summary>
        /// <value></value>
        IResponse? LastResponse { get; }

        ILeerlingClient LeerlingClient { get; }

    }
}
