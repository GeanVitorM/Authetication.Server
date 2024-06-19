using Authetication.Server.Api.DTOs;

namespace Authetication.Server.Api.Services;

public interface IPacienteService
{
    Task<IEnumerable<PacienteDto>> GetAllPacientes();
    Task<PacienteDto> GetPacienteById(int id);
    Task CreatePaciente(PacienteDto pacienteDto);
    Task UpdatePaciente(PacienteDto pacienteDto);
    Task UpdatePrimeiraConsulta(PacienteDto pacienteDto);
    Task DeletePaciente(int id);
}
