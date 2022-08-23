
using DNRSurvey.Enums;
using Services.Models;

namespace DNRSurvey.Utilities
{
    public static class SurveyHelper
    {

        private static Species ConvertToSpeciesLength(FishSurvey survey, SpeciesEnum species)
        {
            //poor, just use a switch
            var speciesDict = new Dictionary<SpeciesEnum, Species>
            {
                {SpeciesEnum.SMB, survey.lengths.SMB},
                {SpeciesEnum.LMB, survey.lengths.LMB},
                {SpeciesEnum.TRP, survey.lengths.TRP},
                {SpeciesEnum.YEP, survey.lengths.YEP},
                {SpeciesEnum.BLG, survey.lengths.BLG},
                {SpeciesEnum.WAE, survey.lengths.WAE},
                {SpeciesEnum.NOP, survey.lengths.NOP},
                {SpeciesEnum.LGP, survey.lengths.LGP},
                {SpeciesEnum.BUB, survey.lengths.BUB},
                {SpeciesEnum.MMS, survey.lengths.MMS},
                {SpeciesEnum.TLC, survey.lengths.TLC},
                {SpeciesEnum.RKB, survey.lengths.RKB},
                {SpeciesEnum.SPO, survey.lengths.SPO},
                {SpeciesEnum.YEB, survey.lengths.YEB},
                {SpeciesEnum.JND, survey.lengths.JND},
                {SpeciesEnum.WTS, survey.lengths.WTS},
                {SpeciesEnum.MUE, survey.lengths.MUE},
                {SpeciesEnum.BLC, survey.lengths.BLC},
                {SpeciesEnum.CAP, survey.lengths.CAP},
                {SpeciesEnum.HSF, survey.lengths.HSF},
                {SpeciesEnum.PMK, survey.lengths.PMK},
                {SpeciesEnum.BOF, survey.lengths.BOF},
                {SpeciesEnum.BRB, survey.lengths.BRB},
                {SpeciesEnum.IOD, survey.lengths.IOD},
                {SpeciesEnum.BKT, survey.lengths.BKT},
                {SpeciesEnum.BNM, survey.lengths.BNM},
                {SpeciesEnum.MTS, survey.lengths.MTS},
                {SpeciesEnum.GOS, survey.lengths.GOS},
                {SpeciesEnum.SHR, survey.lengths.SHR},
                {SpeciesEnum.TPM, survey.lengths.TPM},
                {SpeciesEnum.BLB, survey.lengths.BLB},
                {SpeciesEnum.LND, survey.lengths.LND},
                {SpeciesEnum.GSF, survey.lengths.GSF},
                {SpeciesEnum.RBT, survey.lengths.RBT},
                {SpeciesEnum.BNT, survey.lengths.BNT},
                {SpeciesEnum.SPT, survey.lengths.SPT},
                {SpeciesEnum.LAT, survey.lengths.LAT},
                {SpeciesEnum.WHB, survey.lengths.WHB},
                {SpeciesEnum.LTS, survey.lengths.LTS},
                {SpeciesEnum.CCF, survey.lengths.CCF},
                {SpeciesEnum.WHC, survey.lengths.WHC},
                {SpeciesEnum.BIB, survey.lengths.BIB},
                {SpeciesEnum.TMUE, survey.lengths.TMUE},
                {SpeciesEnum.EMS,survey.lengths.EMS},
                {SpeciesEnum.LNG,survey.lengths.LNG},
                {SpeciesEnum.FCF,survey.lengths.FCF},
                {SpeciesEnum.SAR,survey.lengths.SAR},
                {SpeciesEnum.GIS,survey.lengths.GIS}
            };

            speciesDict.TryGetValue(species, out Species speciesLength);
            return speciesLength;
        } 

        public static Species GetSpeciesData(FishSurvey survey, SpeciesEnum speciesEnum)
        {
            // Get the species model from the speciesEnum value
            var speciesLength = ConvertToSpeciesLength(survey, speciesEnum);

            if(speciesLength == null)
            {
                throw new Exception($"Found Survey but could not find length for {speciesEnum.ToString()}");
            }

            return speciesLength;
        }

        public static FishSurvey GetLastestSurveyWithSpeciesLengthData(FishSurvey[] surveys, SpeciesEnum speciesEnum)
        {
            // Get the latest survey so we can see the latest information
            FishSurvey latestSurvey = null;
            if (surveys?.Count() > 0)
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
        public static Dictionary<int, int> GetSpeciesLengths(Species fishLength)
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
                        fishCount = fishCount + length[1];
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
