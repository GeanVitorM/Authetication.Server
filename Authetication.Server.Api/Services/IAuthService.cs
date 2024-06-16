using System.Threading.Tasks;

namespace Authetication.Server.Api.Services;

public interface IAuthService
{
    Task<string> Authenticate(string username, string password);
}
