using System.Text.Json;
using System.Text.Json.Serialization;

namespace HackerNews.Api.Services;

internal class UnixEpochMilliSecondConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        long value;


        value = reader.GetInt64();

        if (value >= 0)
        {
            DateTimeOffset d = DateTimeOffset.UnixEpoch.AddSeconds(value);


            return d;
        }
        else
        {
            throw new Exception("Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970}");
        }
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
