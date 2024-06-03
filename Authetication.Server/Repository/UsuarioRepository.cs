using Authetication.Server.Context;
using Authetication.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Authetication.Server.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;
    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> CreateNewUsuario(Usuario usuario)
    {
        try
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Usuario> DeleteUsuario(int id)
    {
        try
        {
            var usario = await GetById(id);
            _context.Usuarios.Remove(usario);
            await _context.SaveChangesAsync();
            return usario;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Usuario>> GetAll()
    {
        try
        {
            return await _context.Usuarios.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Usuario> GetById(int id)
    {
        try
        {
            return await _context.Usuarios.Where(p => p.IdUser == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Usuario> UpdateUsuario(Usuario usuario)
    {
        try
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return usuario;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
