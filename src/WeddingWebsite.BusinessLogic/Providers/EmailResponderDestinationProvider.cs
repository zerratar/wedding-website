using WeddingWebsite.BusinessLogic.Responders;

namespace WeddingWebsite.BusinessLogic.Providers
{
    public class EmailResponderDestinationProvider : IResponderDestinationProvider
    {
        public IResponderDestination Get(string data)
        {
            return new EmailResponderDestination(data);
        }
    }
}