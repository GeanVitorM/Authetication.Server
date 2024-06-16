using Authetication.Server.Api.Models;

namespace Authetication.Server.Api.Repository;

public interface IFisioterapeutaRepository
{
    Task<IEnumerable<Fisioterapeuta>> GetAll();
    Task<Fisioterapeuta> GetById(int id);
    Task<Fisioterapeuta> CreateNewFisioterapeuta(Fisioterapeuta fisioterapeuta);
    Task<Fisioterapeuta> UpdateFisioterapeuta(Fisioterapeuta fisioterapeuta);
    Task<Fisioterapeuta> DeleteFisioterapeuta(int id);
}