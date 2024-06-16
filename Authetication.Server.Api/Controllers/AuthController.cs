using Authetication.Server.Api.DTOs;
using Authetication.Server.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Authetication.Server.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;
    private readonly IUsuarioService _usuarioService;

    public AuthController(ILogger<AuthController> logger, IAuthService authService, IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
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

    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePassword(int userId, [FromBody] ChangePasswordDto changePasswordDto)
    {
        if (changePasswordDto == null || string.IsNullOrWhiteSpace(changePasswordDto.OldPassword) || string.IsNullOrWhiteSpace(changePasswordDto.NewPassword))
        {
            return BadRequest("Invalid password data");
        }

        try
        {
            var user = await _usuarioService.GetUsuarioById(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }
            bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, user.Password);

            if (!isOldPasswordValid)
            {
                return BadRequest("Invalid old password");
            }
            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            user.Password = hashedNewPassword;
            await _usuarioService.UpdateUsuario(user);

            return Ok("Password changed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while changing the password.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

}
