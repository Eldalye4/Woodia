using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailGoogleService(IOptions<EmailGoogleSetting> SendGridSetting) : IEmailSender
{
    private readonly EmailGoogleSetting _sendGridSettings = SendGridSetting.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
         try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_sendGridSettings.SenderName, _sendGridSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_sendGridSettings.Host, _sendGridSettings.Port, SecureSocketOptions.StartTls);
                
                await client.AuthenticateAsync(
                    "ab4199001@smtp-brevo.com",
                    _sendGridSettings.Password);
                
                await client.SendAsync(message);
                
                await client.DisconnectAsync(true);
            }

            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}