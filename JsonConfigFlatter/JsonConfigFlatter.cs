using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace JsonConfigFlatter {
    public static class JsonConfigFlatter {
        public static void WriteFlattenedConfig(FlatterOptions flatterOptions) {
            var inputFilePath = System.IO.Path.GetFullPath(flatterOptions.InputFile, Environment.CurrentDirectory);
            var outputFilePath = System.IO.Path.GetFullPath(flatterOptions.OutputFile, Environment.CurrentDirectory);
            var config = new ConfigurationBuilder()
                .AddJsonFile(inputFilePath)
                .Build()
                .AsEnumerable()
                .Where(x => x.Value != null)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value);
            var flattenedConfig = JsonSerializer.Serialize(config, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true});
            File.WriteAllText(outputFilePath, flattenedConfig);
        }
    }
}