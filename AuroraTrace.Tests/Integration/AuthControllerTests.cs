using AuroraTrace.Tests.Integration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_DeveGerarToken()
    {

        var response = await _client.PostAsJsonAsync("/api/auth/login", "teste-usuario");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<TokenResponse>();

        Assert.NotNull(json);
        Assert.False(string.IsNullOrEmpty(json!.Token));
    }

    private record TokenResponse(string Token);
}
