namespace WeddingWebsite.BusinessLogic.Email
{
    public interface IEmailClientSettings
    {
        string Server { get; }
        string Email { get; }
        int Port { get; }
        string Username { get; }
        string Password { get; }
    }
}