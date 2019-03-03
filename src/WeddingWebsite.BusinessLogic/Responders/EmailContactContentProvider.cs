using System;
using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Responders
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