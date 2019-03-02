using Newtonsoft.Json;

namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class Node
    {
        public Node(string id, string shortCode, Dimensions dimensions, string displayUrl, string thumbnailSrc)
        {
            Id = id;
            ShortCode = shortCode;
            Dimensions = dimensions;
            DisplayUrl = displayUrl;
            ThumbnailSrc = thumbnailSrc;
        }

        public string Id { get; }
        public string ShortCode { get; }
        public Dimensions Dimensions { get; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; }

        [JsonProperty("thumbnail_src")]
        public string ThumbnailSrc { get; }
    }
}