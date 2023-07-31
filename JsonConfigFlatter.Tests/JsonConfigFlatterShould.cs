using System;
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
                InputFile = "Samples/SampleConfig.json",
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

        [Fact]
        public void flatten_with_special_characters()
        {
            var options = new FlatterOptions()
            {
                InputFile = "Samples/ConfigWithSpecialCharacters.json",
                OutputFile = "FlattenedConfigWithSpecialCharacters.json"
            };

            JsonConfigFlatter.WriteFlattenedConfig(options);

            var jsonString = File.ReadAllText(options.OutputFile);
            jsonString.Should().NotContain("\\u003C\\u003E");
        }


        [Fact]
        public void throw_file_not_found_exception_with_non_existent_input_file() {
            var options = new FlatterOptions() {
                InputFile = "AnyNonExistentFile.json",
                OutputFile = "FlattenedConfig.json"
            };
            Action act = () => JsonConfigFlatter.WriteFlattenedConfig(options);

            act.Should().ThrowExactly<FileNotFoundException>();

        }

        [Fact]
        public void throw_format_exception_with_non_json_input_file() {
            var options = new FlatterOptions() {
                InputFile = "Samples/PlainTextfile.txt",
                OutputFile = "FlattenedConfig.json"
            };
            Action act = () => JsonConfigFlatter.WriteFlattenedConfig(options);

            act.Should().ThrowExactly<System.IO.InvalidDataException>();

        }
    }
}
