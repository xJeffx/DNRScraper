
using DNRSurvey.Enums;
using Services.Models;

namespace DNRSurvey.Utilities
{
    public static class SurveyHelper
    {

        private static Species ConvertToSpeciesLength(FishSurvey survey, Enums.SupportedSpecies species)
        {
            //poor, just use a switch
            var speciesDict = new Dictionary<Enums.SupportedSpecies, Services.Models.Species>
            {
                { Enums.SupportedSpecies.SMB, survey.lengths.SMB},
                { Enums.SupportedSpecies.LMB, survey.lengths.LMB},
                { Enums.SupportedSpecies.TRP, survey.lengths.TRP},
                { Enums.SupportedSpecies.YEP, survey.lengths.YEP},
                { Enums.SupportedSpecies.BLG, survey.lengths.BLG},
                { Enums.SupportedSpecies.WAE, survey.lengths.WAE},
                { Enums.SupportedSpecies.NOP, survey.lengths.NOP},
                { Enums.SupportedSpecies.LGP, survey.lengths.LGP},
                { Enums.SupportedSpecies.BUB, survey.lengths.BUB},
                { Enums.SupportedSpecies.MMS, survey.lengths.MMS},
                { Enums.SupportedSpecies.TLC, survey.lengths.TLC},
                { Enums.SupportedSpecies.RKB, survey.lengths.RKB},
                { Enums.SupportedSpecies.SPO, survey.lengths.SPO},
                { Enums.SupportedSpecies.YEB, survey.lengths.YEB},
                { Enums.SupportedSpecies.JND, survey.lengths.JND},
                { Enums.SupportedSpecies.WTS, survey.lengths.WTS},
                { Enums.SupportedSpecies.MUE, survey.lengths.MUE},
                { Enums.SupportedSpecies.BLC, survey.lengths.BLC},
                { Enums.SupportedSpecies.CAP, survey.lengths.CAP},
                { Enums.SupportedSpecies.HSF, survey.lengths.HSF},
                { Enums.SupportedSpecies.PMK, survey.lengths.PMK},
                { Enums.SupportedSpecies.BOF, survey.lengths.BOF},
                { Enums.SupportedSpecies.BRB, survey.lengths.BRB},
                { Enums.SupportedSpecies.IOD, survey.lengths.IOD},
                { Enums.SupportedSpecies.BKT, survey.lengths.BKT},
                { Enums.SupportedSpecies.BNM, survey.lengths.BNM},
                { Enums.SupportedSpecies.MTS, survey.lengths.MTS},
                { Enums.SupportedSpecies.GOS, survey.lengths.GOS},
                { Enums.SupportedSpecies.SHR, survey.lengths.SHR},
                { Enums.SupportedSpecies.TPM, survey.lengths.TPM},
                { Enums.SupportedSpecies.BLB, survey.lengths.BLB},
                { Enums.SupportedSpecies.LND, survey.lengths.LND},
                { Enums.SupportedSpecies.GSF, survey.lengths.GSF},
                { Enums.SupportedSpecies.RBT, survey.lengths.RBT},
                { Enums.SupportedSpecies.BNT, survey.lengths.BNT},
                { Enums.SupportedSpecies.SPT, survey.lengths.SPT},
                { Enums.SupportedSpecies.LAT, survey.lengths.LAT},
                { Enums.SupportedSpecies.WHB, survey.lengths.WHB},
                { Enums.SupportedSpecies.LTS, survey.lengths.LTS},
                { Enums.SupportedSpecies.CCF, survey.lengths.CCF},
                { Enums.SupportedSpecies.WHC, survey.lengths.WHC},
                { Enums.SupportedSpecies.BIB, survey.lengths.BIB},
                { Enums.SupportedSpecies.TMUE, survey.lengths.TMUE},
                { Enums.SupportedSpecies.EMS, survey.lengths.EMS},
                { Enums.SupportedSpecies.LNG, survey.lengths.LNG},
                { Enums.SupportedSpecies.FCF, survey.lengths.FCF},
                { Enums.SupportedSpecies.SAR, survey.lengths.SAR},
                { Enums.SupportedSpecies.GIS, survey.lengths.GIS}
            };

            speciesDict.TryGetValue(species, out Species speciesLength);
            return speciesLength;
        } 

        public static Services.Models.Species GetSpeciesData(FishSurvey survey, Enums.SupportedSpecies speciesEnum)
        {
            // Get the species model from the speciesEnum value
            var speciesLength = ConvertToSpeciesLength(survey, speciesEnum);

            if(speciesLength == null)
            {
                throw new ArgumentException($"Found Survey but could not find length for {speciesEnum}");
            }

            return speciesLength;
        }

        public static FishSurvey GetLastestSurveyWithSpeciesLengthData(FishSurvey[] surveys, Enums.SupportedSpecies speciesEnum)
        {
            // Get the latest survey so we can see the latest information
            FishSurvey latestSurvey = null;
            if (surveys?.Length > 0)
            {           
                foreach (var currentSurvey in surveys)
                {
                    // Check if there are any lengths for the species
                    if (ConvertToSpeciesLength(currentSurvey, speciesEnum) == null)
                    {
                        continue;
                    }

                    latestSurvey ??= currentSurvey;

                    var oldSurveyDate = DateTime.Parse(latestSurvey.surveyDate);
                    var currentSurveyDate = DateTime.Parse(currentSurvey.surveyDate);

                    if (DateTime.Compare(oldSurveyDate, currentSurveyDate) <= 0)
                    {
                        latestSurvey = currentSurvey;
                    }
                }
            }

            return latestSurvey;
        }

        /// <summary>
        /// Get all the lengths available for a fish species in a survey
        /// </summary>
        /// <param name="fishLength">The length data</param>
        /// <returns></returns>
        public static Dictionary<int, int> GetSpeciesLengths(Services.Models.Species fishLength)
        {    
            //Key is the size in inches, value is the total number of fish of that size
            var lengthSizeToTotalFish = new Dictionary<int, int>();
                         
            //// Add up
            foreach (var length in fishLength.fishCount)
            {
                try
                {
                    var size = length[0];
                    if (lengthSizeToTotalFish.ContainsKey(size))
                    {
                        lengthSizeToTotalFish.TryGetValue(size, out int fishCount);
                        fishCount += length[1];
                        lengthSizeToTotalFish[size] = fishCount;

                    }
                    else
                    {
                        lengthSizeToTotalFish.Add(size, length[1]);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error processing fish count.");
                    Console.WriteLine($"Error {e.Message} - {e.StackTrace}");
                }
            }
            

            return lengthSizeToTotalFish;

        }
    }
}
