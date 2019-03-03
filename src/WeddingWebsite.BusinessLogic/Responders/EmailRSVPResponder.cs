using System;
using WeddingWebsite.BusinessLogic.Email;
using WeddingWebsite.BusinessLogic.Models;
using WeddingWebsite.BusinessLogic.Providers;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public class EmailRSVPResponder : EmailResponderBase<RSVP>, IRSVPResponder
    {
        public EmailRSVPResponder(IEmailClient client) 
            : base(client, new EmailRSVPContentProvider())
        {
        }
    }
}