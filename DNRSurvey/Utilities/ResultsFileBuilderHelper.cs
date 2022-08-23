using Services.Models;
using System.Text;

namespace DNRSurvey.Utilities
{
    public class ResultsFileBuilderHelper
    {
        private StringBuilder Builder { get; set; } = new StringBuilder();

        public ResultsFileBuilderHelper Append(string lineOfText)
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
            var totalFishAboveMinSizeList = lengthData.Where(sizes => sizes.Key >= minFishSize).OrderBy(key => key.Key).ToList();
          
            Append($"Total Fish Greater Than {minFishSize} inches = '{totalFishAboveMinSizeList.Count}'");

            foreach (var size in totalFishAboveMinSizeList)
            {
                Append($"Size = '{size.Key}', Total = '{size.Value}'");
            }

            return this;
        }

        // Add the survey information
        public ResultsFileBuilderHelper AddSurveyInformation(FishSurvey survey, CountyLake lake)
        {
            Append($"Survey Date = '{survey.surveyDate}',  County Name = '{lake.CountyName}', Lake Name = '{lake.LakeName}'");
            Append($"LakeFinder URL = https://www.dnr.state.mn.us/lakefind/lake.html?id={lake.LakeId}");

            return this;
        }

        public ResultsFileBuilderHelper AddSpeciesInformation(string speciesName)
        {
            Append($" ");
            Append($"Survey Species = '{speciesName}'");
            return this;
        }
    }
}
