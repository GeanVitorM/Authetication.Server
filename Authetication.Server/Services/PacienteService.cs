using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Repository;
using AutoMapper;

namespace Authetication.Server.Services;

public class PacienteService : IPacienteService
{
    private readonly IMapper _mapper;
    private readonly IPacienteRepository _repository;

    public PacienteService(IMapper mapper, IPacienteRepository repository)
    {
        _mapper = mapper;
        repository = repository;
    }

    public async Task CreatPaciente(PacienteDto pacienteDto)
    {
        try
        {
            var pacienteEntity = _mapper.Map<Paciente>(pacienteDto);
            await _repository.CreateNewPaciente(pacienteEntity);
            pacienteDto.IdPaciente = pacienteEntity.IdPaciente;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task DeletePaciente(int id)
    {
        try
        {
            var pacienteEntity = await _repository.GetById(id);
            await _repository.DeletePaciente(pacienteEntity.IdPaciente);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async  Task<IEnumerable<PacienteDto>> GetAllPacientes()
    {
        try
        {
            var pacienteEntity = await _repository.GetAll();
            return _mapper.Map<IEnumerable<PacienteDto>>(pacienteEntity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async  Task<PacienteDto> GetPacienteById(int id)
    {
        try
        {
            var pacienteEntity = await _repository.GetById(id);
            return _mapper.Map<PacienteDto>(pacienteEntity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task UpdatePaciente(PacienteDto pacienteDto)
    {
        try
        {
            var projetoEntity = _mapper.Map<Paciente>(pacienteDto);
            await _repository.UpdatePaciente(projetoEntity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
