namespace Services
{
    public static class DnrEndpoints
    {
        // get counties
        public const string GetCounties = "/cgi-bin/gazetteer2.cgi?type=county&callback=foo&_=15100205911351";

        public static string GetLakesWithinACounty(string countyId) => $"/cgi-bin/lakefinder_json.cgi?context=desktop&callback=foo&name=&county={countyId}&_=1510020650576";

        public static string GetLakesSurvey(string lakeId) => $"/cgi-bin/lakefinder/detail.cgi?type=lake_survey&callback=foo&id={lakeId}&_=1510019564259";
    }
}
