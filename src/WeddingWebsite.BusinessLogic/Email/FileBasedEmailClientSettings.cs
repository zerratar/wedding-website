namespace WeddingWebsite.BusinessLogic.Responders
{
    public class FileBasedEmailClientSettings : IEmailClientSettings
    {
        public FileBasedEmailClientSettings()
        {
            var settingsFile = "email-server.conf";
            if (!System.IO.File.Exists(settingsFile))
            {
                return;
            }

            var data = System.IO.File.ReadAllLines(settingsFile);
            this.Server = data[0];
            this.Email = data[1];
            this.Username = data[2];
            this.Password = data[3];
        }

        public string Server { get; }
        public string Email { get; }
        public string Username { get; }
        public string Password { get; }
    }
}