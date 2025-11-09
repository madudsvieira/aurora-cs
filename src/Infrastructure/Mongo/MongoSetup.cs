using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Infrastructure.Mongo
{
    public class MongoSetup : IHostedService
    {
        private readonly IMongoClient _client;
        private readonly IConfiguration _cfg;

        public MongoSetup(IMongoClient client, IConfiguration cfg)
        {
            _client = client;
            _cfg = cfg;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var dbName = _cfg["MongoDB:Database"] ?? "AuroraTraceDb";
            var db = _client.GetDatabase(dbName);

            await EnsureCollectionWithSample(db, "Motos", new BsonDocument
            {
                { "Placa", "ABC1D23" },
                { "Modelo", "CG 160" },
                { "Status", "DISPONIVEL" },
                { "UltimoSetor", BsonNull.Value },
                { "UltimoSlot", BsonNull.Value }
            });

            await EnsureCollectionWithSample(db, "Patios", new BsonDocument
            {
                { "Nome", "Pátio Central" }
            });

            await EnsureCollectionWithSample(db, "Setores", new BsonDocument
            {
                { "Nome", "Setor A" }
            });

            await EnsureCollectionWithSample(db, "Eventos", new BsonDocument
            {
                { "Tipo", "Criacao" },
                { "Descricao", "Evento 1" }
            });

            await EnsureCollectionWithSample(db, "Deteccoes", new BsonDocument
            {
                { "Placa", "ABC1D23" },
                { "Score", 0.98 }
            });
        }

        private static async Task EnsureCollectionWithSample(IMongoDatabase db, string name, BsonDocument sample)
        {
            var names = await db.ListCollectionNames().ToListAsync();
            if (!names.Contains(name))
                await db.CreateCollectionAsync(name);

            var col = db.GetCollection<BsonDocument>(name);
            var count = await col.CountDocumentsAsync(FilterDefinition<BsonDocument>.Empty);
            if (count == 0)
                await col.InsertOneAsync(sample);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
