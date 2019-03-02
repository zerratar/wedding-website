using System;
using System.Collections.Generic;
using System.Text;
using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Repositories
{
    public interface IRSVPRepository
    {
        bool TryStore(RSVP item);
    }
}
