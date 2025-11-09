using Domain.ValueObject;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Mongo.Serializers
{
    public class PlacaSerializer : SerializerBase<Placa>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Placa value)
        {
            context.Writer.WriteString(value.Valor);
        }

        public override Placa Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var stringValue = context.Reader.ReadString();
            return new Placa(stringValue);
        }
    }
}
