using Authetication.Server.DTOs;
using Authetication.Server.Services;
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

    public FisioterapeutaController(ILogger<FisioterapeutaController> logger, IFisioterapeutaService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
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
    public async Task<ActionResult> Post([FromBody] FisioterapeutaDto fisioterapeutaDto)
    {
        if (fisioterapeutaDto == null)
            return BadRequest("Data Invalid");

        try
        {
            await _service.CreateFisioterapeuta(fisioterapeutaDto);
            return new CreatedAtRouteResult("GetFisio", new { id = fisioterapeutaDto.IdFisio }, fisioterapeutaDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the fisioterapeuta.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut()]
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
