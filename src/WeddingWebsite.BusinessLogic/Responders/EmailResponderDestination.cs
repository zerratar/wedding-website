namespace WeddingWebsite.BusinessLogic.Responders
{
    public class EmailResponderDestination : ResponderDestinationBase
    {
        public EmailResponderDestination(string email)
            : base(email)
        {
        }

        public string Email => ((IResponderDestination)this).Data;
    }
}