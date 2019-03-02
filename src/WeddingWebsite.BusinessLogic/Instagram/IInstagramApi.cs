using System.Threading.Tasks;

namespace WeddingWebsite.BusinessLogic.Instagram
{
    public interface IInstagramApi
    {
        Task<IInstagramResult> GetByTagAsync(string tagName);
    }
}