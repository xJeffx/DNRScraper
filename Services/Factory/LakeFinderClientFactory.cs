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

            return new LakeFinderClient(httpClient);
        }
    }
}
