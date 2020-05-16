using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FluentAssertions;
using Xunit;

namespace JsonConfigFlatter.Tests
{
    public class JsonConfigFlatterShould
    {
        [Fact]
        public void flatten_json_file()
        {
            var options = new FlatterOptions() {
                InputFile = "SampleConfig.json",
                OutputFile = "FlattenedConfig.json"
            };

            JsonConfigFlatter.WriteFlattenedConfig(options);

            var jsonString = File.ReadAllText(options.OutputFile);
            var output = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            output["sampleKey"].Should().Be("sampleValue");
            output["sampleObject:sampleObjectKey"].Should().Be("sampleObject value");
            output["sampleObject:sampleObjectInsideObject:nestedKeyInsideNestedObject"].Should().Be("renested value");
            output["sampleArray:0"].Should().Be("arrayItem1");
            output["sampleArray:1"].Should().Be("arrayItem2");
            output["sampleArray:2"].Should().Be("arrayItem3");
        }
    }
}
