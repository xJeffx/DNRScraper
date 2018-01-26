using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceModel
{
    public class CountyWithLakesAndSurvey
    {
        public List<CountyLakesWithSurvey> CountyLakes { get; set; }
    }

    public class CountyLakesWithSurvey
    {
        public string CountyName { get; set; }
        public string CountyId { get; set; }

        public string LakeName { get; set; }
        public string LakeId { get; set; }
    }
}
