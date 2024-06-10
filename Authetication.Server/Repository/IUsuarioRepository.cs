using Authetication.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Repository;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAll();
    Task<Usuario> GetById(int id);
    Task CreateNewUsuario(Usuario usuario);
    Task UpdateUsuario(Usuario usuario);
    Task DeleteUsuario(int id);
    Task<Usuario> GetByUsername(string username);
}
