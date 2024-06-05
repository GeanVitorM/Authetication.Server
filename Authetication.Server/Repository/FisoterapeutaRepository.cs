using Authetication.Server.Context;
using Authetication.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Authetication.Server.Repository;

public class FisioterapeutaRepository : IFisioterapeutaRepository
{
    private readonly AppDbContext _context;
    public FisioterapeutaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Fisioterapeuta> CreateNewFisioterapeuta(Fisioterapeuta fisioterapeuta)
    {
        try
        {
            _context.Fisioterapeutas.Add(fisioterapeuta);
            await _context.SaveChangesAsync();
            return fisioterapeuta;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Fisioterapeuta> DeleteFisioterapeuta(int id)
    {
        try
        {
            var fisio = await GetById(id);
            _context.Fisioterapeutas.Remove(fisio);
            await _context.SaveChangesAsync();
            return fisio;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Fisioterapeuta>> GetAll()
    {
        try
        {
            return await _context.Fisioterapeutas.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Fisioterapeuta> GetById(int id)
    {
        try
        {
            return await _context.Fisioterapeutas.Where(p => p.IdFisio == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Fisioterapeuta> UpdateFisioterapeuta(Fisioterapeuta fisioterapeuta)
    {
        try
        {
            _context.Entry(fisioterapeuta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return fisioterapeuta;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
