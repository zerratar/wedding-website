using System;
using Microsoft.AspNetCore.Mvc;
using WeddingWebsite.BusinessLogic.Models;
using WeddingWebsite.BusinessLogic.Providers;
using WeddingWebsite.BusinessLogic.Repositories;
using WeddingWebsite.BusinessLogic.Responders;

namespace WeddingWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RSVPController : ControllerBase
    {
        private readonly IRSVPRepository repo;
        private readonly IRSVPResponder responder;
        private readonly IResponderDestinationProvider destinationProvider;
        private readonly ISettings settings;

        public RSVPController(
            IRSVPRepository repo,
            IRSVPResponder responder,
            IResponderDestinationProvider destinationProvider,
            ISettings settings)
        {
            this.repo = repo;
            this.responder = responder;
            this.destinationProvider = destinationProvider;
            this.settings = settings;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello, world!";
        }

        [HttpPost]
        public string Post(RSVP rsvp)
        {
            if (!repo.TryStore(rsvp))
            {
                return "RSVP Failed";
            }

            // send confirmation
            var confirmationDestination = destinationProvider.Get(rsvp.Email);
            responder.TrySend(rsvp, confirmationDestination);

            // notify owner
            foreach (var ownerEmail in settings.ResponseEmails)
            {
                var ownerDestination = destinationProvider.Get(ownerEmail);
                responder.TrySend(rsvp, ownerDestination);
            }
            return "RSVP Submitted";
        }
    }
}