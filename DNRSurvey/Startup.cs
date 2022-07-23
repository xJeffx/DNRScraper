using Microsoft.Extensions.DependencyInjection;
using Services;

namespace DNRSurvey
{
    public static class Startup
    {
        public static ServiceProvider Configure()
        {            

            var mapsHostOne = @"http://maps1.dnr.state.mn.us";

            var mapsHostTwo = @"http://maps2.dnr.state.mn.us";

            var services = new ServiceCollection();

            services.AddCountyListClient(httpClient =>            
            {        
                httpClient.BaseAddress = new Uri(mapsHostOne);
            });

            services.AddLakeFinderClient(httpClient =>
            {
                httpClient.BaseAddress = new Uri(mapsHostTwo);
            });


            return services.BuildServiceProvider();
        }
    }
}
