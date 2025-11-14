using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService(IOptions<SendGridSetting> SendGridSetting) : IEmailSender
{
    private readonly SendGridSetting _sendGridSettings = SendGridSetting.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
         try
        {
            var client = new SendGridClient(_sendGridSettings.ApiKey);
            
            var from = new EmailAddress(_sendGridSettings.SenderEmail, _sendGridSettings.SenderName);
            var to = new EmailAddress(email);
            
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