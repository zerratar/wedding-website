namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class EntryData
    {
        public EntryData(TagPage[] tagPage)
        {
            TagPage = tagPage;
        }

        public TagPage[] TagPage { get; }
    }
}