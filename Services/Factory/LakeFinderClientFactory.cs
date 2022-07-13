using Services.Clients;
using Services.Interfaces;

namespace Services.Factory
{
    public static class LakeFinderClientFactory
    {
        public static ILakeFinderClient Create(string host)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(host)
            };

           // ConfigureHttpClient(httpClient, host);

            return new LakeFinderClient(httpClient);
        }

        //internal static void ConfigureHttpClient(HttpClient httpClient, string host)
        //{
        //    ConfigureHttpClientCore(httpClient);
        //}

        //internal static void ConfigureHttpClientCore(HttpClient httpClient)
        //{
        //    httpClient.DefaultRequestHeaders.Accept.Clear();
        //    //httpClient.DefaultRequestHeaders.Accept.Add(new("text/plain"));
        //}
    }
}
