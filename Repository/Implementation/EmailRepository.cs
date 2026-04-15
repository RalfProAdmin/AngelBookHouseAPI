using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Repository.Interface;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace EkartAPI.Repository.Implementation
{
    public class EmailRepository : IEmailInterface
    {
        private readonly EkartDBcontext _dbContext;
        private readonly SendGridSettings _settings;

        public EmailRepository(
            EkartDBcontext dbContext,
            IOptions<SendGridSettings> options)
        {
            _dbContext = dbContext;
            _settings = options.Value;
        }

        public async Task<bool> SendEmailAsync(
     string toEmail,
     string subject,
     string body,
     string senderName,
     string senderEmail)
        {
            try
            {
                var client = new SendGridClient(_settings.ApiKey);

                // FROM → must be verified in SendGrid
                var from = new EmailAddress(
                    _settings.FromEmail,
                    _settings.FromName
                );

                var to = new EmailAddress(toEmail);

                var msg = MailHelper.CreateSingleEmail(
                    from,
                    to,
                    subject,
                    body,
                    body
                );

                // ✅ THIS IS THE MISSING PART
                // When user/admin clicks "Reply", mail goes here
                if (!string.IsNullOrWhiteSpace(senderEmail))
                {
                    msg.SetReplyTo(new EmailAddress(senderEmail, senderName));
                }

                var response = await client.SendEmailAsync(msg);

                return response.StatusCode == System.Net.HttpStatusCode.Accepted;
            }
            catch (Exception ex)
            {
                _dbContext.ErrorLogs.Add(new ErrorLog
                {
                    ErrorMessage = ex.Message,
                    StackTrace = ex.ToString(),
                    Module = "SendGrid Email",
                    ErrorType = "Email",
                    CreatedAt = DateTime.UtcNow
                });

                await _dbContext.SaveChangesAsync();
                return false;
            }
        }
    }
    }
