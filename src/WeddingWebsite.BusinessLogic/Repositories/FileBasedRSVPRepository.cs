using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Repositories
{
    public class FileBasedRSVPRepository : FileBasedRepository<RSVP>, IRSVPRepository
    {
        public FileBasedRSVPRepository(IRepositorySettingsProvider settingsProvider) 
            : base(settingsProvider)
        {
        }
    }
}
