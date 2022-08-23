
using DNRSurvey.Enums;

namespace DNRSurvey.Utilities
{
    public static class SpeciesHelper
    {

        private static readonly Dictionary<string, SupportedSpecies> speciesdict = new()
        {
            {"smallmouth bass", SupportedSpecies.SMB },
            {"largemouth bass", SupportedSpecies.LMB },
            {"trout-perch", SupportedSpecies.TRP },
            {"yellow perch", SupportedSpecies.YEP },
            {"bluegill", SupportedSpecies.BLG },
            {"walleye", SupportedSpecies.WAE },
            {"northern pike", SupportedSpecies.NOP },
            {"logperch", SupportedSpecies.LGP },
            {"burbot", SupportedSpecies.BUB },
            {"mimic shiner", SupportedSpecies.MMS },
            {"tulibee/cisco", SupportedSpecies.TLC },
            {"rock bass", SupportedSpecies.RKB },
            {"spottail shiner", SupportedSpecies.SPO },
            {"yellow bullhead", SupportedSpecies.YEB },
            {"johnny darter", SupportedSpecies.JND },
            {"white sucker", SupportedSpecies.WTS },
            {"muskellunge", SupportedSpecies.MUE },
            {"black crappie", SupportedSpecies.BLC },
            {"common carp", SupportedSpecies.CAP },
            {"hybrid sunfish", SupportedSpecies.HSF },
            {"pumpkinseed", SupportedSpecies.PMK },
            {"bowfin", SupportedSpecies.BOF },
            {"brown bullhead", SupportedSpecies.BRB },
            {"iowa darter", SupportedSpecies.IOD },
            {"brook trout", SupportedSpecies.BKT },
            {"bluntnose minnow", SupportedSpecies.BNM },
            {"mottled sculpin", SupportedSpecies.MTS },
            {"golden shiner", SupportedSpecies.GOS },
            {"shorthead redhorse", SupportedSpecies.SHR },
            {"tadpole madtom", SupportedSpecies.TPM },
            {"black bullhead", SupportedSpecies.BLB },
            {"longnose dace", SupportedSpecies.LND },
            {"green sunfish", SupportedSpecies.GSF },
            {"rainbow trout", SupportedSpecies.RBT },
            {"brown trout", SupportedSpecies.BNT },
            {"splake", SupportedSpecies.SPT },
            {"lake trout", SupportedSpecies.LAT },
            {"white bass", SupportedSpecies.WHB },
            {"lake sturgeon", SupportedSpecies.LTS },
            {"channel catfish", SupportedSpecies.CCF },
            {"white crappie", SupportedSpecies.WHC },
            {"bigmouth buffalo", SupportedSpecies.BIB },
            {"muskellunge hybrid", SupportedSpecies.TMUE },
            {"emerald shiner", SupportedSpecies.EMS },
            {"longnose gar", SupportedSpecies.LNG },
            {"flathead catfish", SupportedSpecies.FCF },
            {"sauger", SupportedSpecies.SAR },
            {"gizzard shad", SupportedSpecies.GIS }
        };

        public static SupportedSpecies GetSpeciesEnum(string speciesName)
        {
            try
            {
                speciesdict.TryGetValue(speciesName.ToLower(), out SupportedSpecies value);
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
