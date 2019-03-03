using System.Threading.Tasks;

namespace WeddingWebsite.BusinessLogic.Email
{
    public interface IEmailClient
    {
        Task SendAsync(IEmail email);
    }
}