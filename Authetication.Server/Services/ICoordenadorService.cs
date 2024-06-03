using Authetication.Server.DTOs;

namespace Authetication.Server.Services;

public interface ICoordenadorService
{
    Task<IEnumerable<CoordenadorDto>> GetAllCoords();
    Task<CoordenadorDto> GetCoordById(int id);
    Task CreateCoordenador(CoordenadorDto coordenadorDto);
    Task UpdateCoordenador(CoordenadorDto coordenadorDto);
    Task DeleteCoordenador(int id);
}
