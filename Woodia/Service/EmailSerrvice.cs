using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService(IOptions<SendGridSetting> SendGridSetting ,ILogger<EmailService> logger) : IEmailSender
{
    private readonly SendGridSetting _sendGridSettings = SendGridSetting.Value;
    private readonly ILogger<EmailService> _logger = logger;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
         try
        {
            var client = new SendGridClient(_sendGridSettings.ApiKey);
            _logger.LogInformation("ApiKey : {ApiKey} SenderEmail : {SenderEmail}",_sendGridSettings.ApiKey, _sendGridSettings.SenderEmail);
            var from = new EmailAddress(_sendGridSettings.SenderName, _sendGridSettings.SenderEmail);
            var to = new EmailAddress(email);

            _logger.LogInformation("Toooooooooooooooo {to}", to);
            
            var msg = MailHelper.CreateSingleEmail(
                from, 
                to, 
                subject, 
                plainTextContent: null,
                htmlContent: htmlMessage
            );

            var response = await client.SendEmailAsync(msg);

            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}