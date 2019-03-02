namespace WeddingWebsite.BusinessLogic.Models
{
    public class RSVP
    {
        public RSVP(string firstName, string lastName, string email, string phone, string message, Attendance attendance, Food food)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Message = message;
            Attendance = attendance;
            Food = food;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Phone { get; }
        public string Message { get; }
        public Attendance Attendance { get; }
        public Food Food { get; }
    }
}
