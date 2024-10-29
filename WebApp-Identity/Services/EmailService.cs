using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using WebApp_Identity.Settings;
using Task = System.Threading.Tasks.Task;

namespace WebApp_Identity.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<SmtpSetting> smtpSetting;

        public EmailService(IOptions<SmtpSetting> smtpSetting)
        {
            this.smtpSetting = smtpSetting;
        }

        public async Task SendAsync(string from, string to, string subject, string body)
        {
            var apiInstance = new TransactionalEmailsApi();

            SendSmtpEmailSender Email = new SendSmtpEmailSender(smtpSetting.Value.User, smtpSetting.Value.User);
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(to, to);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);


            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, null, body, subject);
                CreateSmtpEmail result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
