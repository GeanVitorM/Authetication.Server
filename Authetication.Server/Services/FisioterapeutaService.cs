using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Repository;
using AutoMapper;

namespace Authetication.Server.Services;

public class FisioterapeutaService : IFisioterapeutaService
{
    private readonly IMapper _mapper;
    private readonly IFisioterapeutaRepository _repository;

    public FisioterapeutaService(IMapper mapper, IFisioterapeutaRepository repository)
    {
        _mapper = mapper;
        repository = repository;
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
            throw new Exception(ex.Message);
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
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<FisioterapeutaDto>> GetAllFisios()
    {
        try
        {
            var fisioEntity = await _repository.GetAll();
            return _mapper.Map<IEnumerable<FisioterapeutaDto>>(fisioEntity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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
            throw new Exception(ex.Message);
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
            throw new Exception(ex.Message);
        }
    }
}
