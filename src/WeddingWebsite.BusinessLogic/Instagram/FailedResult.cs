namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class FailedResult : IInstagramResult
    {
        public FailedResult(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public bool Success => false;
    }
}