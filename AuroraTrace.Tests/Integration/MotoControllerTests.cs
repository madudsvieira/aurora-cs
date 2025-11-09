using Infrastructure.Security;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace AuroraTrace.Tests.Integration
{
    public class MotoControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MotoControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            
            var jwtService = new JwtService(new JwtSettings
            {
                Key = "chave_local_super_secreta_para_testes_1234567890123456",
                Issuer = "AuroraTraceAPI",
                Audience = "AuroraTraceClients"
            });

            var token = jwtService.GenerateToken("user-teste");
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        [Fact]
        public async Task GetMotos_DeveRetornarListaVazia()
        {
            var response = await _client.GetAsync("/api/Moto");

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Assert.Contains("[]", json);
        }
    }
}
