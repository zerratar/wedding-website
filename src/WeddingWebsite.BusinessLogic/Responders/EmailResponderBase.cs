using System.Threading.Tasks;
using WeddingWebsite.BusinessLogic.Email;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public abstract class EmailResponderBase<T> : IResponder<T>
    {
        private readonly IEmailClient client;
        private readonly IEmailContentProvider contentProvider;

        protected EmailResponderBase(
            IEmailClient client,
            IEmailContentProvider contentProvider)
        {
            this.client = client;
            this.contentProvider = contentProvider;
        }

        public async Task<bool> TrySendAsync(T model, IResponderDestination destination)
        {
            try
            {
                var email = this.contentProvider.Get(model, destination);
                await this.client.SendAsync(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}