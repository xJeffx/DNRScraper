using Services.Models;

namespace Services.Factory
{
    public static class CountyLakesFactory
    {
        public static CountyLake Create(CountyData county, LakeData lake)
        {
            return new CountyLake()
            {
                CountyId = county.id,
                CountyName = county.name,
                LakeId = lake.id,
                LakeName = lake.name
            };
        }
    }
}
