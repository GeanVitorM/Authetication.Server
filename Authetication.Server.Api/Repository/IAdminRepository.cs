using Authetication.Server.Api.Models;

namespace Authetication.Server.Api.Repository;

public interface IAdminRepository
{
    Task<IEnumerable<Admin>> GetAll();
    Task<Admin> GetById(int id);
    Task<Admin> CreateNewAdmin(Admin admin);
    Task<Admin> UpdateAdmin(Admin admin);
    Task<Admin> DeleteAdmin(int id);
}
