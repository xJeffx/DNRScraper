
namespace DNRSurvey
{
    public static class Utilities
    {
        public static void CreateFilePathIfNotExist(string pathString)
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

    }
}
