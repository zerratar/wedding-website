using System;

namespace WeddingWebsite.BusinessLogic.Models
{
    public class Comment
    {
        public Comment(string name, string email, string message, DateTime date, bool accepted)
        {
            Name = name;
            Email = email;
            Message = message;
            Date = date;
            Accepted = accepted;
        }

        public string Name { get; }
        public string Email { get; }
        public string Message { get; }        
        public DateTime Date { get; }
        public bool Accepted { get; }
    }
}