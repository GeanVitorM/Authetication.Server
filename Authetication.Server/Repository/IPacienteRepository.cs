using Authetication.Server.Models;

namespace Authetication.Server.Repository;

public interface IPacienteRepository
{
    Task<IEnumerable<Paciente>> GetAll();
    Task<Paciente> GetById(int id);
    Task<Paciente> CreateNewPaciente(Paciente paciente);
    Task<Paciente> UpdatePaciente(Paciente paciente);
    Task<Paciente> DeletePaciente(int id);
}
