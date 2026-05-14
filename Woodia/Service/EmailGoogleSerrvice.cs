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
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("api-key", _sendGridSettings.Password);

            var payload = new
            {
                sender = new { name = _sendGridSettings.SenderName, email = _sendGridSettings.SenderEmail },
                to = new[] { new { email = email } },
                subject = subject,
                htmlContent = htmlMessage
            };

            var response = await _httpClient.PostAsJsonAsync(
                "https://api.brevo.com/v3/smtp/email", payload);

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Brevo response: {response.StatusCode} - {body}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}