using Services.Clients;
using Services.Interfaces;

namespace Services.Factory
{
    public static class CGIBinClientFactory
    {
        public static ICGIBinClient Create(string host)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(host)
            };

            //ConfigureHttpClient(httpClient, host);

            return new CGIBinClient(httpClient);
        }

        //internal static void ConfigureHttpClient(HttpClient httpClient)
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
