using CommandLine;

namespace JsonConfigFlatter {
    public class FlatterOptions {
        [Option('i', "input", Required = true, HelpText = "Set input json file.")]
        public string InputFile { get; set; }
        [Option('o', "output", Required = true, HelpText = "Set output json file.")]
        public string OutputFile { get; set; }
    }
}