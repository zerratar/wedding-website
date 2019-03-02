namespace WeddingWebsite.BusinessLogic.Repositories
{
    public interface IRepositorySettingsProvider
    {
        IRepositorySettings Get(string name);
    }
}