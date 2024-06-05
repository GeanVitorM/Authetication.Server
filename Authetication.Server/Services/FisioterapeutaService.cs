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

public class FisioterapeutaService : IFisioterapeutaService
{
    private readonly IMapper _mapper;
    private readonly IFisioterapeutaRepository _repository;
    private readonly ILogger<FisioterapeutaService> _logger;

    public FisioterapeutaService(IMapper mapper, IFisioterapeutaRepository repository, ILogger<FisioterapeutaService> logger)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            if (fisioEntity == null)
            {
                throw new Exception("Fisioterapeuta não encontrado.");
            }
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
            if (fisioEntities == null || !fisioEntities.Any())
            {
                throw new Exception("Nenhum fisioterapeuta encontrado.");
            }
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
            if (fisioEntity == null)
            {
                throw new Exception("Fisioterapeuta não encontrado.");
            }
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
