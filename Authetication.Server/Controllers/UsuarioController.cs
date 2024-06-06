﻿using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Controllers
{
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

        [Authorize(Policy = "AdminPolicy")]
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
        [Authorize(Policy = "AdminPolicy")]
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
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
                return BadRequest("Data Invalid");

            try
            {
                // Adiciona lógica para criar o tipo de usuário correspondente
                await CreateTipoUsuario(usuarioDto);

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
        [Authorize(Policy = "AdminPolicy")]
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
        [Authorize(Policy = "AdminPolicy")]
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

        private async Task CreateTipoUsuario(UsuarioDto usuarioDto)
        {
            switch (usuarioDto.TipoUsuario)
            {
                case TipoUsuario.Admin:
                    usuarioDto.Admin = new AdminDto
                    {
                        NomeAdmin = usuarioDto.NameUser,
                        EmailAdmin = usuarioDto.Login
                    };
                    break;
                case TipoUsuario.Coordenador:
                    usuarioDto.Coordenador = new CoordenadorDto
                    {
                        NomeCoordenador = usuarioDto.NameUser,
                        EmailCoordenador = usuarioDto.Login
                    };
                    break;
                case TipoUsuario.Paciente:
                    usuarioDto.Paciente = new PacienteDto
                    {
                        NomePaciente = usuarioDto.NameUser,
                        EmailPaciente = usuarioDto.Login
                    };
                    break;
                case TipoUsuario.Fisioterapeuta:
                    usuarioDto.Fisioterapeuta = new FisioterapeutaDto
                    {
                        NomeFisio = usuarioDto.NameUser,
                        EmailFisio = usuarioDto.Login
                    };
                    break;
                default:
                    throw new InvalidOperationException("Tipo de usuário inválido");
            }

            await Task.CompletedTask;
        }
    }
}