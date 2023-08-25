using System;

public class ArgumentParser
{
    public struct Result
    {
        public bool EmptyArgument {get; set;}
        public bool UseCustomConfigFile {get; set;}
        public string ConfigJsonFilePath {get; set;}
    }


    public static Result ParseArgument(string[] args)
    {
        Result result = new()
        {
            EmptyArgument = args.Length == 0,
        };
        (result.UseCustomConfigFile, result.ConfigJsonFilePath) = ParseConfigFileCLIArgument(args);


        return result;
    }
    
    private static (bool, string) ParseConfigFileCLIArgument(string[] args)
    {
        bool useCustomConfigFile = 
            args.Contains("--custom-json-file") || args.Contains("-c");
        var position = 
            Array.IndexOf(args, "--custom-json-file");
        position = 
            position == -1? Array.IndexOf(args, "-c") : position;
        if (position == -1)
            throw new ArgumentException("Error: contains \"-c\" or \"--custom-json-file\" but didn't gave a filename after it");
        return (useCustomConfigFile, args[position + 1]);
    }
}