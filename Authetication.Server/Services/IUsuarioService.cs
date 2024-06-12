using Authetication.Server.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Services;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDto>> GetAllUsers();
    Task<UsuarioDto> GetUsuarioById(int id);
    Task CreateUsuario(UsuarioDto usuarioDto);
    Task UpdateUsuario(UsuarioDto usuarioDto);
    Task DeleteUsuario(int id);
    Task<UsuarioDto> GetByUsernameAndPassword(string username, string password);
    Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
}
