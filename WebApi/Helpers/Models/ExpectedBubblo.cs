using System;
using Newtonsoft.Json;

namespace Wwww
{
    public class ExpectedPeril
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("shortCode")]

        public string shortCode { get; set; }
        [JsonProperty("name")]

        public string name { get; set; }
        [JsonProperty("lastUpdateTime")]

        public DateTime lastUpdateTime { get; set; }
        [JsonProperty("description")]

        public virtual ExpectedPerilDescription description { get; set; }
    }
}