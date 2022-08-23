using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Models;

namespace DNRSurvey.Utilities
{
    public static class DnrExtractionHelper
    {
        /// <summary>
        /// DNR Data ETL. Running Extraction and transformation process.
        /// Extract the data
        /// Transform the data
        /// Filter the date
        /// </summary>
        /// <param name="provider">The service provider</param>
        /// <param name="savePath">The path to save the json file to</param>
        public static void ExtractJsonData(ServiceProvider provider, string savePath)
        {
            // Extract data from DNR
            var countiesWithSurveys = new CountyLakesRefModel() { CountyLakes = new List<CountyLake>() };

            var lakeFinderClient = provider.GetRequiredService<ILakeFinderClient>();

            // Get all counties so we can find which have lake surveys
            var countyList = provider.GetRequiredService<ICgiBinClient>().GetCountyListAsync().GetAwaiter().GetResult();

            Console.WriteLine($"Extracting data...");

            // Get all county lakes with surveys
            foreach (var county in countyList.counties)
            {
                var lakes = lakeFinderClient.GetLakesAsync(county.id).GetAwaiter().GetResult();

                // Filter and trasform the data to only include counties and lakes with survey data
                var lakesWithSurveys = LakesHelper.GetOnlyLakesWithSurveys(lakeFinderClient, county, lakes);
                countiesWithSurveys.CountyLakes.AddRange(lakesWithSurveys);
            }

            // Write data to json
            Console.WriteLine($"Generating json data file...");
            FileHelper.WriteJSONToFile(countiesWithSurveys, savePath);
        }
    }
}
