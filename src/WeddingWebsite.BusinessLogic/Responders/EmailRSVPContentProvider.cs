using System;
using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public class EmailRSVPContentProvider : IEmailContentProvider
    {
        public IEmail Get(object model, IResponderDestination destination)
        {
            if (!(model is RSVP rsvp))
            {
                throw new NotSupportedException();
            }
            
            var attendWedding = (rsvp.Attendance.Wedding.GetValueOrDefault() ? "Will Attend Wedding" : "Will Not Attend");
            var attendCermony = (rsvp.Attendance.Cermony.GetValueOrDefault() ? "Will Attend Cermony / after party" : "Will Not Attend Cermony / after party");
            var subject = $"{rsvp.FirstName} {rsvp.LastName} Submitted RSVP - {attendWedding}!";

            var meat = rsvp.Food.Meat == null ? "Meat: no preference" : rsvp.Food.Meat.GetValueOrDefault() ? "Eats Meat" : "Do not eat meat";
            var fish = rsvp.Food.Fish == null ? "Fish: no preference" : rsvp.Food.Fish.GetValueOrDefault() ? "Eats Fish" : "Do not eat fish";
            var vegetarian = rsvp.Food.Vegetarian == null ? "Vegetarian: no preference" : rsvp.Food.Vegetarian.GetValueOrDefault() ? "Prefers Vegetarian food" : "Not vegetarian";
            var vegan = rsvp.Food.Vegan == null ? "Vegan: no preference" : rsvp.Food.Vegan.GetValueOrDefault() ? "Prefers Vegan food" : "Not vegan";
            var message = $"{attendWedding}<br/>{attendCermony}<br/><br/><br/>Message: {rsvp.Message}<br/><br/>{meat}<br/>{fish}<br/>{vegetarian}<br/>{vegan}<br/><br/><strong>Contact</strong><br/>E-mail: {rsvp.Email}<br/>Phone: {rsvp.Phone}";
            return new ResponderDestinationEmail(subject, message, destination);
        }
    }
}