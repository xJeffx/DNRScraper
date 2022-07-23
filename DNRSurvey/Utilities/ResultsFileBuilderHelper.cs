using Services.Models;
using System.Text;

namespace DNRSurvey.Utilities
{
    public class ResultsFileBuilderHelper
    {
        private StringBuilder Builder { get; set; } = new StringBuilder();

        public ResultsFileBuilderHelper AddToFile(string lineOfText)
        {
            Builder.AppendLine(lineOfText);
            return this;
        }

        public string Build()
        {
            return Builder.ToString();
        }

        // Add the lengths to the file
        public ResultsFileBuilderHelper AddSizeInformation(Dictionary<int, int> lengthData, int minFishSize = 0)
        {
            var totalFishAboveMinSizeList = lengthData.Where(sizes => sizes.Key >= minFishSize).ToList();
            totalFishAboveMinSizeList.OrderBy(key => key.Key);
            AddToFile($"Total Fish Greater Than {minFishSize} inches = '{totalFishAboveMinSizeList.Count}'");

            foreach (var size in totalFishAboveMinSizeList)
            {
                AddToFile($"Size = '{size.Key}', Total = '{size.Value}'");
            }

            return this;
        }

        // Add the survey information
        public ResultsFileBuilderHelper AddSurveyInformation(FishSurvey survey, CountyLake lake)
        {
            AddToFile($"Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}'");
            AddToFile($"LakeFinder URL = https://www.dnr.state.mn.us/lakefind/lake.html?id={lake.LakeId}");

            return this;
        }

        public ResultsFileBuilderHelper AddSpeciesInformation(string speciesName)
        {
            AddToFile($" ");
            AddToFile($"Survey Species = '{speciesName}'");
            return this;
        }
    }
}
