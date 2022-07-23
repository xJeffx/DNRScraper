using Newtonsoft.Json;

namespace DNRSurvey.Utilities
{
    public static class FileHelper
    {
        public static void CreateFilePathIfNotExist(string pathString)
        {
            Directory.CreateDirectory(pathString);
        }

        public static void CreateFile(string pathString)
        {
            Directory.CreateDirectory(pathString);
        }

        public static bool CheckForLakesWithSurveyDefaultLocaltion()
        {
            return File.Exists(@"C:\DNRResult\LakesWithSurveys.json");
        }

        public static bool FileExists(string pathString)
        {
            return File.Exists(pathString);
        }

        public static void WriteJSONToFile(object valueToWrite, string pathToSaveFile)
        {
            using (StreamWriter file = File.CreateText(pathToSaveFile))
            {
                using (JsonTextWriter writer = new JsonTextWriter(file) { Formatting = Formatting.Indented })
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, valueToWrite);
                }
            }
        }

        public static void WriteTextToFile(string valueToWrite, string pathToSaveFile)
        {

            using (StreamWriter file = File.CreateText(pathToSaveFile))
            {
                file.Write(valueToWrite);
            }
        }

    }
}
