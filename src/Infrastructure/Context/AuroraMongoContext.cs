using Domain.Entity;
using Infrastructure.Mongo.Serializers;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infrastructure.Context
{
    public class AuroraMongoContext
    {
        private readonly IMongoDatabase _database;

        static AuroraMongoContext()
        {
            BsonSerializer.RegisterSerializer(typeof(Domain.ValueObject.Placa), new PlacaSerializer());
        }

        public AuroraMongoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("AuroraTraceDb");
        }

        public AuroraMongoContext(MongoClient client, string databaseName)
        {
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Moto> Motos => _database.GetCollection<Moto>("Motos");
        public IMongoCollection<Patio> Patios => _database.GetCollection<Patio>("Patios");
        public IMongoCollection<Setor> Setores => _database.GetCollection<Setor>("Setores");
        public IMongoCollection<Evento> Eventos => _database.GetCollection<Evento>("Eventos");
        public IMongoCollection<Deteccao> Deteccoes => _database.GetCollection<Deteccao>("Deteccoes");
    }
}
