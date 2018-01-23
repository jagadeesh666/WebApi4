using Newtonsoft.Json;

namespace Www
{
    public class OdataPagedList<T>
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@odata.count")]
        public string odatacount { get; set; }

        [JsonProperty("@odata.nextLink")]
        public string odatanextLink { get; set; }

        public T[] value { get; set; }
    }
}
