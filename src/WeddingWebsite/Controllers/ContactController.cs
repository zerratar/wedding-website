using Microsoft.AspNetCore.Mvc;
using WeddingWebsite.BusinessLogic.Models;
using WeddingWebsite.BusinessLogic.Providers;
using WeddingWebsite.BusinessLogic.Responders;

namespace WeddingWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactResponder responder;
        private readonly IResponderDestinationProvider destinationProvider;
        private readonly ISettings settings;

        public ContactController(
            IContactResponder responder,
            IResponderDestinationProvider destinationProvider,
            ISettings settings)
        {
            this.responder = responder;
            this.destinationProvider = destinationProvider;
            this.settings = settings;
        }

        [HttpPost]
        public void Post(Contact contact)
        {
            foreach (var email in settings.ResponseEmails)
            {
                var destination = this.destinationProvider.Get(email);
                responder.TrySend(contact, destination);
            }
        }
    }
}