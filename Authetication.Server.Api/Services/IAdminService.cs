using Authetication.Server.Api.DTOs;

namespace Authetication.Server.Api.Services;

public interface IAdminService
{
    Task<IEnumerable<AdminDto>> GetAllAdmins();
    Task<AdminDto> GetAdminById(int id);
    Task CreateAdmin(AdminDto adminDto);
    Task UpdateAdmin(AdminDto adminDto);
    Task DeleteAdmin(int id);
}
