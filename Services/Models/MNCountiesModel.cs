using Newtonsoft.Json;

namespace Services.Models
{

    public class MNCountiesModel
    {
        public string status { get; set; }

        [JsonProperty(PropertyName = "results")]
        public CountyData[] counties { get; set; }
        public string message { get; set; }
    }

    public class CountyData
    {
        public Bbox bbox { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string county { get; set; }
        public string type { get; set; }
    }

    public class Bbox
    {
        public float[] epsg26915 { get; set; }
        public float[] epsg4326 { get; set; }
    }
}
