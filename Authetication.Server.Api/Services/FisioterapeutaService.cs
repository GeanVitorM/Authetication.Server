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

public class FisioterapeutaService : IFisioterapeutaService
{
    private readonly IMapper _mapper;
    private readonly IFisioterapeutaRepository _repository;
    private readonly ILogger<FisioterapeutaService> _logger;

    public FisioterapeutaService(IMapper mapper, IFisioterapeutaRepository repository, ILogger<FisioterapeutaService> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateFisioterapeuta(FisioterapeutaDto fisioterapeutaDto)
    {
        try
        {
            var fisioEntity = _mapper.Map<Fisioterapeuta>(fisioterapeutaDto);
            await _repository.CreateNewFisioterapeuta(fisioEntity);
            fisioterapeutaDto.IdFisio = fisioEntity.IdFisio;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar fisioterapeuta.");
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task DeleteFisioterapeuta(int id)
    {
        try
        {
            var fisioEntity = await _repository.GetById(id);
            await _repository.DeleteFisioterapeuta(fisioEntity.IdFisio);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar fisioterapeuta.");
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<IEnumerable<FisioterapeutaDto>> GetAllFisios()
    {
        try
        {
            var fisioEntities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<FisioterapeutaDto>>(fisioEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao recuperar fisioterapeutas.");
            throw new Exception($"Erro ao recuperar fisioterapeutas: {ex.Message}", ex);
        }
    }

    public async Task<FisioterapeutaDto> GetFisioById(int id)
    {
        try
        {
            var fisioEntity = await _repository.GetById(id);
            return _mapper.Map<FisioterapeutaDto>(fisioEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao recuperar fisioterapeuta por ID.");
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task UpdateFisioterapeuta(FisioterapeutaDto fisioterapeutaDto)
    {
        try
        {
            var fisioEntity = _mapper.Map<Fisioterapeuta>(fisioterapeutaDto);
            await _repository.UpdateFisioterapeuta(fisioEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar fisioterapeuta.");
            throw new Exception(ex.Message, ex);
        }
    }
}
