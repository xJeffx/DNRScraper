
namespace Services.Models
{
    public class CountyLakesRefModel
    {
        public List<CountyLake> CountyLakes { get; set; }
    }

    public class CountyLake
    {
        public string CountyName { get; set; }
        public string CountyId { get; set; }

        public string LakeName { get; set; }
        public string LakeId { get; set; }
    }
}
