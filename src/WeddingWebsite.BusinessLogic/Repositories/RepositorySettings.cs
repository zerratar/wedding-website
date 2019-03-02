namespace WeddingWebsite.BusinessLogic.Repositories
{
    public class RepositorySettings : IRepositorySettings
    {
        public RepositorySettings(string source)
        {
            Source = source;
        }

        public string Source { get; }
    }
}