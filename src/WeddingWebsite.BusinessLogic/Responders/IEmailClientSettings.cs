namespace WeddingWebsite.BusinessLogic.Responders
{
    public interface IEmailClientSettings
    {
        string Server { get; }
        string Email { get; }
        string Username { get; }
        string Password { get; }
    }
}