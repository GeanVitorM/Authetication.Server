using Authetication.Server.DTOs;
using Authetication.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Authetication.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UsuarioDto loginDto)
    {
        if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            return BadRequest("Invalid username or password");

        try
        {
            _logger.LogInformation($"Attempting login for user: {loginDto.Username}");
            var token = await _authService.Authenticate(loginDto.Username, loginDto.Password);
            if (token == null)
            {
                _logger.LogWarning($"Login failed for user: {loginDto.Username}");
                return Unauthorized();
            }

            string nameUser = loginDto.Username?.Split('@')[0];

            _logger.LogInformation($"Login successful for user: {loginDto.Username}");
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while logging in.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
