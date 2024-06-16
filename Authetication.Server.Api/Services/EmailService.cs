using SendGrid.Helpers.Mail;
using SendGrid;

namespace Authetication.Server.Api.Services;

public class EmailService : IEmailService
{
    private readonly string _sendGridApiKey;

    public EmailService(IConfiguration configuration)
    {
        _sendGridApiKey = configuration["SendGridApiKey"];
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var client = new SendGridClient(_sendGridApiKey);
        var from = new EmailAddress("no-reply@yourapp.com", "Your App Name");
        var toEmail = new EmailAddress(to);
        var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, body, body);

        var response = await client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to send email: {response.StatusCode}");
        }
    }
}
