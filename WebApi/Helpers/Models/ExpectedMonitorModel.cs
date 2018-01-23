using System.Collections.Generic;
using Newtonsoft.Json;

namespace Www
{
    public class ExpectedMonitorModel<T>
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
        public List<T> Results { get; set; }

        //public virtual ICollection<MonitorResults> Results { get; set; }
    }

    public partial class MonitorResults
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }

    }
}