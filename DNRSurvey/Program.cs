using CommandLine;
using DNRSurvey;
using DNRSurvey.Enums;
using DNRSurvey.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Models;
using System.Reflection;

Console.WriteLine("Hello, Welcome to DNR Survey!");

var provider = Startup.Configure();
var defaultPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
var pathToSurvey = "";
var pathToResults = "";

Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
                   {
                       // Set file paths
                       pathToSurvey = $"{(string.IsNullOrEmpty(o.LakesWithSurveyPath) ? defaultPath : o.LakesWithSurveyPath)}" + @"\LakesWithSurveys.json";
                       pathToResults = $"{(string.IsNullOrEmpty(o.SavePath) ? defaultPath : o.SavePath)}" + $"\\{o.Species.Replace(" ", "")}_Survey_{DateTime.Now.ToString("MM_dd_yyyy_HHmmss")}.txt";

                       var lakeFinderClient = provider.GetRequiredService<ILakeFinderClient>();

                       // Check if the path to survey exists, if it does not exists then extract and transform the data
                       if (!FileHelper.FileExists(pathToSurvey))
                       {
                           Console.WriteLine($"No previous survey was ran. Running Extraction and filtering process to get only lakes with surveys...");
                           DnrExtractionHelper.ExtractJsonData(provider, pathToSurvey);                          
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

