using System;
using Newtonsoft.Json;

namespace SomToday.NET.Models.Responses
{
    public partial class OAuthResponse
    {
        private long _expiresIn;
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("somtoday_api_url")]
        public Uri SomtodayApiUrl { get; set; }

        [JsonProperty("somtoday_oop_url")]
        public Uri SomtodayOopUrl { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("somtoday_tenant")]
        public string SomtodayTenant { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn
        {
            get => _expiresIn;
            set
            {
                _expiresIn = value;
                Created = DateTime.UtcNow;
            }
        }

        public DateTime Created { get; private set; }
        public bool IsValid => (DateTime.UtcNow - Created).TotalSeconds - 10 < ExpiresIn;
    }
}
