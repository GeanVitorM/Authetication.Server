using Authetication.Server.DTOs;
using Authetication.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoordenadorController : ControllerBase
{
    private readonly ILogger<CoordenadorController> _logger;
    private readonly ICoordenadorService _service;

    public CoordenadorController(ILogger<CoordenadorController> logger, ICoordenadorService service)
    {
        _logger = logger;
        _service = service;
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
            return BadRequest("Data Invalid");

        try
        {
            await _service.CreateCoordenador(coordenadorDto);
            return new CreatedAtRouteResult("GetCoord", new { id = coordenadorDto.IdCoordenador }, coordenadorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the coordenador.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
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
            if (coordenadorDto == null)
            {
                return NotFound("Coordenador not found");
            }

            await _service.DeleteCoordenador(id);
            return Ok(coordenadorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the coordenador with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
