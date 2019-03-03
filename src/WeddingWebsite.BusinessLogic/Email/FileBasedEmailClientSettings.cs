namespace WeddingWebsite.BusinessLogic.Email
{
    public class FileBasedEmailClientSettings : IEmailClientSettings
    {
        public FileBasedEmailClientSettings()
        {
            var settingsFile = "smtp.conf";
            if (!System.IO.File.Exists(settingsFile))
            {
                return;
            }

            var data = System.IO.File.ReadAllLines(settingsFile);
            this.Server = data[0];
            this.Port = int.TryParse(data[1], out var port) ? port : 465;
            this.Email = data[2];
            this.Username = data[3];
            this.Password = data[4];
        }

        public string Server { get; }
        public string Email { get; }
        public int Port { get; }
        public string Username { get; }
        public string Password { get; }
    }
}