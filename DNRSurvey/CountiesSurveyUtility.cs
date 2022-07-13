using Newtonsoft.Json;
using Services.Interfaces;
using Services.Models;

namespace DNRSurvey
{
    public static class CountiesSurveyUtility
    {
        /**
         * Cycles through the county lakes and returns only those with surveys
         * 
         * 
         **/
        public static List<CountyLakeRef> GetOnlyLakesWithSurveys(ILakeFinderClient client, CountyData county, AllCountyLakesModel countyLakes)
        {
            var countyLakesWithSurvey = new List<CountyLakeRef>();

            if (countyLakes.lakeDataResults == null)
            {
                Console.WriteLine($"County {county.name} with ID {county.id} has no Lakes");
                return countyLakesWithSurvey;
            }

            Console.WriteLine($"Checking County {county.name} with ID {county.id} has Lakes");
            foreach (var lake in countyLakes.lakeDataResults)
            {
                //// Get all surveys for the lake
                var lakesWithSurvey = GetLakesSurveyDataAsync(client, lake.id).GetAwaiter().GetResult();
               
                    if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                    {
                        //// Specific Fish Code
                        var survey = lakesWithSurvey.result.surveys.Last();

                        //// Check if lenghts exits and crappy are available
                        if (survey.lengths != null)
                        {
                            countyLakesWithSurvey.Add(new CountyLakeRef()
                            {
                                CountyId = county.id,
                                CountyName = county.name,
                                LakeId = lake.id,
                                LakeName = lake.name
                            });
                        }
                    }
                
            }

            Console.WriteLine($"Checking County {county.name} with ID {county.id} has {countyLakesWithSurvey.Count} Lakes with Surveys");
            return countyLakesWithSurvey;
        }
    


        //public static async Task<AllCountyLakesModel> GetAllLakesAsync(ILakeFinderClient client, string countyId)
        //{
        //    return await client.GetLakesAsync(countyId);
        //}        
        public static async void WriteLakesSurveyJson(CountyLakesRefModel counties, string savepath)
        {
            if (counties.CountyLakes.Count != 0)
            {
                using (StreamWriter file = File.CreateText(savepath))
                {
                    using (JsonTextWriter writer = new JsonTextWriter(file) { Formatting = Formatting.Indented })
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, counties);
                    }
                }
            }
        }

        private static async Task<LakesSurveyModel> GetLakesSurveyDataAsync(ILakeFinderClient client, string lakeId)
        {
            return await client.GetLakeSurveyAsync(lakeId);
        }
    }
}
