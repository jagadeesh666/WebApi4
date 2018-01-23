using Newtonsoft.Json;

namespace Www
{
    public class ExpectedHeartbeatModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Version")]
        public string Version { get; set; }
        [JsonProperty("ServiceName")]
        public string ServiceName { get; set; }

        [JsonProperty("TimeStamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("Results")]
        public string Results { get; set; }
    }
}