using Services.Interfaces;
using Services.Models;

namespace DNRSurvey.Utilities
{
    public static class LakesHelper
    {      
        /**
         * Cycles through the county lakes and returns only those with surveys
         * 
         * 
         **/
        public static List<CountyLake> GetOnlyLakesWithSurveys(ILakeFinderClient client, CountyData county, AllCountyLakesModel countyLakes)
        {
            var countyLakesWithSurvey = new List<CountyLake>();

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
                    var surveyWitNotNullLength = lakesWithSurvey.result.surveys.LastOrDefault(n=> n.lengths != null);
                    var lastSurvey = lakesWithSurvey.result.surveys.Last();


                    if(surveyWitNotNullLength != null && lastSurvey != null && !surveyWitNotNullLength.surveyID.Equals(lastSurvey.surveyID))
                    {
                        Console.WriteLine($"Lastest survey and lastest survey with length data not the same. {county.name} with Lake {lake.id}: {lake.name}");
                    }
                 
                    //// Check if lenghts exits and crappy are available
                    if (surveyWitNotNullLength != null)
                    {
                        countyLakesWithSurvey.Add(new CountyLake()
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
   
        public static async void WriteLakesSurveyJson(CountyLakesRefModel counties, string savepath)
        {
            if (counties.CountyLakes.Count != 0)
            {
                FileHelper.WriteJSONToFile(counties, savepath);
            }
        }

        public static async Task<LakesSurveyModel> GetLakesSurveyDataAsync(ILakeFinderClient client, string lakeId)
        {
            return await client.GetLakeSurveyAsync(lakeId);
        }
    }
}
