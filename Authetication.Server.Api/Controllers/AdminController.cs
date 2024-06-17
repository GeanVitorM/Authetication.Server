using Authetication.Server.Api.DTOs;
using Authetication.Server.Api.Middlewares;
using Authetication.Server.Api.Models;
using Authetication.Server.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;
    private readonly IAdminService _service;
    private readonly IUsuarioService _usuarioService;
    private readonly RandomPassword _randomPassword;

    public AdminController(ILogger<AdminController> logger, IAdminService service, IUsuarioService usuarioService, RandomPassword randomPassword)
    {
        _logger = logger;
        _service = service;
        _usuarioService = usuarioService;
        _randomPassword = randomPassword;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<IEnumerable<AdminDto>>> Get()
    {
        try
        {
            var adminDto = await _service.GetAllAdmins();
            if (adminDto == null)
            {
                return NotFound("Admins not found");
            }
            return Ok(adminDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all admins.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "GetAdmin")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<AdminDto>> Get(int id)
    {
        try
        {
            var adminDto = await _service.GetAdminById(id);
            if (adminDto == null)
            {
                return NotFound("Admin not found");
            }
            return Ok(adminDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching the admin with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> Post([FromBody] AdminDto adminDto)
    {
        if (adminDto == null)
            return BadRequest("Dados inválidos");

        string senhaAleatoria = _randomPassword.GerarSenhaAleatoria();

        try
        {
            var novoUsuarioDto = new UsuarioDto
            {
                Username = adminDto.EmailAdmin,
                Password = senhaAleatoria,
                TipoUsuario = TipoUsuario.Admin
            };

            await _usuarioService.CreateUsuario(novoUsuarioDto);
            adminDto.IdAdmin = novoUsuarioDto.IdUser;
            await _service.CreateAdmin(adminDto);
            return Ok(new { Admin = adminDto, Usuario = novoUsuarioDto });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar o Administrador");
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }
    }

    [HttpPut()]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> Put([FromBody] AdminDto adminDto)
    {
        if (adminDto == null)
            return BadRequest("Data invalid");

        try
        {
            await _service.UpdateAdmin(adminDto);
            return Ok(adminDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the admin.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<AdminDto>> Delete(int id)
    {
        try
        {
            var adminDto = await _service.GetAdminById(id);
            if (adminDto == null)
            {
                return NotFound("Admin not found");
            }

            await _service.DeleteAdmin(id);
            return Ok(adminDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the admin with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
