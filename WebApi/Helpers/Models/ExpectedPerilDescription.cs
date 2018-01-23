using Newtonsoft.Json;

namespace Www
{
    public class ExpectedPerilDescription
    {
        [JsonProperty("perilId")]
        public int perilId { get; set; }
        [JsonProperty("desc")]
        public string desc { get; set; }

    }
}