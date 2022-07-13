using CommandLine;
using DNRSurvey;
using Microsoft.Extensions.DependencyInjection;
using Services.Factory;
using Services.Interfaces;
using Services.Models;

Console.WriteLine("Hello, Welcome to DNR Survey!");

var provider = Startup.Configure();
var pathToSurvey = "";

//var countiesWithSurveys = new CountyLakesRefModel() { CountyLakes = new List<CountyLakeRef>() };
//countiesWithSurveys.CountyLakes.Add(new CountyLakeRef()
//{
//    CountyId = "1",
//    CountyName = "jeff",
//    LakeId = "1",
//    LakeName = "12"
//});
//CountiesSurveyUtility.WriteLakesSurveyJson(countiesWithSurveys, @"C:\DNRResult\LakesWithSurveys.json");


Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(async o =>
                   {
                       if (!string.IsNullOrEmpty(o.SavePath))
                       {
                           Utilities.CreateFilePathIfNotExist(o.SavePath);
                       }
                       else
                       {
                           Utilities.CreateFilePathIfNotExist(@"C:\DNRResult");
                       }

                       // No previous survey was ran, running it all to generate the lakes with survey
                       if (string.IsNullOrEmpty(o.LakesWithSurveyPath) || !Utilities.FileExists(o.LakesWithSurveyPath))
                       {
                           pathToSurvey = @"C:\DNRResult\LakesWithSurveys.json";
                           var countiesWithSurveys = new CountyLakesRefModel() { CountyLakes = new List<CountyLakeRef>() };

                           // Get all counties so we can find which have lake surveys
                           var countyList = provider.GetRequiredService<ICGIBinClient>().GetCountyListAsync().GetAwaiter().GetResult();

                           // Get all county lakes with surveys
                           foreach (var county in countyList.counties)
                           {
                               var client = provider.GetRequiredService<ILakeFinderClient>();
                               var lakes = client.GetLakesAsync(county.id).GetAwaiter().GetResult();
                               var lakesWithSurveys = CountiesSurveyUtility.GetOnlyLakesWithSurveys(client, county, lakes);
                               countiesWithSurveys.CountyLakes.AddRange(lakesWithSurveys);
                           }

                           // write to json
                           CountiesSurveyUtility.WriteLakesSurveyJson(countiesWithSurveys, pathToSurvey);

                       }
                       else
                       {
                           pathToSurvey = o.LakesWithSurveyPath;
                       }

                       if (!string.IsNullOrEmpty(o.Species))
                       {
                           Console.WriteLine($"You requested Species is '{o.Species}'");
                       }
                   });

