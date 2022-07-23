using Newtonsoft.Json;

namespace DNRSurvey.Utilities
{
    public static class JSONHelper
    {
        public static T ConvertFromJson<T>(string filePathToJson)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePathToJson));
        }
    }
}
