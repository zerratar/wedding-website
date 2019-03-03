using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Repositories
{
    public class FileBasedCommentRepository : FileBasedRepository<Comment>, ICommentRepository
    {
        public FileBasedCommentRepository(IRepositorySettingsProvider settingsProvider)
            : base(settingsProvider)
        {
        }
    }
}