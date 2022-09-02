using Newtonsoft.Json;
using Services.Interfaces;
using Services.Models;
using System.Text.RegularExpressions;

namespace Services.Clients
{
    public class LakeFinderClient : ILakeFinderClient, IAsyncDisposable
    {
        private HttpClient httpClient;

        public LakeFinderClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public ValueTask DisposeAsync() => default;

        public async Task<AllLakesPerCountyModel> GetLakesAsync(string countyId)
        {            
            var uri = DnrEndpoints.GetLakesWithinACounty(countyId);
            var httpResponse = await httpClient.GetAsync(uri);
            httpResponse.EnsureSuccessStatusCode();

            //remove junk json
            var jsonString = (await httpResponse.Content.ReadAsStringAsync()).Remove(0, 4);

            jsonString = Regex.Replace(jsonString, @"\t|\n|\r", "");
            jsonString = Regex.Replace(jsonString, @";", "");

            jsonString = jsonString.Remove(jsonString.Length - 1, 1);

            var countyLakes = JsonConvert.DeserializeObject<AllLakesPerCountyModel>(jsonString);
            return countyLakes;
        }

        public async Task<LakesSurveyModel> GetLakeSurveyAsync(string lakeId)
        {
            var uri = DnrEndpoints.GetLakesSurvey(lakeId);
            string lakeSurveyResult = "";

            for (var i = 0; i < 30; i++)
            {
                try
                {
                    var httpResponse = await httpClient.GetAsync(uri);
                    httpResponse.EnsureSuccessStatusCode();
                    lakeSurveyResult = (await httpResponse.Content.ReadAsStringAsync()).Remove(0, 4);
                    break;
                }
                catch (AggregateException)
                {
                    //Try again
                    Thread.Sleep(500);
                }
            }

            lakeSurveyResult = Regex.Replace(lakeSurveyResult, @"\t|\n|\r", "");
            lakeSurveyResult = lakeSurveyResult.Remove(lakeSurveyResult.Length - 1, 1);
            return JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyResult);
        }


    }
}
