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
public class FisioterapeutaController : ControllerBase
{
    private readonly ILogger<FisioterapeutaController> _logger;
    private readonly IFisioterapeutaService _service;
    private readonly IUsuarioService _usuarioService;
    private readonly RandomPassword _randomPassword;

    public FisioterapeutaController(ILogger<FisioterapeutaController> logger, IFisioterapeutaService service, IUsuarioService usuarioService, RandomPassword randomPassword)
    {
        _logger = logger;
        _service = service;
        _usuarioService = usuarioService;
        _randomPassword = randomPassword;
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
                Password = fisioterapeutaDto.Password,
                TipoUsuario = TipoUsuario.Fisioterapeuta 
            };

            await _usuarioService.CreateUsuario(novoUsuarioDto);
            fisioterapeutaDto.IdFisio = novoUsuarioDto.IdUser;
            await _service.CreateFisioterapeuta(fisioterapeutaDto);
            return Ok(new { Fisioterapeuta = fisioterapeutaDto, Usuario = novoUsuarioDto });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar o Fisioterapeuta");
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
            var usuarioDto = await _usuarioService.GetUsuarioById(id);
            if (fisioterapeutaDto == null)
            {
                return NotFound("Fisioterapeuta not found");
            }

            await _service.DeleteFisioterapeuta(id);
            await _usuarioService.DeleteUsuario(id);
            return Ok(fisioterapeutaDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the fisioterapeuta with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
