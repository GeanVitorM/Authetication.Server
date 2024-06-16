using Authetication.Server.Api.DTOs;
using Authetication.Server.Api.Models;
using Authetication.Server.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CoordenadorController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<CoordenadorController> _logger;
    private readonly ICoordenadorService _service;
    private readonly IUsuarioService _usuarioService;

    public CoordenadorController(IMapper mapper, ILogger<CoordenadorController> logger, ICoordenadorService service, IUsuarioService usuarioService)
    {
        _logger = logger;
        _mapper = mapper;
        _service = service;
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<IEnumerable<CoordenadorDto>>> Get()
    {
        try
        {
            var coordenadorDto = await _service.GetAllCoords();
            if (coordenadorDto == null)
            {
                return NotFound("Coordenadores not found");
            }
            return Ok(coordenadorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all coordenadores.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "GetCoord")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<CoordenadorDto>> Get(int id)
    {
        try
        {
            var coordenadorDto = await _service.GetCoordById(id);
            if (coordenadorDto == null)
            {
                return NotFound("Coordenador not found");
            }
            return Ok(coordenadorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching the coordenador with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> Post([FromBody] CoordenadorDto coordenadorDto)
    {
        if (coordenadorDto == null)
            return BadRequest("Dados inválidos");

        try
        {
            var novoUsuarioDto = new UsuarioDto
            {
                Username = coordenadorDto.EmailCoordenador,
                Password = "asdf1234",
                TipoUsuario = TipoUsuario.Coordenador
            };

            await _usuarioService.CreateUsuario(novoUsuarioDto);
            coordenadorDto.IdCoordenador = novoUsuarioDto.IdUser;
            await _service.CreateCoordenador(coordenadorDto);
            return Ok(new { Coordenador = coordenadorDto, Usuario = novoUsuarioDto });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar o Coordenador");
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }
    }



    [HttpPut()]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> Put([FromBody] CoordenadorDto coordenadorDto)
    {
        if (coordenadorDto == null)
            return BadRequest("Data invalid");

        try
        {
            await _service.UpdateCoordenador(coordenadorDto);
            return Ok(coordenadorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the coordenador.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<CoordenadorDto>> Delete(int id)
    {
        try
        {
            var coordenadorDto = await _service.GetCoordById(id);
            var usuarioDto = await _usuarioService.GetUsuarioById(id);
            if (coordenadorDto == null && usuarioDto == null)
            {
                return NotFound("Coordenador not found");
            }

            await _service.DeleteCoordenador(id);
            await _usuarioService.DeleteUsuario(id);
            return Ok(coordenadorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the coordenador with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
