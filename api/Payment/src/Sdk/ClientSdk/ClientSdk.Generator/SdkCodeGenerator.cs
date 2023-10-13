using System.Text.RegularExpressions;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.OperationNameGenerators;

namespace ClientSdk.Generator;

public class SdkCodeGenerator
{
    public async Task<string> Generate(string @namespace, string swaggerJson)
    {
        var document = await OpenApiDocument.FromJsonAsync(swaggerJson);
        CSharpClientGeneratorSettings settings = new()
        {
            CSharpGeneratorSettings = { Namespace = @namespace },
            OperationNameGenerator = new MultipleClientsFromFirstTagAndPathSegmentsOperationNameGenerator(),
            GenerateClientInterfaces = true,
            UseBaseUrl = false,
            DisposeHttpClient = false
        };

        CSharpClientGenerator generator = new(document, settings);
        var generatedSource = generator.GenerateFile();

        generatedSource = Regex.Replace(generatedSource, @"^\s*///.*$", string.Empty, RegexOptions.Multiline);
        generatedSource = Regex.Replace(generatedSource, @"^(?:[\t ]*(?:\r?\n|\r))+", "\n", RegexOptions.Multiline);
        return generatedSource;
    }
}