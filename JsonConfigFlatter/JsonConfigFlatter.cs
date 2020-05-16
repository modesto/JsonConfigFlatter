using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace JsonConfigFlatter {
    public static class JsonConfigFlatter {
        public static void WriteFlattenedConfig(FlatterOptions flatterOptions) {
            var config = new ConfigurationBuilder()
                .AddJsonFile(flatterOptions.InputFile)
                .Build()
                .AsEnumerable()
                .Where(x => x.Value != null)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value);
            var flattenedConfig = JsonSerializer.Serialize(config, new JsonSerializerOptions() {WriteIndented = true});
            File.WriteAllText(flatterOptions.OutputFile, flattenedConfig);
        }
    }
}