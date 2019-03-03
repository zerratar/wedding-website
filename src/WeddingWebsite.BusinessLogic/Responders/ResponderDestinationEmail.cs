namespace WeddingWebsite.BusinessLogic.Responders
{
    public class ResponderDestinationEmail : IEmail
    {
        public ResponderDestinationEmail(string subject, string message, IResponderDestination destination)
        {
            this.Subject = subject;
            this.Message = message;
            this.ToEmail = destination.Data;
        }

        public string Subject { get; }
        public string Message { get; }
        public string ToEmail { get; }
    }
}