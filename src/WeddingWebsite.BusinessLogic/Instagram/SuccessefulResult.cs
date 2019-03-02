using System.Collections.Generic;
using System.Linq;

namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class SuccessefulResult : IInstagramResult
    {
        public SuccessefulResult(ResultData data)
        {
            //Data = data;

            var images = new List<InstagramImage>();
            foreach (var tagPage in data.EntryData.TagPage)
            {
                images.AddRange(tagPage.GraphQL.Hashtag.EdgeHashtagToMedia.Edges.Select(x =>
                    new InstagramImage(x.Node.DisplayUrl, x.Node.ThumbnailSrc)));
            }

            this.Images = images.ToArray();
        }

        //public ResultData Data { get; }

        public InstagramImage[] Images { get; }

        public bool Success => true;
    }

    public class InstagramImage
    {
        public InstagramImage(string displayUrl, string thumbnailUrl)
        {
            DisplayUrl = displayUrl;
            ThumbnailUrl = thumbnailUrl;
        }

        public string DisplayUrl { get; }
        public string ThumbnailUrl { get; }
    }
}