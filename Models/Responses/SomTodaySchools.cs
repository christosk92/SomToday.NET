using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SomToday.NET.Models.Responses
{
    internal class SomTodaySchools
    {
        [JsonProperty("instellingen")]
        public List<Instellingen> Instellingen { get; set; }
    }

    public class Instellingen
    {
        /// <summary>
        /// Unique UUID of a school in format:
        /// xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx1
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Name of the school
        /// </summary>
        [JsonProperty("naam")]
        public string Naam { get; set; }

        /// <summary>
        /// Location of the school (Accurate to city)
        /// </summary>
        [JsonProperty("plaats")]
        public string Plaats { get; set; }

        /// <summary>
        /// Not yet known. Could be urls for 2fa?
        /// </summary>
        [JsonProperty("oidcurls")]
        public List<Oidcurl> Oidcurls { get; set; }
    }

    public class Oidcurl
    {
        [JsonProperty("omschrijving")]
        public string Omschrijving { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("domain_hint")]
        public string DomainHint { get; set; }
    }
}
