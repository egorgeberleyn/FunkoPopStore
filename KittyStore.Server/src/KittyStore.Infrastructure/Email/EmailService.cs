using System.Net.Mail;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Email;
using KittyStore.Domain.Common.Errors;
using MailKit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace KittyStore.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly EmailOptions _emailOptions;

    public EmailService(ILogger<EmailService> logger, IOptions<EmailOptions> emailOptions)
    {
        _logger = logger;
        _emailOptions = emailOptions.Value;
    }

    public async Task<ErrorOr<Success>> SendAsync(string email, string subject, string message)
    {
        if (!IsValidEmail(email))
            return Errors.Email.EmailNotValid;
        
        using var client = new SmtpClient(new ProtocolLogger(EmailOptions.SmtpLogFilePath));
        await client.ConnectAsync(_emailOptions.SmtpAddress, _emailOptions.SmtpPort, true);
        await client.AuthenticateAsync(_emailOptions.Email, _emailOptions.Password);
        
        var emailMessage = new MimeMessage() 
        { 
            Subject = subject, 
            Body = new TextPart(TextFormat.Html) 
            {
                Text = message
            }
        };
        emailMessage.From.Add(new MailboxAddress(_emailOptions.EmailName, _emailOptions.Email));
        emailMessage.To.Add(new MailboxAddress("", email));
        
        try
        {
            await client.SendAsync(emailMessage);
        }
        catch (Exception)
        {
            _logger.LogError("Sending a message: \'{Message}\' to: \'{Email}\' failed...", message, email);
            return Errors.Email.EmailNotFound;
        }

        await client.DisconnectAsync(true);
        _logger.LogInformation("Sending an email to: \'{Email}\', message: \'{Message}\'...", email, message);
        return new Success();
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}