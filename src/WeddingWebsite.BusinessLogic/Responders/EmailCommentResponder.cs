using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public class EmailCommentResponder : ICommentResponder
    {
        public bool TrySend(Comment value, IResponderDestination destination)
        {
            return false;
        }
    }
}