using System.Threading;
using Magenic.MaqsFramework.BaseWebServiceTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServiceModel;

namespace Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    /// <summary>
    /// Tests test class
    /// </summary>
    [TestClass]
    public class CountiesLakesAndSurveys : BaseWebServiceTest
    {
        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetAllLakesWithSurvey()
        {
            var countiesJsonOutput = new CountyWithLakesAndSurvey() { CountyLakes = new List<CountyLakesWithSurvey>() };

            //// Get all counties
            int startCountyIndex = 1;

            this.WebServiceWrapper.BaseHttpClient.BaseAddress = new Uri("http://maps1.dnr.state.mn.us");

            var countiesResult = this.WebServiceWrapper.Get("/cgi-bin/gazetteer2.cgi?type=county&callback=foo&_=15100205911351", "text/plain", false);

            var jsonString = countiesResult.Remove(0, 4);
            jsonString = Regex.Replace(jsonString, @"\t|\n|\r", "");
            jsonString = jsonString.Remove(jsonString.Length - 1, 1);

            var counties = JsonConvert.DeserializeObject<CountyModel>(jsonString);

            foreach (var county in counties.results)
            {
                //// Get All lakes
                /// Loop through all counties
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                string lakesResult = "";
                LakesModel lakesWithinCounty;
                try
                {
                    lakesResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder_json.cgi?context=desktop&callback=foo&name=&county={county.id}&_=1510020650576", "text/plain", false);
                    var lakesJsonString = lakesResult.Remove(0, 4);
                    lakesJsonString = Regex.Replace(lakesJsonString, @"\t|\n|\r", "");
                    lakesJsonString = Regex.Replace(lakesJsonString, @";", "");

                    lakesJsonString = lakesJsonString.Remove(lakesJsonString.Length - 1, 1);

                    lakesWithinCounty = JsonConvert.DeserializeObject<LakesModel>(lakesJsonString);

                }
                catch (Exception e)
                {
                    this.TestObject.Log.LogMessage(
                        $"Error with countyId: {county.id} {county.id} and JSON:{System.Environment.NewLine}");
                    this.TestObject.Log.LogMessage($"{lakesResult}");
                    throw e;
                }

                if (lakesWithinCounty.results == null)
                {
                    this.TestObject.Log.LogMessage(
                        $"Skipping countyId: {county.id} and Status:{lakesWithinCounty.status} and Message: {lakesWithinCounty.message}");
                    continue;
                }

                foreach (var lake in lakesWithinCounty.results)
                {
                    //// Get all surveyes
                    this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                    string lakeSurveyResult = "";
                   
                    for (var i = 0; i < 30; i++)
                    {
                        if (String.IsNullOrWhiteSpace(lakeSurveyResult))
                        {
                            try
                            {
                                  lakeSurveyResult = this.WebServiceWrapper.Get(
                                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.id}&_=1510019564259",
                                    "text/plain", false);
                            }
                            catch (AggregateException)
                            {
                                //Try again
                                Thread.Sleep(500);
                                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (String.IsNullOrWhiteSpace(lakeSurveyResult))
                    {
                        this.TestObject.Log.LogMessage(
                            $"Error with countyId{county.id} {lake.id}.");
                    }

                    var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                    lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                    lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                    try
                    {
                        var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);
                        if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                        {
                            //// Specific Fish Code
                            var survey = lakesWithSurvey.result.surveys.Last();

                            //// Check if lenghts exits and crappy are available
                            if (survey.lengths != null)
                            {
                                countiesJsonOutput.CountyLakes.Add(new CountyLakesWithSurvey()
                                {
                                    CountyId = county.id,
                                    CountyName = county.name,
                                    LakeId = lake.id,
                                    LakeName = lake.name
                                });
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        this.TestObject.Log.LogMessage(
                            $"Error with countyId{county.id} {lake.id} and JSON:{System.Environment.NewLine}");
                        this.TestObject.Log.LogMessage($"{lakeSurveyJsonString}");
                        throw e;
                    }
                }
            }

            if (countiesJsonOutput.CountyLakes.Count != 0)
            {
                using (StreamWriter file = File.CreateText(@"C:\DNRLogs\LakesWithSurveys.json"))
                {
                    using (JsonTextWriter writer = new JsonTextWriter(file) { Formatting = Formatting.Indented })
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, countiesJsonOutput);
                    }
                }
            }
        }

    }
}
