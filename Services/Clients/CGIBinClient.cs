using Newtonsoft.Json;
using Services.Interfaces;
using Services.Models;
using System.Text.RegularExpressions;

namespace Services.Clients
{
    public class CGIBinClient : ICGIBinClient, IAsyncDisposable
    {
        private HttpClient httpClient;

        public CGIBinClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public ValueTask DisposeAsync() => default;

        public async Task<MNCountiesModel> GetCountyListAsync()
        {
            var countyPath = DnrEndpoints.GetCounties;
            //var httpResponse = httpClient.GetAsync(countyPath).WaitAsync(new TimeSpan(0,0,60)).Result;
           
            var httpResponse = await httpClient.GetAsync(countyPath);

            httpResponse.EnsureSuccessStatusCode();

            //replace junk on json
            var jsonString = (await httpResponse.Content.ReadAsStringAsync()).Remove(0, 4);
            jsonString = Regex.Replace(jsonString, @"\t|\n|\r", "");
            jsonString = jsonString.Remove(jsonString.Length - 1, 1);

            return JsonConvert.DeserializeObject<MNCountiesModel>(jsonString);
        }
    }
}
