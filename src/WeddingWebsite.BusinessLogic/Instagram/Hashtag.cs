using Newtonsoft.Json;

namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class Hashtag
    {
        public Hashtag(string id, string name, string profilePicUrl, EdgeCollection edgeHashtagToMedia, EdgeCollection edgeHashtagToTopPosts)
        {
            Id = id;
            Name = name;
            ProfilePicUrl = profilePicUrl;
            EdgeHashtagToMedia = edgeHashtagToMedia;
            EdgeHashtagToTopPosts = edgeHashtagToTopPosts;
        }

        public string Id { get; }
        public string Name { get; }

        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; }

        [JsonProperty("edge_hashtag_to_media")]
        public EdgeCollection EdgeHashtagToMedia { get; }

        [JsonProperty("edge_hashtag_to_top_posts")]
        public EdgeCollection EdgeHashtagToTopPosts { get; }
    }
}