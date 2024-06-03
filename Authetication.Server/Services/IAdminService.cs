using Authetication.Server.DTOs;

namespace Authetication.Server.Services;

public interface IAdminService
{
    Task<IEnumerable<AdminDto>> GetAllAdmins();
    Task<AdminDto> GetAdminById(int id);
    Task CreateAdmin(AdminDto adminDto);
    Task UpdateAdmin(AdminDto adminDto);
    Task DeleteAdmin(int id);
}
