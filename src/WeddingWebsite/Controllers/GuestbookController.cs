using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WeddingWebsite.BusinessLogic.Models;
using WeddingWebsite.BusinessLogic.Providers;
using WeddingWebsite.BusinessLogic.Repositories;
using WeddingWebsite.BusinessLogic.Responders;

namespace WeddingWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestbookController : ControllerBase
    {
        private readonly ICommentResponder responder;
        private readonly ICommentRepository repository;
        private readonly IResponderDestinationProvider destinationProvider;
        private readonly ISettings settings;

        public GuestbookController(
            ICommentResponder responder,
            ICommentRepository repository,
            IResponderDestinationProvider destinationProvider,
            ISettings settings)
        {
            this.responder = responder;
            this.repository = repository;
            this.destinationProvider = destinationProvider;
            this.settings = settings;
        }

        [HttpPost]
        public void Post(Comment comment)
        {
            comment = new Comment(
                comment.Name,
                comment.Email,
                comment.Message,
                DateTime.UtcNow,
                false);

            if (!this.repository.TryStore(comment))
            {
                return;
            }

            foreach (var ownerEmail in this.settings.ResponseEmails)
            {
                var destination = destinationProvider.Get(ownerEmail);
                responder.TrySend(comment, destination);
            }
        }

        [HttpGet]
        public IReadOnlyList<Comment> Get()
        {
            var comments = repository.All();
            return comments.Where(x => x.Accepted).ToArray();
        }

        [HttpGet("approve/{date}")]
        public void Approve(DateTime date)
        {
            var comment = repository.Find(x => x.Date == date.ToUniversalTime());
            if (comment == null)
            {
                return;
            }
            var newComment = new Comment(
                comment.Name,
                comment.Email,
                comment.Message,
                comment.Date,
                true);
            this.repository.Replace(comment, newComment);
        }

        [HttpGet("all")]
        public IReadOnlyList<Comment> All()
        {
            return repository.All();
        }
    }
}