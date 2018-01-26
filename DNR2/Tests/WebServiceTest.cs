using Magenic.MaqsFramework.BaseWebServiceTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServiceModel;

namespace Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    /// <summary>
    /// Tests test class
    /// </summary>
    [TestClass]
    public class WebServiceTest : BaseWebServiceTest
    {
        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetCrappieLakes()
        {
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
                var lakesResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder_json.cgi?context=desktop&callback=foo&name=&county={county.id}&_=1510020650576", "text/plain", false);

                var lakesJsonString = lakesResult.Remove(0, 4);
                lakesJsonString = Regex.Replace(lakesJsonString, @"\t|\n|\r", "");
                lakesJsonString = lakesJsonString.Remove(lakesJsonString.Length - 2, 2);

                var lakesWithinCounty = JsonConvert.DeserializeObject<LakesModel>(lakesJsonString);

                foreach (var lake in lakesWithinCounty.results)
                {
                    //// Get all surveyes
                    this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                    var lakeSurveyResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.id}&_=1510019564259", "text/plain", false);

                    var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                    lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                    lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                    var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                    if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                    {
                        //// Specific Fish Code
                        foreach (var survey in lakesWithSurvey.result.surveys)
                        {
                            //// Check if lenghts exits and crappy are available
                            if (survey.lengths != null && survey.lengths.BLC != null)
                            {
                                var total = 0;
                                var totalList = new Dictionary<int, int>();

                                //// Add up
                                foreach (var length in survey.lengths.BLC.fishCount)
                                {
                                    if (length[0] >= 8)
                                    {
                                        total = total + length[1];
                                        totalList.Add(length[0], length[1]);
                                    }
                                }

                                //// Print out
                                if (total > 0)
                                {
                                    this.TestObject.Log.LogMessage($"County Name = '{county.county}', Lake Name = '{lake.name}'");
                                    this.TestObject.Log.LogMessage($"Total Crappie Greater Than 8 inches = '{total}'");

                                    foreach (var size in totalList)
                                    {
                                        this.TestObject.Log.LogMessage($"Crappie Size = '{size.Key}', Total = '{size.Value}'");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetCrappieLastestLakes()
        {
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
                var lakesResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder_json.cgi?context=desktop&callback=foo&name=&county={county.id}&_=1510020650576", "text/plain", false);

                var lakesJsonString = lakesResult.Remove(0, 4);
                lakesJsonString = Regex.Replace(lakesJsonString, @"\t|\n|\r", "");
                lakesJsonString = lakesJsonString.Remove(lakesJsonString.Length - 2, 2);

                var lakesWithinCounty = JsonConvert.DeserializeObject<LakesModel>(lakesJsonString);

                foreach (var lake in lakesWithinCounty.results)
                {
                    //// Get all surveyes
                    this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                    var lakeSurveyResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.id}&_=1510019564259", "text/plain", false);

                    var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                    lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                    lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                    var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                    if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                    {
                        //// Specific Fish Code
                        var survey = lakesWithSurvey.result.surveys.Last();
                        
                        //// Check if lenghts exits and crappy are available
                        if (survey.lengths != null && survey.lengths.BLC != null)
                        {
                            var total = 0;
                            var totalList = new Dictionary<int, int>();

                            //// Add up
                            foreach (var length in survey.lengths.BLC.fishCount)
                            {
                                if (length[0] >= 8)
                                {
                                    total = total + length[1];
                                    totalList.Add(length[0], length[1]);
                                }
                            }

                            //// Print out
                            if (total > 0)
                            {
                                var logString = $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{county.county}', Lake Name = '{lake.name}' {System.Environment.NewLine}";
                                logString = $"{logString} Total Crappie Greater Than 8 inches = '{total}' {System.Environment.NewLine}";
                                
                                var sizes = "";
                                foreach (var size in totalList)
                                {
                                    sizes = $"{sizes}Crappie Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                                }

                                this.TestObject.Log.LogMessage($"{logString} {sizes}");
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetLakeTroutLastestLakes()
        {
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
                var lakesResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder_json.cgi?context=desktop&callback=foo&name=&county={county.id}&_=1510020650576", "text/plain", false);

                var lakesJsonString = lakesResult.Remove(0, 4);
                lakesJsonString = Regex.Replace(lakesJsonString, @"\t|\n|\r", "");
                lakesJsonString = lakesJsonString.Remove(lakesJsonString.Length - 2, 2);

                var lakesWithinCounty = JsonConvert.DeserializeObject<LakesModel>(lakesJsonString);

                foreach (var lake in lakesWithinCounty.results)
                {
                    //// Get all surveyes
                    this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                    var lakeSurveyResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.id}&_=1510019564259", "text/plain", false);

                    var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                    lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                    lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                    var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                    if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                    {
                        //// Specific Fish Code
                        var survey = lakesWithSurvey.result.surveys.Last();

                        //// Check if lenghts exits and crappy are available
                        if (survey.lengths != null && survey.lengths.LAT != null)
                        {
                            var total = 0;
                            var totalList = new Dictionary<int, int>();

                            //// Add up
                            foreach (var length in survey.lengths.LAT.fishCount)
                            {
                                if (length[0] >= 8)
                                {
                                    total = total + length[1];
                                    totalList.Add(length[0], length[1]);
                                }
                            }

                            //// Print out
                            if (total > 0)
                            {
                                var logString = $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{county.county}', Lake Name = '{lake.name}' {System.Environment.NewLine}";
                                logString = $"{logString} Total Lake Trout Greater Than 8 inches = '{total}' {System.Environment.NewLine}";
                                
                                var sizes = "";
                                foreach (var size in totalList)
                                {
                                    sizes = $"{sizes}Lake Trout Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                                }

                                this.TestObject.Log.LogMessage($"{logString} {sizes}");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetRainbowTroutLastestLakes()
        {
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
                var lakesResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder_json.cgi?context=desktop&callback=foo&name=&county={county.id}&_=1510020650576", "text/plain", false);

                var lakesJsonString = lakesResult.Remove(0, 4);
                lakesJsonString = Regex.Replace(lakesJsonString, @"\t|\n|\r", "");
                lakesJsonString = lakesJsonString.Remove(lakesJsonString.Length - 2, 2);

                var lakesWithinCounty = JsonConvert.DeserializeObject<LakesModel>(lakesJsonString);

                foreach (var lake in lakesWithinCounty.results)
                {
                    //// Get all surveyes
                    this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                    var lakeSurveyResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.id}&_=1510019564259", "text/plain", false);

                    var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                    lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                    lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                    var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                    if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                    {
                        //// Specific Fish Code
                        var survey = lakesWithSurvey.result.surveys.Last();

                        //// Check if lenghts exits and crappy are available
                        if (survey.lengths != null && survey.lengths.RBT != null)
                        {
                            var total = 0;
                            var totalList = new Dictionary<int, int>();

                            //// Add up
                            foreach (var length in survey.lengths.RBT.fishCount)
                            {
                                if (length[0] >= 8)
                                {
                                    total = total + length[1];
                                    totalList.Add(length[0], length[1]);
                                }
                            }

                            //// Print out
                            if (total > 0)
                            {
                                var logString = $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{county.county}', Lake Name = '{lake.name}' {System.Environment.NewLine}";
                                logString = $"{logString} Total Rainbow Trout Greater Than 8 inches = '{total}' {System.Environment.NewLine}";
                                
                                var sizes = "";
                                foreach (var size in totalList)
                                {
                                    sizes = $"{sizes}Rainbow Trout Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                                }

                                this.TestObject.Log.LogMessage($"{logString} {sizes}");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetSplakeLastestLakes()
        {
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
                var lakesResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder_json.cgi?context=desktop&callback=foo&name=&county={county.id}&_=1510020650576", "text/plain", false);

                var lakesJsonString = lakesResult.Remove(0, 4);
                lakesJsonString = Regex.Replace(lakesJsonString, @"\t|\n|\r", "");
                lakesJsonString = lakesJsonString.Remove(lakesJsonString.Length - 2, 2);

                var lakesWithinCounty = JsonConvert.DeserializeObject<LakesModel>(lakesJsonString);

                foreach (var lake in lakesWithinCounty.results)
                {
                    //// Get all surveyes
                    this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                    var lakeSurveyResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.id}&_=1510019564259", "text/plain", false);

                    var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                    lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                    lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                    var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                    if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                    {
                        //// Specific Fish Code
                        var survey = lakesWithSurvey.result.surveys.Last();

                        //// Check if lenghts exits and crappy are available
                        if (survey.lengths != null && survey.lengths.SPT != null)
                        {
                            var total = 0;
                            var totalList = new Dictionary<int, int>();

                            //// Add up
                            foreach (var length in survey.lengths.SPT.fishCount)
                            {
                                if (length[0] >= 8)
                                {
                                    total = total + length[1];
                                    totalList.Add(length[0], length[1]);
                                }
                            }

                            //// Print out
                            if (total > 0)
                            {
                                var logString = $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{county.county}', Lake Name = '{lake.name}' {System.Environment.NewLine}";
                                logString = $"{logString} Total Splake Greater Than 8 inches = '{total}' {System.Environment.NewLine}";
                                
                                var sizes = "";
                                foreach (var size in totalList)
                                {
                                    sizes = $"{sizes}Splake Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                                }

                                this.TestObject.Log.LogMessage($"{logString} {sizes}");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetBrownTroutLastestLakes()
        {
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
                var lakesResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder_json.cgi?context=desktop&callback=foo&name=&county={county.id}&_=1510020650576", "text/plain", false);

                var lakesJsonString = lakesResult.Remove(0, 4);
                lakesJsonString = Regex.Replace(lakesJsonString, @"\t|\n|\r", "");
                lakesJsonString = lakesJsonString.Remove(lakesJsonString.Length - 2, 2);

                var lakesWithinCounty = JsonConvert.DeserializeObject<LakesModel>(lakesJsonString);

                foreach (var lake in lakesWithinCounty.results)
                {
                    //// Get all surveyes
                    this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                    var lakeSurveyResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.id}&_=1510019564259", "text/plain", false);

                    var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                    lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                    lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                    var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                    if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                    {
                        //// Specific Fish Code
                        var survey = lakesWithSurvey.result.surveys.Last();

                        //// Check if lenghts exits and brown trout are available
                        if (survey.lengths != null && survey.lengths.BNT != null)
                        {
                            var total = 0;
                            var totalList = new Dictionary<int, int>();

                            //// Add up
                            foreach (var length in survey.lengths.BNT.fishCount)
                            {
                                if (length[0] >= 8)
                                {
                                    total = total + length[1];
                                    totalList.Add(length[0], length[1]);
                                }
                            }

                            //// Print out
                            if (total > 0)
                            {
                                var logString = $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{county.county}', Lake Name = '{lake.name}' {System.Environment.NewLine}";
                                logString = $"{logString} Total Brown Trout Greater Than 8 inches = '{total}' {System.Environment.NewLine}";
                                
                                var sizes = "";
                                foreach (var size in totalList)
                                {
                                    sizes = $"{sizes}Brown Trout Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                                }

                                this.TestObject.Log.LogMessage($"{logString} {sizes}");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetBurbotLastestLakes()
        {
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
                var lakesResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder_json.cgi?context=desktop&callback=foo&name=&county={county.id}&_=1510020650576", "text/plain", false);

                var lakesJsonString = lakesResult.Remove(0, 4);
                lakesJsonString = Regex.Replace(lakesJsonString, @"\t|\n|\r", "");
                lakesJsonString = lakesJsonString.Remove(lakesJsonString.Length - 2, 2);

                var lakesWithinCounty = JsonConvert.DeserializeObject<LakesModel>(lakesJsonString);

                foreach (var lake in lakesWithinCounty.results)
                {
                    //// Get all surveyes
                    this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                    var lakeSurveyResult = this.WebServiceWrapper.Get($"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.id}&_=1510019564259", "text/plain", false);

                    var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                    lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                    lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                    var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                    if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                    {
                        //// Specific Fish Code
                        var survey = lakesWithSurvey.result.surveys.Last();

                        //// Check if lenghts exits and brown trout are available
                        if (survey.lengths != null && survey.lengths.BUB != null)
                        {
                            var total = 0;
                            var totalList = new Dictionary<int, int>();

                            //// Add up
                            foreach (var length in survey.lengths.BUB.fishCount)
                            {
                                if (length[0] >= 8)
                                {
                                    total = total + length[1];
                                    totalList.Add(length[0], length[1]);
                                }
                            }

                            //// Print out
                            if (total > 0)
                            {
                                var logString = $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{county.county}', Lake Name = '{lake.name}' {System.Environment.NewLine}";
                                logString = $"{logString} Total Burbot Greater Than 8 inches = '{total}' {System.Environment.NewLine}";

                                var sizes = "";
                                foreach (var size in totalList)
                                {
                                    sizes = $"{sizes}Burbot Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                                }

                                this.TestObject.Log.LogMessage($"{logString} {sizes}");
                            }
                        }
                    }
                }
            }
        }



















    }
}
