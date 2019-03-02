namespace WeddingWebsite.BusinessLogic.Responders
{
    public abstract class ResponderDestinationBase : IResponderDestination
    {
        private readonly string data;
        protected ResponderDestinationBase(string data)
        {
            this.data = data;
        }

        string IResponderDestination.Data => data;
    }
}