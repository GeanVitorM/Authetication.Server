using Authetication.Server.DTOs;
using Authetication.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authetication.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacienteController : ControllerBase
{
    private readonly ILogger<PacienteController> _logger;
    private readonly IPacienteService _service;

    public PacienteController(ILogger<PacienteController> logger, IPacienteService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> Get()
    {
        try
        {
            var pacienteDto = await _service.GetAllPacientes();
            if (pacienteDto == null)
            {
                return NotFound("Pacientes not found");
            }
            return Ok(pacienteDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all users.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "GetPaciente")]
    public async Task<ActionResult<PacienteDto>> Get(int id)
    {
        try
        {
            var pacienteDto = await _service.GetPacienteById(id);
            if (pacienteDto == null)
            {
                return NotFound("Pacientes not found");
            }
            return Ok(pacienteDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching the user with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PacienteDto pacienteDto)
    {
        if (pacienteDto == null)
            return BadRequest("Data Invalid");

        try
        {
            await _service.CreatePaciente(pacienteDto);
            return new CreatedAtRouteResult("GetPaciente", new { id = pacienteDto.IdPaciente }, pacienteDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the user.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut()]
    public async Task<ActionResult> Put([FromBody] PacienteDto pacienteDto)
    {
        if (pacienteDto == null)
            return BadRequest("Data invalid");

        try
        {
            await _service.UpdatePaciente(pacienteDto);
            return Ok(pacienteDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the user.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PacienteDto>> Delete(int id)
    {
        try
        {
            var pacienteDto = await _service.GetPacienteById(id);
            if (pacienteDto == null)
            {
                return NotFound("Paciente not found");
            }

            await _service.DeletePaciente(id);
            return Ok(pacienteDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the user with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
