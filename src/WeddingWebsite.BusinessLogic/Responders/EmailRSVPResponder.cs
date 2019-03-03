using System;
using WeddingWebsite.BusinessLogic.Models;

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