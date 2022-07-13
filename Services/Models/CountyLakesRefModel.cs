
namespace Services.Models
{
    public class CountyLakesRefModel
    {
        public List<CountyLakeRef> CountyLakes { get; set; }
    }

    public class CountyLakeRef
    {
        public string CountyName { get; set; }
        public string CountyId { get; set; }

        public string LakeName { get; set; }
        public string LakeId { get; set; }
    }
}
