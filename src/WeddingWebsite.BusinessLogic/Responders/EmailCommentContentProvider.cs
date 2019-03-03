using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public class EmailCommentContentProvider : IEmailContentProvider
    {
        public IEmail Get(object model, IResponderDestination destination)
        {
            var subject = "Someone posted to the guestbook!";
            var message = "Someone posted to the guestbook!";
            if (model is Comment comment)
            {
                subject = $"{comment.Name} posted to the guestbook!";
                message = $"Following message was posted to the guestbook:<br/>{comment.Message}";
            }
            return new ResponderDestinationEmail(subject, message, destination);
        }
    }
}