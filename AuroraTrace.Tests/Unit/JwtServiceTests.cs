using Infrastructure.Security;
using Xunit;

public class JwtServiceTests
{
    [Fact]
    public void GenerateToken_DeveRetornarTokenValido()
    {
        var settings = new JwtSettings
        {
            Key = "chave_local_super_secreta_para_testes_1234567890123456",
            Issuer = "AuroraTraceAPI",
            Audience = "AuroraTraceClients",
            ExpirationMinutes = 60
        };

        var service = new JwtService(settings);

        var token = service.GenerateToken("user123", "Admin");

        Assert.False(string.IsNullOrEmpty(token));
    }
}
