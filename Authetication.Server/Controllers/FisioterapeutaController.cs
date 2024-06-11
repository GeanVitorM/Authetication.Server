using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FisioterapeutaController : ControllerBase
{
    private readonly ILogger<FisioterapeutaController> _logger;
    private readonly IFisioterapeutaService _service;
    private readonly IUsuarioService _usuarioService;

    public FisioterapeutaController(ILogger<FisioterapeutaController> logger, IFisioterapeutaService service, IUsuarioService usuarioService)
    {
        _logger = logger;
        _service = service;
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminOrCoordenadorPolicy")]
    public async Task<ActionResult<IEnumerable<FisioterapeutaDto>>> Get()
    {
        try
        {
            var fisioterapeutaDto = await _service.GetAllFisios();
            if (fisioterapeutaDto == null)
            {
                return NotFound("Fisioterapeutas not found");
            }
            return Ok(fisioterapeutaDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all fisioterapeutas.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "GetFisio")]
    [Authorize(Policy = "AdminOrCoordenadorPolicy")]
    public async Task<ActionResult<FisioterapeutaDto>> Get(int id)
    {
        try
        {
            var fisioterapeutaDto = await _service.GetFisioById(id);
            if (fisioterapeutaDto == null)
            {
                return NotFound("Fisioterapeuta not found");
            }
            return Ok(fisioterapeutaDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching the fisioterapeuta with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    [Authorize(Policy = "AdminOrCoordenadorPolicy")]
    public async Task<ActionResult> Post([FromBody] FisioterapeutaDto fisioterapeutaDto)
    {
        if (fisioterapeutaDto == null)
            return BadRequest("Dados inválidos");

        try
        {
            var novoUsuarioDto = new UsuarioDto
            {
                Username = fisioterapeutaDto.EmailFisio,
                Password = "asdf1234",
                TipoUsuario = TipoUsuario.Fisioterapeuta
            };

            await _usuarioService.CreateUsuario(novoUsuarioDto);

            fisioterapeutaDto.IdFisio = novoUsuarioDto.IdUser;

            await _service.CreateFisioterapeuta(fisioterapeutaDto);

            return Ok(new { Paciente = fisioterapeutaDto, Usuario = novoUsuarioDto });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar o paciente");
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }
    }


    [HttpPut()]
    [Authorize(Policy = "AdminOrCoordenadorPolicy")]
    public async Task<ActionResult> Put([FromBody] FisioterapeutaDto fisioterapeutaDto)
    {
        if (fisioterapeutaDto == null)
            return BadRequest("Data invalid");

        try
        {
            await _service.UpdateFisioterapeuta(fisioterapeutaDto);
            return Ok(fisioterapeutaDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the fisioterapeuta.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOrCoordenadorPolicy")]
    public async Task<ActionResult<FisioterapeutaDto>> Delete(int id)
    {
        try
        {
            var fisioterapeutaDto = await _service.GetFisioById(id);
            if (fisioterapeutaDto == null)
            {
                return NotFound("Fisioterapeuta not found");
            }

            await _service.DeleteFisioterapeuta(id);
            return Ok(fisioterapeutaDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the fisioterapeuta with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
