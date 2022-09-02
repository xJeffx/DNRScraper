using Services.Factory;
using Services.Interfaces;
using Services.Models;

namespace DNRSurvey.Utilities
{
    public static class LakesHelper
    {
        /// <summary>
        /// Cycles through the county lakes and returns only those with surveys
        /// </summary>
        /// <param name="client">The lakefinder client</param>
        /// <param name="county">The county to pull lake data from</param>
        /// <param name="countyLakes"></param>
        /// <returns></returns>
        public static List<CountyLake> GetOnlyLakesWithSurveys(ILakeFinderClient client, CountyData county, AllLakesPerCountyModel countyLakes)
        {
            var countyLakesWithSurvey = new List<CountyLake>();

            if (countyLakes.lakeDataResults == null)
            {
                Console.WriteLine($"County {county.name} with ID {county.id} has no Lakes");
                return countyLakesWithSurvey;
            }

            foreach (var lake in countyLakes.lakeDataResults)
            {
                //// Get all surveys for the lake
                var lakesWithSurvey = GetLakesSurveyDataAsync(client, lake.id).GetAwaiter().GetResult();

                if (lakesWithSurvey.result?.surveys != null)
                {                    
                    var surveyWitNotNullLength = GetLatestSurveyWithLengthsData(lakesWithSurvey);
                   
                    //// Check if lenghts exits
                    if (surveyWitNotNullLength != null)
                    {
                        countyLakesWithSurvey.Add(CountyLakesFactory.Create(county, lake));
                    }
                }

            }

            return countyLakesWithSurvey;
        }   
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="lakeId"></param>
        /// <returns></returns>
        public static async Task<LakesSurveyModel> GetLakesSurveyDataAsync(ILakeFinderClient client, string lakeId)
        {
            return await client.GetLakeSurveyAsync(lakeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lakesWithSurvey"></param>
        /// <returns></returns>
        private static FishSurvey GetLatestSurveyWithLengthsData(LakesSurveyModel lakesWithSurvey)
        {
            var surveyWitNotNullLength = lakesWithSurvey.result?.surveys?.LastOrDefault(n => n.lengths != null);
            var lastSurvey = lakesWithSurvey.result?.surveys?.Last();


            if (surveyWitNotNullLength != null && lastSurvey != null && !surveyWitNotNullLength.surveyID.Equals(lastSurvey.surveyID))
            {
                Console.WriteLine($"Warning: lastest survey and lastest survey with length data not the same. Lake - {lakesWithSurvey.result?.lakeName}");
            }

            return surveyWitNotNullLength;

        }
    }
}
