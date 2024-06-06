using Authetication.Server.Context;
using Authetication.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            var usuario = await GetById(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return usuario;
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
            return await _context.Usuarios.FirstOrDefaultAsync(p => p.IdUser == id);
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

    public async Task<Usuario> GetByUsernameAndPassword(string username, string password)
    {
        try
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == username && u.Password == password);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
