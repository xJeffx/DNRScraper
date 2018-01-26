using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceModel
{

    public class CountyModel
    {
        public string status { get; set; }
        public Result[] results { get; set; }
        public string message { get; set; }
    }

    public partial class Result
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
