using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingWebsite.BusinessLogic.Instagram;

namespace WeddingWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstagramController : ControllerBase
    {
        private readonly IInstagramApi instagram;

        public InstagramController(IInstagramApi instagram)
        {
            this.instagram = instagram;
        }

        [HttpGet("{tag}")]
        public async Task<InstagramImage[]> GetAsync(string tag)
        {
            var result = await instagram.GetByTagAsync(tag);
            if (result is SuccessefulResult success)
            {
                return success.Images;
            }

            return new InstagramImage[0];
        }
    }
}