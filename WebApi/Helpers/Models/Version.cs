using Newtonsoft.Json;

namespace WTW
{
    public class Version
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("company")]
        public string company { get; set; }
        [JsonProperty("copyright")]
        public string copyright { get; set; }
        [JsonProperty("version")]
        public string version { get; set; }
    }
}
