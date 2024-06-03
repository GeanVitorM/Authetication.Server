using Authetication.Server.Context;
using Authetication.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Authetication.Server.Repository;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _context;
    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Admin> CreateNewAdmin(Admin admin)
    {
        try
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Admin> DeleteAdmin(int id)
    {
        try
        {
            var admin = await GetById(id);
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Admin>> GetAll()
    {
        try
        {
            return await _context.Admins.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Admin> GetById(int id)
    {
        try
        {
            return await _context.Admins.Where(p => p.IdAdmin == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Admin> UpdateAdmin(Admin admin)
    {
        try
        {
            _context.Entry(admin).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return admin;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
