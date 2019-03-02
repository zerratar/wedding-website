namespace WeddingWebsite.BusinessLogic.Repositories
{
    public class JsonRepositorySettingsProvider : IRepositorySettingsProvider
    {
        public IRepositorySettings Get(string name)
        {
            return new RepositorySettings(
                System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "repositories", $"{name}.json"));
        }
    }
}