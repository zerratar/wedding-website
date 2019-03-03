using WeddingWebsite.BusinessLogic.Email;
using WeddingWebsite.BusinessLogic.Models;
using WeddingWebsite.BusinessLogic.Providers;

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