using CommandLine;
using DNRSurvey;
using DNRSurvey.Enums;
using DNRSurvey.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Models;

Console.WriteLine("Hello, Welcome to DNR Survey!");

var provider = Startup.Configure();
var pathToSurvey = "";
var pathToResults = "";
Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(async o =>
                   {
                       pathToSurvey = string.IsNullOrEmpty(o.LakesWithSurveyPath) ? @"C:\DNRResult\LakesWithSurveys.json" : o.LakesWithSurveyPath;
                       pathToResults = $"{(string.IsNullOrEmpty(o.SavePath) ? @"C:\DNRResult" : o.SavePath)}" + $"\\{o.Species.Replace(" ", "")}_Survey_{DateTime.Now.ToString("MM_dd_yyyy_HHmmss")}.txt";

                       var lakeFinderClient = provider.GetRequiredService<ILakeFinderClient>();

                       // No previous survey was ran, running it all to generate the lakes with survey
                       if (!FileHelper.FileExists(pathToSurvey))
                       {

                           var countiesWithSurveys = new CountyLakesRefModel() { CountyLakes = new List<CountyLake>() };

                           // Get all counties so we can find which have lake surveys
                           var countyList = provider.GetRequiredService<ICGIBinClient>().GetCountyListAsync().GetAwaiter().GetResult();


                           // Get all county lakes with surveys
                           foreach (var county in countyList.counties)
                           {
                               var lakes = lakeFinderClient.GetLakesAsync(county.id).GetAwaiter().GetResult();
                               var lakesWithSurveys = LakesHelper.GetOnlyLakesWithSurveys(lakeFinderClient, county, lakes);
                               countiesWithSurveys.CountyLakes.AddRange(lakesWithSurveys);
                           }

                           // write to json
                           FileHelper.WriteJSONToFile(countiesWithSurveys, pathToSurvey);

                       }

                       if (!string.IsNullOrEmpty(o.Species))
                       {
                           Console.WriteLine($"You requested Species is '{o.Species}'");
                       }

                       // Get the county lakes reference model from saved data json file
                       CountyLakesRefModel countyLakes = JSONHelper.ConvertFromJson<CountyLakesRefModel>(pathToSurvey);

                       // Get SpeciesEnum, get the enum from the string
                       var speciesEnum = SpeciesHelper.GetSpeciesEnum(o.Species);

                       // Get latest survey with data and build file result
                       var resultsBuilder = new ResultsFileBuilderHelper();

                       foreach (var lake in countyLakes.CountyLakes)
                       {
                           // Get all surveys for the lake
                           var lakeSurveys = LakesHelper.GetLakesSurveyDataAsync(lakeFinderClient, lake.LakeId).GetAwaiter().GetResult();

                           // Get the latest survey with species length data
                           if (lake.LakeId.Equals("01006200"))
                           {
                               Console.WriteLine($"Found Big Sandy");
                           }

                           var latestSurvey = SurveyHelper.GetLastestSurveyWithSpeciesLengthData(lakeSurveys.result.surveys, speciesEnum);

                           if (latestSurvey == null)
                           {
                               Console.WriteLine($"Servey was found but the requested Species '{o.Species}' has no length information for lake {lake.LakeName} in county {lake.CountyName}");
                               Console.WriteLine($"LakeFinder URL = 'https://www.dnr.state.mn.us/lakefind/lake.html?id={lake.LakeId}'");
                               continue;

                           }

                           // Get the fish lengths for the latest survey
                           try
                           {
                               var speciesData = SurveyHelper.GetSpeciesData(latestSurvey, speciesEnum);
                               var speciesLengths = SurveyHelper.GetSpeciesLengths(speciesData);

                               resultsBuilder.AddSpeciesInformation(speciesEnum.ToString());
                               resultsBuilder.AddSurveyInformation(latestSurvey, lake);
                               resultsBuilder.AddSizeInformation(speciesLengths, o.MinSize);
                           }
                           catch (Exception e)
                           {
                               Console.WriteLine($"Error processing surveys.  {lake.CountyName} - {lake.LakeName}");
                               Console.WriteLine($"Error {e.Message} - {e.StackTrace}");
                               throw;
                           }
                       }

                       FileHelper.WriteTextToFile(resultsBuilder.Build(), pathToResults);
                   });

