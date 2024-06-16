using Authetication.Server.Api.DTOs;

namespace Authetication.Server.Api.Services;

public interface ICoordenadorService
{
    Task<IEnumerable<CoordenadorDto>> GetAllCoords();
    Task<CoordenadorDto> GetCoordById(int id);
    Task CreateCoordenador(CoordenadorDto coordenadorDto);
    Task UpdateCoordenador(CoordenadorDto coordenadorDto);
    Task DeleteCoordenador(int id);
}
