using Authetication.Server.Models;

namespace Authetication.Server.Repository;

public interface ICoordenadorRepository
{
    Task<IEnumerable<Coordenador>> GetAll();
    Task<Coordenador> GetById(int id);
    Task<Coordenador> CreateNewCoordenador(Coordenador coordenador);
    Task<Coordenador> UpdateCoordenador(Coordenador coordenador);
    Task<Coordenador> DeleteCoordenador(int id);
}
