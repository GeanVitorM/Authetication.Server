using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Repository;
using AutoMapper;

namespace Authetication.Server.Services;

public class CoordenadorService : ICoordenadorService
{
    private readonly IMapper _mapper;
    private readonly ICoordenadorRepository _repository;

    public CoordenadorService(IMapper mapper, ICoordenadorRepository repository)
    {
        _mapper = mapper;
        repository = repository;
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
            throw new Exception(ex.Message);
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
            throw new Exception(ex.Message);
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
            throw new Exception(ex.Message);
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
            throw new Exception(ex.Message);
        }
    }

    public async  Task UpdateCoordenador(CoordenadorDto coordenadorDto)
    {
        try
        {
            var coordEntity = _mapper.Map<Coordenador>(coordenadorDto);
            await _repository.UpdateCoordenador(coordEntity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
