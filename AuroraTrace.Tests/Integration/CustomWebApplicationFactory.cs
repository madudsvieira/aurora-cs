using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace AuroraTrace.Tests.Integration
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(AuroraMongoContext));
                if (descriptor != null)
                    services.Remove(descriptor);

                var mongoClientDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IMongoClient));
                if (mongoClientDescriptor != null)
                    services.Remove(mongoClientDescriptor);

                var client = new MongoClient("mongodb://localhost:27017");
                var testDbName = $"AuroraTrace_Test_{Guid.NewGuid()}";
                var database = client.GetDatabase(testDbName);

                database.DropCollection("Motos");
                database.DropCollection("Patios");
                database.DropCollection("Setores");
                database.DropCollection("Eventos");
                database.DropCollection("Deteccoes");

                services.AddSingleton<IMongoClient>(client);
                services.AddSingleton<AuroraMongoContext>(_ =>
                {
                    var configuration = new ConfigurationBuilder()
                        .AddInMemoryCollection(new Dictionary<string, string?>
                        {
                            { "ConnectionStrings:MongoDb", "mongodb://localhost:27017" }
                        })
                        .Build();

                    return new AuroraMongoContext(configuration);
                });
            });

            return base.CreateHost(builder);
        }
    }
}
