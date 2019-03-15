using FilmCatalogue.Domain.DataTypes.Common;
using Newtonsoft.Json;
using System;

namespace FilmCatalogue.Api.Web.Rest.Converters
{
    public class IdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
            => objectType == typeof(Id);

        // this converter is only used for serialization, not to deserialize
        public override bool CanRead => false;

        // implement this if you need to read the string representation to create an AccountId
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            => throw new NotImplementedException();

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Id accountId)
            {
                writer.WriteValue((Guid)accountId);
            }
            else
            {
                throw new JsonSerializationException("Expected AccountId object value.");
            }
        }
    }
}
