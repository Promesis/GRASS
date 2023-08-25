using System;
using System.Collections.Generic;
using System.Text.Json;

namespace GRASS
{
    public class GRASS 
    {
        private static JsonElement configObject;
        public static void Main(string [] args)
        {
            ArgumentParser.Result result = ArgumentParser.ParseArgument(args);
            InitializeWith(result);
        }

        private static void InitializeWith(ArgumentParser.Result result)
        {
            if (!result.UseCustomConfigFile)
            {
                if (!ExistsDefaultConfigFile())
                    CreateNewDefaultConfigFileAndReadIt();
            }
            else
            {
                if (!ExistsCustomConfigFilePathInArgumentParserResult(result))
                    throw new ArgumentException($"doesn't exists custom config file with path {result.ConfigJsonFilePath}");
                ReadCustomConfigFilePathInArgumentParserResult(result);
            }
        }

        private static bool ExistsDefaultConfigFile()
        {
            return File.Exists("./config.json");
        }

        private static void CreateNewDefaultConfigFileAndReadIt()
        {
            if (ExistsDefaultConfigFile())
                throw new ArgumentException("Cannot handle with a default config file exists");
            File.Create("./config.json");

            ReadConfigFilePathIfItExistsInPath("./config.json");
        }

        private static bool ExistsCustomConfigFilePathInArgumentParserResult(ArgumentParser.Result result)
        {
            return File.Exists(result.ConfigJsonFilePath);
        }

        private static void ReadCustomConfigFilePathInArgumentParserResult(ArgumentParser.Result result)
        {
            ReadConfigFilePathIfItExistsInPath(result.ConfigJsonFilePath);
        }

        private static void ReadConfigFilePathIfItExistsInPath(string path)
        {
            using JsonDocument configFileObject = JsonDocument.Parse(File.ReadAllText("./config.json"));
            configObject = configFileObject.RootElement;
        }
    }
}