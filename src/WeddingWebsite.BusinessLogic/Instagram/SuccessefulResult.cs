using System.Collections.Generic;
using System.Linq;

namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class SuccessefulResult : IInstagramResult
    {
        public SuccessefulResult(ResultData data)
        {
            var images = new List<InstagramImage>();
            foreach (var tagPage in data.EntryData.TagPage)
            {
                images.AddRange(tagPage.GraphQL.Hashtag.EdgeHashtagToMedia.Edges.Select(x =>
                    new InstagramImage(x.Node.DisplayUrl, x.Node.ThumbnailSrc)));
            }
            this.Images = images.ToArray();
        }

        public InstagramImage[] Images { get; }

        public bool Success => true;
    }
}