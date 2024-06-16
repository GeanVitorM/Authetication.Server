using Authetication.Server.Api.DTOs;
using Authetication.Server.Api.Models;
using Authetication.Server.Api.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authetication.Server.Api.Services;

public class CoordenadorService : ICoordenadorService
{
    private readonly IMapper _mapper;
    private readonly ICoordenadorRepository _repository;
    private readonly ILogger<CoordenadorService> _logger;

    public CoordenadorService(IMapper mapper, ICoordenadorRepository repository, ILogger<CoordenadorService> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
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
            var coordEntity = await _repository.GetAll();
            return _mapper.Map<IEnumerable<CoordenadorDto>>(coordEntity);
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
