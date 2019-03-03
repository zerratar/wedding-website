using WeddingWebsite.BusinessLogic.Email;
using WeddingWebsite.BusinessLogic.Models;
using WeddingWebsite.BusinessLogic.Providers;

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