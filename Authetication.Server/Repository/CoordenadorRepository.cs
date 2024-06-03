using Authetication.Server.Context;
using Authetication.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Authetication.Server.Repository;

public class CoordenadorRepository : ICoordenadorRepository
{
    private readonly AppDbContext _context;
    public CoordenadorRepository(AppDbContext context)
    {
    
        _context = context;
    }

    public async Task<Coordenador> CreateNewCoordenador(Coordenador coordenador)
    {
        try
        {
            _context.Coordenadores.Add(coordenador);
            await _context.SaveChangesAsync();
            return coordenador;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Coordenador> DeleteCoordenador(int id)
    {
        try
        {
            var coordenador = await GetById(id);
            _context.Coordenadores.Remove(coordenador);
            await _context.SaveChangesAsync();
            return coordenador;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Coordenador>> GetAll()
    {
        try
        {
            return await _context.Coordenadores.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Coordenador> GetById(int id)
    {
        try
        {
            return await _context.Coordenadores.Where(p => p.IdCoordenador == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async  Task<Coordenador> UpdateCoordenador(Coordenador coordenador)
    {
        try
        {
            _context.Entry(coordenador).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return coordenador;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
