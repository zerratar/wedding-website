namespace WeddingWebsite.BusinessLogic.Instagram
{
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