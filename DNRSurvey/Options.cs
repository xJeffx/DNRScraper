using CommandLine;
using CommandLine.Text;

namespace DNRSurvey
{
    public class Options
    {
        [Option('l', "lakeswithsurveypath", Required = false, HelpText = "File path to json file containing lakes with surveys")]
        public string? LakesWithSurveyPath { get; set; }

        [Option('s', "species", Required = true, HelpText = "The fish species to look for")]
        public string? Species { get; set; }

        [Option('m', "min", Required = false, HelpText = "Minimum size of the fish species")]
        public string? MinSize { get; set; }

        [Option('p', "savepath", Required = false, HelpText = "Path to save the log")]
        public string? SavePath { get; set; }

        [Option('c', "county", Required = false, HelpText = "Focus on one county")]
        public IEnumerable<string>? County { get; set; }

        [Usage(ApplicationAlias = "DNRSurvey")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>() {
                new Example("Generate a text file of all lakes with surveys for a fish species. Takes the longest", new Options { Species = "largemouth bass" })
      };
            }
        }

    }
}
