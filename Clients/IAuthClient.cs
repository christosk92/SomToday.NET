using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SomToday.NET.Models.Responses;

namespace SomToday.NET.Clients
{
    [Headers("Authorization: Basic RDUwRTBDMDYtMzJEMS00QjQxLUExMzctQTlBODUwQzg5MkMyOnZEZFdkS3dQTmFQQ3loQ0RoYUNuTmV5ZHlMeFNHTkpY")]
    public interface IAuthClient
    {
        [Post("/oauth2/token")]
        Task<OAuthResponse> GetToken([Body(BodySerializationMethod.UrlEncoded)]
            Dictionary<string, object> data);
    }
}
