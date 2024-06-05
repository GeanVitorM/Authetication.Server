using Authetication.Server.Context;
using Authetication.Server.Models;
using Authetication.Server.Repository;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Repository;

public class PacienteRepository : IPacienteRepository
{
    private readonly AppDbContext _context;

    public PacienteRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Paciente> CreateNewPaciente(Paciente paciente)
    {
        try
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async  Task<Paciente> DeletePaciente(int id)
    {
        try
        {
            var paciente = await GetById(id);
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Paciente>> GetAll()
    {
        try
        {
            return await _context.Pacientes.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Paciente> GetById(int id)
    {
        try
        {
            return await _context.Pacientes.Where(p => p.IdPaciente == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Paciente> UpdatePaciente(Paciente paciente)
    {
        try
        {
            _context.Entry(paciente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return paciente;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
