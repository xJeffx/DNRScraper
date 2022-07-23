
using DNRSurvey.Enums;

namespace DNRSurvey.Utilities
{
    public static class SpeciesHelper
    {

        private static readonly Dictionary<string, SpeciesEnum> speciesdict = new Dictionary<string, SpeciesEnum>
        {
            {"smallmouth bass", SpeciesEnum.SMB },
            {"largemouth bass", SpeciesEnum.LMB },
            {"trout-perch", SpeciesEnum.TRP },
            {"yellow perch", SpeciesEnum.YEP },
            {"bluegill", SpeciesEnum.BLG },
            {"walleye", SpeciesEnum.WAE },
            {"northern pike", SpeciesEnum.NOP },
            {"logperch", SpeciesEnum.LGP },
            {"burbot", SpeciesEnum.BUB },
            {"mimic shiner", SpeciesEnum.MMS },
            {"tulibee/cisco", SpeciesEnum.TLC },
            {"rock bass", SpeciesEnum.RKB },
            {"spottail shiner", SpeciesEnum.SPO },
            {"yellow bullhead", SpeciesEnum.YEB },
            {"johnny darter", SpeciesEnum.JND },
            {"white sucker", SpeciesEnum.WTS },
            {"muskellunge", SpeciesEnum.MUE },
            {"black crappie", SpeciesEnum.BLC },
            {"common carp", SpeciesEnum.CAP },
            {"hybrid sunfish", SpeciesEnum.HSF },
            {"pumpkinseed", SpeciesEnum.PMK },
            {"bowfin", SpeciesEnum.BOF },
            {"brown bullhead", SpeciesEnum.BRB },
            {"iowa darter", SpeciesEnum.IOD },
            {"brook trout", SpeciesEnum.BKT },
            {"bluntnose minnow", SpeciesEnum.BNM },
            {"mottled sculpin", SpeciesEnum.MTS },
            {"golden shiner", SpeciesEnum.GOS },
            {"shorthead redhorse", SpeciesEnum.SHR },
            {"tadpole madtom", SpeciesEnum.TPM },
            {"black bullhead", SpeciesEnum.BLB },
            {"longnose dace", SpeciesEnum.LND },
            {"green sunfish", SpeciesEnum.GSF },
            {"rainbow trout", SpeciesEnum.RBT },
            {"brown trout", SpeciesEnum.BNT },
            {"splake", SpeciesEnum.SPT },
            {"lake trout", SpeciesEnum.LAT },
            {"white bass", SpeciesEnum.WHB },
            {"lake sturgeon", SpeciesEnum.LTS },
            {"channel catfish", SpeciesEnum.CCF },
            {"white crappie", SpeciesEnum.WHC },
            {"bigmouth buffalo", SpeciesEnum.BIB },
            {"muskellunge hybrid", SpeciesEnum.TMUE },
            {"emerald shiner", SpeciesEnum.EMS },
            {"longnose gar", SpeciesEnum.LNG },
            {"flathead catfish", SpeciesEnum.FCF },
            {"sauger", SpeciesEnum.SAR },
            {"gizzard shad", SpeciesEnum.GIS }
        };

        public static SpeciesEnum GetSpeciesEnum(string speciesName)
        {
            try
            {
                speciesdict.TryGetValue(speciesName.ToLower(), out SpeciesEnum value);
                return value;
            } catch(Exception e)
            {
                Console.WriteLine($"Could not find species '{speciesName}'. Make sure you spelled it correctly and it is a support species");
                Console.WriteLine($"Eception - {e.Message}");

                throw;
            }           
        }       
    }
}
