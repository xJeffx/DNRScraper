using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebServiceModel
{
    public class LakesModel
    {
        public string status { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public Result[] results { get; set; }
        
        public string message { get; set; }
    }

    public partial class Result
    {
        public Resources resources { get; set; }
        public string[] fishSpecies { get; set; }
        public Specialfishingreg[] specialFishingRegs { get; set; }
        public string[] mapid { get; set; }
        public string border { get; set; }
        public string[] apr_ids { get; set; }
        public Point point { get; set; }
        public string notes { get; set; }
        public string nearest_town { get; set; }
        public string[] invasiveSpecies { get; set; }
        public string pca_id { get; set; }
    }

    public class Resources
    {
        public int fca { get; set; }
        public int lakeSurvey { get; set; }
        public int specialFishingRegs { get; set; }
        public int waterQuality { get; set; }
        public int waterLevels { get; set; }
        public int lakeMap { get; set; }
        public int waterAccess { get; set; }
        public int fishStocking { get; set; }
    }

    public class Point
    {
        public int[] epsg26915 { get; set; }
        public float[] epsg4326 { get; set; }
    }

    public class Specialfishingreg
    {
        public Reg[] regs { get; set; }
        public string location { get; set; }
        public int locDisplayType { get; set; }
    }

    public class Reg
    {
        public string text { get; set; }
        public string[] species { get; set; }
    }
}
