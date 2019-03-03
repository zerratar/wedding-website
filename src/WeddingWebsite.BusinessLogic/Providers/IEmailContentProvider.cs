using WeddingWebsite.BusinessLogic.Email;
using WeddingWebsite.BusinessLogic.Responders;

namespace WeddingWebsite.BusinessLogic.Providers
{
    public interface IEmailContentProvider
    {
        IEmail Get(object item, IResponderDestination destination);
    }
}