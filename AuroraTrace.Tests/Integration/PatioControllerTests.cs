using Infrastructure.Security;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace AuroraTrace.Tests.Integration
{
    public class PatioControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PatioControllerTests(CustomWebApplicationFactory<Program> factory)
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
        public async Task PostPatio_DeveCriarPatioERetornarCreated()
        {
            var request = new
            {
                nome = "Pátio Central",
                cols = 4,
                rows = 3
            };

            var response = await _client.PostAsJsonAsync("/api/patio", request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<PatioResponse>();
            Assert.NotNull(result);
            Assert.Equal("Pátio Central", result!.Nome);
            Assert.True(result.Cols > 0 && result.Rows > 0);
        }

        [Fact]
        public async Task GetAll_DeveRetornarListaComPeloMenosUmPatio()
        {
            var create = new { nome = "Teste Pátio", cols = 2, rows = 2 };
            var post = await _client.PostAsJsonAsync("/api/patio", create);
            post.EnsureSuccessStatusCode();

            var response = await _client.GetAsync("/api/patio");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            Assert.Contains("Teste Pátio", json);
        }

        private record PatioResponse(string Id, string Nome, int Cols, int Rows);
    }
}
