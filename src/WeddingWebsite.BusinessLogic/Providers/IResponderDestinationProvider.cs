using WeddingWebsite.BusinessLogic.Responders;

namespace WeddingWebsite.BusinessLogic.Providers
{
    public interface IResponderDestinationProvider
    {
        IResponderDestination Get(string data);
    }
}
