using CommandLine;

namespace JsonConfigFlatter
{
    partial class Program
    {
        static void Main(string[] args) {
            Parser.Default.ParseArguments<FlatterOptions>(args).WithParsed<FlatterOptions>(JsonConfigFlatter.WriteFlattenedConfig);
        }
    }
}
