﻿using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authetication.Server.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IMapper _mapper;
        private readonly IPacienteRepository _repository;
        private readonly ILogger<PacienteService> _logger;

        public PacienteService(IMapper mapper, IPacienteRepository repository, ILogger<PacienteService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CreatePaciente(PacienteDto pacienteDto)
        {
            try
            {
                var pacienteEntity = _mapper.Map<Paciente>(pacienteDto);
                await _repository.CreateNewPaciente(pacienteEntity);
                pacienteDto.IdPaciente = pacienteEntity.IdPaciente;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar paciente.");
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeletePaciente(int id)
        {
            try
            {
                var pacienteEntity = await _repository.GetById(id);
                if (pacienteEntity == null)
                {
                    throw new Exception("Paciente não encontrado.");
                }
                await _repository.DeletePaciente(pacienteEntity.IdPaciente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar paciente.");
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<PacienteDto>> GetAllPacientes()
        {
            try
            {
                var pacienteEntities = await _repository.GetAll();
                if (pacienteEntities == null || !pacienteEntities.Any())
                {
                    throw new Exception("Nenhum paciente encontrado.");
                }
                return _mapper.Map<IEnumerable<PacienteDto>>(pacienteEntities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar pacientes.");
                throw new Exception($"Erro ao recuperar pacientes: {ex.Message}", ex);
            }
        }

        public async Task<PacienteDto> GetPacienteById(int id)
        {
            try
            {
                var pacienteEntity = await _repository.GetById(id);
                if (pacienteEntity == null)
                {
                    throw new Exception("Paciente não encontrado.");
                }
                return _mapper.Map<PacienteDto>(pacienteEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar paciente por ID.");
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdatePaciente(PacienteDto pacienteDto)
        {
            try
            {
                var pacienteEntity = _mapper.Map<Paciente>(pacienteDto);
                await _repository.UpdatePaciente(pacienteEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar paciente.");
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
