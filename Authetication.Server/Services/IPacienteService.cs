﻿using Authetication.Server.DTOs;

namespace Authetication.Server.Services;

public interface IPacienteService
{
    Task<IEnumerable<PacienteDto>> GetAllPacientes();
    Task<PacienteDto> GetPacienteById(int id);
    Task CreatePaciente(PacienteDto pacienteDto);
    Task UpdatePaciente(PacienteDto pacienteDto);
    Task DeletePaciente(int id);
}
