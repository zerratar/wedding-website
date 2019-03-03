using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public interface IEmailClient
    {
        Task SendAsync(IEmail email);
    }
}