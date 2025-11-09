namespace Infrastructure.Security;

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = "AuroraTraceAPI";
    public string Audience { get; set; } = "AuroraTraceClients";
    public int ExpirationMinutes { get; set; } = 60;
}
