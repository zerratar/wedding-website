using System;
using WeddingWebsite.BusinessLogic.Email;
using WeddingWebsite.BusinessLogic.Models;
using WeddingWebsite.BusinessLogic.Responders;

namespace WeddingWebsite.BusinessLogic.Providers
{
    public class EmailContactContentProvider : IEmailContentProvider
    {
        public IEmail Get(object model, IResponderDestination destination)
        {
            if (!(model is Contact contact))
            {
                throw new NotSupportedException();
            }

            var subject = $"{contact.Name} sent a message!";
            var message = $"{contact.Message}<br/><br/>E-mail: {contact.Email}";
            return new ResponderDestinationEmail(subject, message, destination);
        }
    }
}