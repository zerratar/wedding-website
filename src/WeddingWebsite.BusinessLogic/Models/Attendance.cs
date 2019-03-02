namespace WeddingWebsite.BusinessLogic.Models
{
    public class Attendance
    {
        public Attendance(bool? wedding, bool? cermony)
        {
            Wedding = wedding;
            Cermony = cermony;
        }

        public bool? Wedding { get; }
        public bool? Cermony { get; }
    }
}