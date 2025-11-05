using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

public class EmailService(IOptions<SmtpSetting> smtpSettings) : IEmailSender
{
    private readonly SmtpSetting _smtpSetting = smtpSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Woodia", _smtpSetting.Mail));
        message.Sender = new MailboxAddress("Woodia", _smtpSetting.Mail);
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;
        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };
        message.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_smtpSetting.Server, _smtpSetting.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_smtpSetting.Login, _smtpSetting.Password);
        await smtp.SendAsync(message);
        smtp.Disconnect(true);
    }
}