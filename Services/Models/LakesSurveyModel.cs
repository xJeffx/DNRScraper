
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
        public BKS BKS { get; set; }
        public BNM BNM { get; set; }
        public MTS MTS { get; set; }
        public GOS GOS { get; set; }
        public SHR SHR { get; set; }
        public TPM TPM { get; set; }
        public BLB BLB { get; set; }
        public LND LND { get; set; }
        public GSF GSF { get; set; }

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
    }

    public class SMB
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class WHB
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class RBT
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class BNT
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class SPT
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class LAT
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class LMB
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class TRP
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class YEP
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class BLG
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class WAE
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class NOP
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class LGP
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class BUB
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class MMS
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class TLC
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class RKB
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class SPO
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class YEB
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class JND
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class WTS
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class MUE
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class BLC
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class CAP
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class HSF
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class PMK
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class BOF
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class BRB
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class IOD
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class BKS
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class BNM
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class MTS
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class GOS
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class SHR
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class TPM
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class BLB
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class LND
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }

    public class GSF
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
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
