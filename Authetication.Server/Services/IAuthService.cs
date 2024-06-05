using System.Threading.Tasks;

namespace Authetication.Server.Services;

public interface IAuthService
{
    Task<string> Authenticate(string username, string password);
}
