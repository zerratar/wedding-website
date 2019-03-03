using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace WeddingWebsite.BusinessLogic.Email
{
    public class EmailClient : IEmailClient
    {
        private readonly IEmailClientSettings settings;
        private readonly SmtpClient service;

        public EmailClient(IEmailClientSettings settings)
        {
            this.settings = settings;
            this.service = new MailKit.Net.Smtp.SmtpClient();
        }

        public async Task SendAsync(IEmail email)
        {
            var mimeMessage = CreateMimeMessage(email);
            if (!this.service.IsConnected)
            {
                await this.service.ConnectAsync(settings.Server);
            }

            if (!this.service.IsAuthenticated)
            {
                await this.service.AuthenticateAsync(settings.Username, settings.Password);
            }

            await this.service.SendAsync(mimeMessage);
        }

        private MimeMessage CreateMimeMessage(IEmail email)
        {
            var msg = new MimeMessage();

            msg.From.Add(InternetAddress.Parse(ParserOptions.Default, this.settings.Email));
            msg.To.Add(InternetAddress.Parse(ParserOptions.Default, email.ToEmail));
            msg.Subject = email.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = email.Message };
            msg.Body = bodyBuilder.ToMessageBody();

            return msg;
        }
    }
}