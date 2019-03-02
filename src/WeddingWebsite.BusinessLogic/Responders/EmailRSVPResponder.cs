using System;
using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public class EmailRSVPResponder : IRSVPResponder
    {
        public bool TrySend(RSVP value, IResponderDestination destination)
        {
            if (value.Email.Equals(destination.Data, StringComparison.CurrentCultureIgnoreCase))
            {
                return TrySendConfirmation(value, destination);
            }

            return TryNotifyOwner(value, destination);
        }

        private bool TrySendConfirmation(RSVP value, IResponderDestination destination)
        {
            // person submitted RSVP, send back a confirmation
            return false;
        }

        private bool TryNotifyOwner(RSVP value, IResponderDestination destination)
        {
            // person submitted RSVP, notify owner of the website 
            return false;
        }
    }
}