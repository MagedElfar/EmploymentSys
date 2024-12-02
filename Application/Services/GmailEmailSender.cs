using Core.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using Core.Exceptions;

namespace Application.Services
{
    public class GmailEmailSender : IEmailSender
    {
        private readonly GmailSmtpSettings smtpSettings;

        public GmailEmailSender(IOptions<GmailSmtpSettings> smtpSettings)
        {
            this.smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message
            };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

                    await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);

                    await client.SendAsync(emailMessage);
                }
                catch (Exception ex)
                {
                    throw new InternalServerErrorException(ex.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
