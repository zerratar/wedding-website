namespace WeddingWebsite.BusinessLogic.Models
{
    public class Contact
    {
        public Contact(string name, string email, string message)
        {
            Name = name;
            Email = email;
            Message = message;
        }

        public string Name { get; }
        public string Email { get; }
        public string Message { get; }
    }
}