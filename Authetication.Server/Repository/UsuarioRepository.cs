using Authetication.Server.Context;
using Authetication.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateNewUsuario(Usuario usuario)
    {
        try
        {
             usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
             _context.Usuarios.Add(usuario);
             await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteUsuario(int id)
    {
        try
        {
            var usuario = await GetById(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
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
            var usuarioEntity = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(p => p.IdUser == id);
            return usuarioEntity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateUsuario(Usuario usuario)
    {
        try
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Usuario> GetByUsername(string username)
    {
        try
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
