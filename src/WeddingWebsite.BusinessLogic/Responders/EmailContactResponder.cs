using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public class EmailContactResponder : IContactResponder
    {
        public bool TrySend(Contact value, IResponderDestination destination)
        {
            return false;
        }
    }
}