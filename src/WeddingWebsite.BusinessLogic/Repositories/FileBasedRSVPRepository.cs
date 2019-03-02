using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Repositories
{
    public class FileBasedRSVPRepository : IRSVPRepository
    {
        public bool TryStore(RSVP item)
        {
            return true;
        }
    }
}
