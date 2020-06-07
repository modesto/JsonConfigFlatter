# JsonConfigFlatter
JSON config flatter is a tool to transform JSON configuration files with nested keys into a key-value JSON file compatible with Azure App Configuration CLI.

As an example, a JSON file like this:

```JSON
{
  "sampleKey": "sampleValue",
  "sampleObject": {
    "sampleObjectKey": "sampleObject value",
    "sampleObjectInsideObject": {
      "nestedKeyInsideNestedObject":  "renested value"
    }
  },
  "sampleArray": [
    "arrayItem1",
    "arrayItem2",
    "arrayItem3"
  ]
}
```

Will be converted into this:

```JSON
{
    "sampleKey": "sampleValue",
    "sampleObject:sampleObjectKey": "sampleObject value",
    "sampleObject:sampleObjectInsideObject:nestedKeyInsideNestedObject":  "renested value",
    "sampleArray:0" : "arrayItem1",
    "sampleArray:1" : "arrayItem2",
    "sampleArray:2" : "arrayItem3",
}
```

## Installation

```
dotnet tool install --global JsonConfigFlatter
```

## Usage

```
dotnet flattenconfig --input anyinputfile.json --output anyoutputfile.json
```
