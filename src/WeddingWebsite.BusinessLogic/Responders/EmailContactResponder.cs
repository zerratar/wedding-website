using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public class EmailContactResponder : EmailResponderBase<Contact>, IContactResponder
    {
        public EmailContactResponder(IEmailClient client) 
            : base(client, new EmailContactContentProvider())
        {
        }
    }
}