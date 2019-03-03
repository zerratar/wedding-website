namespace WeddingWebsite.BusinessLogic.Responders
{
    public interface IEmail
    {
        string Subject { get; }
        string Message { get; }
        string ToEmail { get; }
    }
}