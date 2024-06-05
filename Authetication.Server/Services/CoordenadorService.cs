using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authetication.Server.Services;

public class CoordenadorService : ICoordenadorService
{
    private readonly IMapper _mapper;
    private readonly ICoordenadorRepository _repository;
    private readonly ILogger<CoordenadorService> _logger;

    public CoordenadorService(IMapper mapper, ICoordenadorRepository repository, ILogger<CoordenadorService> logger)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task CreateCoordenador(CoordenadorDto coordenadorDto)
    {
        try
        {
            var coordEntity = _mapper.Map<Coordenador>(coordenadorDto);
            await _repository.CreateNewCoordenador(coordEntity);
            coordenadorDto.IdCoordenador = coordEntity.IdCoordenador;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar coordenador.");
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task DeleteCoordenador(int id)
    {
        try
        {
            var coordEntity = await _repository.GetById(id);
            if (coordEntity == null)
            {
                throw new Exception("Coordenador não encontrado.");
            }
            await _repository.DeleteCoordenador(coordEntity.IdCoordenador);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar coordenador.");
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<IEnumerable<CoordenadorDto>> GetAllCoords()
    {
        try
        {
            var coordEntities = await _repository.GetAll();
            if (coordEntities == null || !coordEntities.Any())
            {
                throw new Exception("Nenhum coordenador encontrado.");
            }
            return _mapper.Map<IEnumerable<CoordenadorDto>>(coordEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao recuperar coordenadores.");
            throw new Exception($"Erro ao recuperar coordenadores: {ex.Message}", ex);
        }
    }

    public async Task<CoordenadorDto> GetCoordById(int id)
    {
        try
        {
            var coordEntity = await _repository.GetById(id);
            if (coordEntity == null)
            {
                throw new Exception("Coordenador não encontrado.");
            }
            return _mapper.Map<CoordenadorDto>(coordEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao recuperar coordenador por ID.");
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task UpdateCoordenador(CoordenadorDto coordenadorDto)
    {
        try
        {
            var coordEntity = _mapper.Map<Coordenador>(coordenadorDto);
            await _repository.UpdateCoordenador(coordEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar coordenador.");
            throw new Exception(ex.Message, ex);
        }
    }
}
