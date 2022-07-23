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

            return new CGIBinClient(httpClient);
        }
    }
}
