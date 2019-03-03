namespace WeddingWebsite.BusinessLogic.Email
{
    public interface IEmail
    {
        string Subject { get; }
        string Message { get; }
        string ToEmail { get; }
    }
}