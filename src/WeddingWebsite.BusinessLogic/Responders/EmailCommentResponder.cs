using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public class EmailCommentResponder : EmailResponderBase<Comment>, ICommentResponder
    {
        public EmailCommentResponder(IEmailClient client) 
            : base(client, new EmailCommentContentProvider())
        {
        }
    }
}