using System;
using System.IO;
using CommandLine;

namespace JsonConfigFlatter
{
    partial class Program
    {
        static void Main(string[] args) {
            try {
                Parser.Default.ParseArguments<FlatterOptions>(args)
                    .WithParsed<FlatterOptions>(JsonConfigFlatter.WriteFlattenedConfig);
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is FormatException) {
                WriteErrorToConsole(ex);
                Environment.Exit(-1);
            }
        }

        private static void WriteErrorToConsole(Exception ex) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }
}
