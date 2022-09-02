using Newtonsoft.Json;
using Services.Interfaces;
using Services.Models;
using System.Text.RegularExpressions;

namespace Services.Clients
{
    public class LakeFinderClient : ILakeFinderClient, IAsyncDisposable
    {
        private readonly HttpClient httpClient;

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
            var jsonString = FormatReponse(await httpResponse.Content.ReadAsStringAsync());
           
           

            return JsonConvert.DeserializeObject<AllLakesPerCountyModel>(jsonString);
        }

        public async Task<LakesSurveyModel> GetLakeSurveyAsync(string lakeId)
        {
            var uri = DnrEndpoints.GetLakesSurvey(lakeId);
            string lakeSurveyResult = "";

            // TODO : Get this working. Implement wait in future
            for (var i = 0; i < 30; i++)
            {
                try
                {
                    var httpResponse = await httpClient.GetAsync(uri);
                    httpResponse.EnsureSuccessStatusCode();
                    lakeSurveyResult = FormatReponse(await httpResponse.Content.ReadAsStringAsync());
                    break;
                }
                catch (AggregateException)
                {
                    //Try again
                    Thread.Sleep(500);
                }
            }

            return JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyResult);
        }

        private string FormatReponse(string reponse)
        {
            // Remove junk Foo
            var formattedResponse = reponse.Remove(0, 4);
            formattedResponse = Regex.Replace(formattedResponse, @"\t|\n|\r", "");
            formattedResponse = Regex.Replace(formattedResponse, @";", "");
            return formattedResponse.Remove(formattedResponse.Length - 1, 1);
        }

    }
}
