using Authetication.Server.DTOs;

namespace Authetication.Server.Services;

public interface IFisioterapeutaService
{
    Task<IEnumerable<FisioterapeutaDto>> GetAllFisios();
    Task<FisioterapeutaDto> GetFisioById(int id);
    Task CreateFisioterapeuta(FisioterapeutaDto fisioterapeutaDto);
    Task UpdateFisioterapeuta(FisioterapeutaDto fisioterapeutaDto);
    Task DeleteFisioterapeuta(int id);
}
