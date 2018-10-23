using System.Globalization;
using Magenic.MaqsFramework.BaseWebServiceTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServiceModel;

namespace Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;

    /// <summary>
    /// Tests test class
    /// </summary>
    [TestClass]
    public class UsingJson : BaseWebServiceTest
    {
        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetBullheadLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.lengths != null && n.lengths.BLB != null);

                    if (survey != null)
                    {
                        var total = 0;
                        var totalList = new Dictionary<int, int>();

                        //// Add up
                        foreach (var length in survey.lengths.BLB.fishCount)
                        {
                            if (length[0] >= 0)
                            {
                                total = total + length[1];
                                totalList.Add(length[0], length[1]);
                            }
                        }

                        //// Print out
                        if (total > 0)
                        {
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Total Bullhead Greater Than 0 inches = '{total}' {System.Environment.NewLine}";

                            var sizes = "";
                            foreach (var size in totalList)
                            {
                                sizes =
                                    $"{sizes}Bullhead Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                            }

                            this.TestObject.Log.LogMessage($"{logString} {sizes}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetCrappieLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.lengths != null && n.lengths.BLC != null);

                    if (survey != null)
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
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Total Crappie Greater Than 8 inches = '{total}' {System.Environment.NewLine}";

                            var sizes = "";
                            foreach (var size in totalList)
                            {
                                sizes =
                                    $"{sizes}Crappie Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                            }

                            this.TestObject.Log.LogMessage($"{logString} {sizes}");
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetLakeTroutLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.lengths != null && n.lengths.LAT != null);

                    if (survey != null)
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
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Total Lake Trout Greater Than 8 inches = '{total}' {System.Environment.NewLine}";

                            var sizes = "";
                            foreach (var size in totalList)
                            {
                                sizes =
                                    $"{sizes}Lake Trout Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                            }

                            this.TestObject.Log.LogMessage($"{logString} {sizes}");
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetRainbowTroutLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.lengths != null && n.lengths.RBT != null);

                    if (survey != null)
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
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Total Rainbow Trout Greater Than 8 inches = '{total}' {System.Environment.NewLine}";

                            var sizes = "";
                            foreach (var size in totalList)
                            {
                                sizes =
                                    $"{sizes}Rainbow Trout Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                            }

                            this.TestObject.Log.LogMessage($"{logString} {sizes}");
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetSplakeLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.lengths != null && n.lengths.SPT != null);

                    if (survey != null)
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
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Total Splake Greater Than 8 inches = '{total}' {System.Environment.NewLine}";

                            var sizes = "";
                            foreach (var size in totalList)
                            {
                                sizes =
                                    $"{sizes}Splake Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                            }

                            this.TestObject.Log.LogMessage($"{logString} {sizes}");
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetBrownTroutLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.lengths != null && n.lengths.BNT != null);

                    if (survey != null)
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
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Total Brown Trout Greater Than 8 inches = '{total}' {System.Environment.NewLine}";

                            var sizes = "";
                            foreach (var size in totalList)
                            {
                                sizes =
                                    $"{sizes}Brown Trout Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                            }

                            this.TestObject.Log.LogMessage($"{logString} {sizes}");
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetBurbotLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.lengths != null && n.lengths.BUB != null);

                    if (survey != null)
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
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Total Burbot Greater Than 8 inches = '{total}' {System.Environment.NewLine}";

                            var sizes = "";
                            foreach (var size in totalList)
                            {
                                sizes =
                                    $"{sizes}Burbot Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                            }

                            this.TestObject.Log.LogMessage($"{logString} {sizes}");
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetWalleyeLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.lengths != null && n.lengths.WAE != null);

                    if (survey != null)
                    {
                        var total = 0;
                        var totalList = new Dictionary<int, int>();

                        //// Add up
                        foreach (var length in survey.lengths.WAE.fishCount)
                        {
                            if (length[0] >= 10)
                            {
                                total = total + length[1];
                                totalList.Add(length[0], length[1]);
                            }
                        }

                        //// Print out
                        if (total > 0)
                        {
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Total Walleye Greater Than 10 inches = '{total}' {System.Environment.NewLine}";

                            var sizes = "";
                            foreach (var size in totalList)
                            {
                                sizes =
                                    $"{sizes}Walleye Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                            }

                            this.TestObject.Log.LogMessage($"{logString} {sizes}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetPerchLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.lengths != null && n.lengths.YEP != null);

                    if (survey != null)
                    {

                        //// Check if lenghts exits and brown trout are available
                        var total = 0;
                        var totalList = new Dictionary<int, int>();

                        //// Add up
                        foreach (var length in survey.lengths.YEP.fishCount)
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
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Total Perch Greater Than 8 inches = '{total}' {System.Environment.NewLine}";

                            var sizes = "";
                            foreach (var size in totalList)
                            {
                                sizes =
                                    $"{sizes}Perch Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                            }

                            this.TestObject.Log.LogMessage($"{logString} {sizes}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetWhitebassLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.lengths != null && n.lengths.WHB != null);

                    if (survey != null)
                    {
                        var total = 0;
                        var totalList = new Dictionary<int, int>();

                        //// Add up
                        foreach (var length in survey.lengths.WHB.fishCount)
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
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Total WHB Greater Than 8 inches = '{total}' {System.Environment.NewLine}";

                            var sizes = "";
                            foreach (var size in totalList)
                            {
                                sizes =
                                    $"{sizes}WHB Size = '{size.Key}', Total = '{size.Value}' {System.Environment.NewLine}";
                            }

                            this.TestObject.Log.LogMessage($"{logString} {sizes}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetAnyPikeLastestLakesJSON()
        {
            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    var latestDate = new DateTime(1900, 1, 1);
                    Survey survey = null;
                    //// Specific Fish Code
                    foreach (var surv in lakesWithSurvey.result.surveys)
                    {
                        var date = DateTime.ParseExact(surv.surveyDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        if (date > latestDate)
                        {
                            latestDate = date;
                            survey = surv;
                        }
                    }
                    //var survey = lakesWithSurvey.result.surveys.LastOrDefault(n => n.fishCatchSummaries.Any(m => m.species.Equals("NOP", StringComparison.InvariantCultureIgnoreCase)));

                    if (survey != null)
                    {
                        //// Print out
                        var surveyString = $"https://www.dnr.state.mn.us/lakefind/showreport.html?downum={lake.LakeId}";
                        var logString =
                            $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                        logString =
                            $"{logString} Survey Link = '{surveyString}' {System.Environment.NewLine}";


                        this.TestObject.Log.LogMessage($"{logString}");
                        
                    }
                }
            }
        }

        /// <summary>
        /// Get single product as XML
        /// </summary>
        [TestMethod]
        public void GetAllKeyWorksNarrative()
        {
            //fertility, fertile, eutrophic, blooms

            CountyWithLakesAndSurvey countiesWithLakes =
                JsonConvert.DeserializeObject<CountyWithLakesAndSurvey>(
                    File.ReadAllText(@"C:\DNRLogs\LakesWithSurveys.json"));

            foreach (var lake in countiesWithLakes.CountyLakes)
            {
                //// Get all surveyes
                this.WebServiceWrapper = new HttpClientWrapper(new Uri("http://maps2.dnr.state.mn.us"));
                var lakeSurveyResult = this.WebServiceWrapper.Get(
                    $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lake.LakeId}&_=1510019564259",
                    "text/plain",
                    false);

                var lakeSurveyJsonString = lakeSurveyResult.Remove(0, 4);
                lakeSurveyJsonString = Regex.Replace(lakeSurveyJsonString, @"\t|\n|\r", "");
                lakeSurveyJsonString = lakeSurveyJsonString.Remove(lakeSurveyJsonString.Length - 1, 1);

                var lakesWithSurvey = JsonConvert.DeserializeObject<LakesSurveyModel>(lakeSurveyJsonString);

                if (lakesWithSurvey.result != null && lakesWithSurvey.result.surveys != null)
                {
                    //// Specific Fish Code
                    var survey = lakesWithSurvey.result.surveys.LastOrDefault();

                    if (survey != null)
                    {
                        if(survey.narrative.Contains("fertility") || survey.narrative.Contains("fertile") || survey.narrative.Contains("eutrophic") || survey.narrative.Contains("blooms"))
                        {
                            var clartiyString = "";

                            if (lakesWithSurvey.result.waterClarity != null && lakesWithSurvey.result.waterClarity.Length > 0)
                            {
                                var clarity = lakesWithSurvey.result.waterClarity.Last();

                                if (clarity != null)
                                {
                                    clartiyString = $"Date: {clarity[0].ToString()} Clarity: {clarity[1].ToString()}";
                                }
                            }                           

                            //// Print out
                            var logString =
                                $"{System.Environment.NewLine} Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}' {System.Environment.NewLine}";
                            logString =
                                $"{logString} Lake Clarity = '{clartiyString}' {System.Environment.NewLine}";

                            logString =
                                $"{logString} Narrative = '{survey.narrative}' {System.Environment.NewLine}";
                            
                            this.TestObject.Log.LogMessage($"{logString}");
                        }                                     
                    }
                }
            }
        }

    }
}
