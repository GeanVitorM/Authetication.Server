using Authetication.Server.DTOs;
using Authetication.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;
    private readonly IAdminService _service;

    public AdminController(ILogger<AdminController> logger, IAdminService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
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
    public async Task<ActionResult> Post([FromBody] AdminDto adminDto)
    {
        if (adminDto == null)
            return BadRequest("Data Invalid");

        try
        {
            await _service.CreateAdmin(adminDto);
            return new CreatedAtRouteResult("GetAdmin", new { id = adminDto.IdAdmin }, adminDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the admin.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut()]
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
