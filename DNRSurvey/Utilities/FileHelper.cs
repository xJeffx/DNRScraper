using Newtonsoft.Json;

namespace DNRSurvey.Utilities
{
    public static class FileHelper
    {
        public static void CreateFilePathIfNotExist(string pathString)
        {
            Directory.CreateDirectory(pathString);
        }

        public static bool FileExists(string pathString)
        {
            return File.Exists(pathString);
        }

        public static void WriteJSONToFile(object valueToWrite, string pathToSaveFile)
        {
            using StreamWriter file = File.CreateText(pathToSaveFile);
            using JsonTextWriter writer = new(file) { Formatting = Formatting.Indented };
            JsonSerializer serializer = new();
            serializer.Serialize(writer, valueToWrite);
        }

        public static void WriteTextToFile(string valueToWrite, string pathToSaveFile)
        {

            using StreamWriter file = File.CreateText(pathToSaveFile);
            file.Write(valueToWrite);
        }

    }
}
