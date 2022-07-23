using Microsoft.Extensions.DependencyInjection;
using Services.Clients;
using Services.Interfaces;

namespace Services
{
    // Using DI to create our httpclients
    public static class SeviceCollectionExtensions
    {
        public static IHttpClientBuilder AddCountyListClient(this IServiceCollection services, Action<HttpClient> configureClient) =>
            services.AddHttpClient<ICGIBinClient, CGIBinClient>((httpClient) =>
            {
                configureClient(httpClient);
            });

        public static IHttpClientBuilder AddLakeFinderClient(this IServiceCollection services, Action<HttpClient> configureClient) =>
            services.AddHttpClient<ILakeFinderClient, LakeFinderClient>((httpClient) =>
            {
                configureClient(httpClient);
            });
    }
}
