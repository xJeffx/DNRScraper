using Services.Clients;
using Services.Interfaces;

namespace Services.Factory
{
    public static class CgiBinClientFactory
    {
        public static ICgiBinClient Create(string host)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(host)
            };

            return new CgiBinClient(httpClient);
        }
    }
}
