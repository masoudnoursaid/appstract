using System.Collections;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Sql.Configuration.Converter;

public class JsonValueConverter<T> : ValueConverter<T, string>
    where T : IEnumerable
{
    public JsonValueConverter(ConverterMappingHints? mappingHints = null) : base(
        input => JsonConvert.SerializeObject(input)
        , output => JsonConvert.DeserializeObject<T>(output)!
        , mappingHints)
    {
    }

    public JsonValueConverter(bool convertsNulls,
        ConverterMappingHints? mappingHints = null) : base(
        input => JsonConvert.SerializeObject(input)
        , output => JsonConvert.DeserializeObject<T>(output)!
        , convertsNulls
        , mappingHints)
    {
    }
}