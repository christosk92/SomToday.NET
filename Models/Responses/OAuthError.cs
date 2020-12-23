using Newtonsoft.Json;

namespace SomToday.NET.Models.Responses
{
    public class OAuthError
    {
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
