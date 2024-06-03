using Authetication.Server.Models;

namespace Authetication.Server.Repository;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAll();
    Task<Usuario> GetById(int id);
    Task<Usuario> CreateNewUsuario(Usuario usuario);
    Task<Usuario> UpdateUsuario(Usuario usuario);
    Task<Usuario> DeleteUsuario(int id);
}
