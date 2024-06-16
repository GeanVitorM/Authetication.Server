namespace Authetication.Server.Api.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}
