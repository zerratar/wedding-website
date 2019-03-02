using Newtonsoft.Json;

namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class TagPage
    {
        public TagPage(GraphQL graphql)
        {
            this.GraphQL = graphql;
        }

        [JsonProperty("graphql")]
        public GraphQL GraphQL { get; }
    }
}