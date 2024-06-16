using Authetication.Server.Api.DTOs;

namespace Authetication.Server.Api.Services;

public interface IFisioterapeutaService
{
    Task<IEnumerable<FisioterapeutaDto>> GetAllFisios();
    Task<FisioterapeutaDto> GetFisioById(int id);
    Task CreateFisioterapeuta(FisioterapeutaDto fisioterapeutaDto);
    Task UpdateFisioterapeuta(FisioterapeutaDto fisioterapeutaDto);
    Task DeleteFisioterapeuta(int id);
}
