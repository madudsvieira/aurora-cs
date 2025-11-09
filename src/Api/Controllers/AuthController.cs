using Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    /// <summary>
    /// Gera um token JWT de teste para autenticação no Swagger.
    /// </summary>
    /// <param name="request">Objeto contendo o userId.</param>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserId))
            return BadRequest(new { message = "UserId é obrigatório." });

        var token = _jwtService.GenerateToken(request.UserId);
        return Ok(new { token });
    }

    public record LoginRequest(string UserId);
}
