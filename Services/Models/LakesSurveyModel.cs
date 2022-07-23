
using Newtonsoft.Json;

namespace Services.Models
{
    // This class represents the lake survey data
    public class LakesSurveyModel
    {
        public int timestamp { get; set; }
        public string status { get; set; }

        [JsonProperty(PropertyName = "result")]
        public LakeSurveyData result { get; set; }
        public string message { get; set; }
    }

    public class LakeSurveyData
    {
        public string averageWaterClarity { get; set; }
        public object[] sampledPlants { get; set; }
        public string officeCode { get; set; }
        public float littoralAcres { get; set; }
        public float shoreLengthMiles { get; set; }
        public float areaAcres { get; set; }
        public FishSurvey[] surveys { get; set; }
        public Access[] accesses { get; set; }
        public string lakeName { get; set; }
        public string DOWNumber { get; set; }
        public string[][] waterClarity { get; set; }
        public float meanDepthFeet { get; set; }
        public float maxDepthFeet { get; set; }
    }

    public class FishSurvey
    {
        public Fishcatchsummary[] fishCatchSummaries { get; set; }
        public string surveyDate { get; set; }
        public string surveySubType { get; set; }
        public string[] headerInfo { get; set; }
        public string surveyType { get; set; }
        public string narrative { get; set; }
        public string surveyID { get; set; }
        public Lengths lengths { get; set; }
    }

    public class Lengths
    {
        public SMB SMB { get; set; }
        public LMB LMB { get; set; }
        public TRP TRP { get; set; }
        public YEP YEP { get; set; }
        public BLG BLG { get; set; }
        public WAE WAE { get; set; }
        public NOP NOP { get; set; }
        public LGP LGP { get; set; }
        public BUB BUB { get; set; }
        public MMS MMS { get; set; }
        public TLC TLC { get; set; }
        public RKB RKB { get; set; }
        public SPO SPO { get; set; }
        public YEB YEB { get; set; }
        public JND JND { get; set; }
        public WTS WTS { get; set; }
        public MUE MUE { get; set; }
        public BLC BLC { get; set; }
        public CAP CAP { get; set; }
        public HSF HSF { get; set; }
        public PMK PMK { get; set; }
        public BOF BOF { get; set; }
        public BRB BRB { get; set; }
        public IOD IOD { get; set; }
        public BKT BKT { get; set; }
        public BNM BNM { get; set; }
        public MTS MTS { get; set; }
        public GOS GOS { get; set; }
        public SHR SHR { get; set; }
        public TPM TPM { get; set; }
        public BLB BLB { get; set; }
        public LND LND { get; set; }
        public GSF GSF { get; set; }

        public LTS LTS { get; set; }        

        /// <summary>
        /// Rainbow Trout
        /// </summary>
        public RBT RBT { get; set; }

        /// <summary>
        /// Brown Trout
        /// </summary>
        public BNT BNT { get; set; }

        /// <summary>
        /// Splake
        /// </summary>
        public SPT SPT { get; set; }

        /// <summary>
        /// Lake Trout
        /// </summary>
        public LAT LAT { get; set; }

        /// <summary>
        /// Lake Trout
        /// </summary>
        public WHB WHB { get; set; }

        public CCF CCF { get; set; }

        public WHC WHC { get; set; }
        public BIB BIB { get; set; }

        public TMUE TMUE { get; set; }
        public EMS EMS { get; set; }
        public LNG LNG { get; set; }
        public FCF FCF { get; set; }

        public SAR SAR { get; set; }

        public GIS GIS { get; set; }
        public HBS HBS { get; set; }

    }
    public class GIS : Species
    {
    }
    public class SAR : Species
    {
    }

    public class FCF : Species
    {
    }

    public class LNG : Species
    {
    }
    public class EMS : Species
    {
    }


    public class TMUE : Species
    {
    }

    public class BIB : Species
    {
    }

    public class WHC : Species
    {
    }

    public class CCF : Species
    {
    }

    public class SMB : Species
    {
    }
    public class HBS : Species
    {
    }
    

    public class WHB : Species
    {
    }

    public class RBT : Species
    {
    }

    public class LTS : Species
    { 
    }

    public class BNT : Species
    {
    }

    public class SPT : Species
    {
    }

    public class LAT : Species
    {
    }

    public class LMB : Species
    {
    }

    public class TRP : Species
    {
    }

    public class YEP : Species
    {
    }

    public class BLG : Species
    {
    }

    public class WAE : Species
    {
    }

    public class NOP : Species
    {
    }

    public class LGP : Species
    {
    }

    public class BUB : Species
    {
    }

    public class MMS : Species
    {
    }

    public class TLC : Species
    {
    }

    public class RKB : Species
    {
    }

    public class SPO : Species
    {
    }

    public class YEB : Species
    {
    }

    public class JND : Species
    {
    }

    public class WTS : Species
    {
    }

    public class MUE : Species
    {
    }

    public class BLC : Species
    {
    }

    public class CAP : Species
    {
    }

    public class HSF : Species
    {
    }

    public class PMK : Species
    {
    }

    public class BOF : Species
    {
    }

    public class BRB : Species
    {
    }

    public class IOD : Species
    {
    }

    public class BKT : Species
    {
    }

    public class BNM : Species
    {
    }

    public class MTS : Species
    {
    }

    public class GOS : Species
    {
    }

    public class SHR : Species
    {
    }

    public class TPM : Species
    {
    }

    public class BLB : Species
    {
    }

    public class LND : Species
    {
    }

    public class GSF : Species
    {
    }

    public class Fishcatchsummary
    {
        public string quartileCount { get; set; }
        public string CPUE { get; set; }
        public int totalCatch { get; set; }
        public string species { get; set; }
        public float totalWeight { get; set; }
        public string quartileWeight { get; set; }
        public string averageWeight { get; set; }
        public float gearCount { get; set; }
        public string gear { get; set; }
    }

    public class Access
    {
        public string accessTypeId { get; set; }
        public string location { get; set; }
        public string publicUseAuthCode { get; set; }
        public string ownerTypeId { get; set; }
        public string lakeAccessComments { get; set; }
    }
}
