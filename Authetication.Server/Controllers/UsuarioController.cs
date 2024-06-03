using Authetication.Server.DTOs;
using Authetication.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioService _service;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> Get()
    {
        try
        {
            var usuarioDto = await _service.GetAllUsers();
            if (usuarioDto == null)
            {
                return NotFound("Usuarios not found");
            }
            return Ok(usuarioDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all users.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<UsuarioDto>> Get(int id)
    {
        try
        {
            var usuarioDto = await _service.GetUsuarioById(id);
            if (usuarioDto == null)
            {
                return NotFound("Usuario not found");
            }
            return Ok(usuarioDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching the user with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UsuarioDto usuarioDto)
    {
        if (usuarioDto == null)
            return BadRequest("Data Invalid");

        try
        {
            await _service.CreateUsuario(usuarioDto);
            return new CreatedAtRouteResult("GetUser", new { id = usuarioDto.IdUser }, usuarioDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the user.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut()]
    public async Task<ActionResult> Put([FromBody] UsuarioDto usuarioDto)
    {
        if (usuarioDto == null)
            return BadRequest("Data invalid");

        try
        {
            await _service.UpdateUsuario(usuarioDto);
            return Ok(usuarioDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the user.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UsuarioDto>> Delete(int id)
    {
        try
        {
            var usuarioDto = await _service.GetUsuarioById(id);
            if (usuarioDto == null)
            {
                return NotFound("Usuario not found");
            }

            await _service.DeleteUsuario(id);
            return Ok(usuarioDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the user with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
