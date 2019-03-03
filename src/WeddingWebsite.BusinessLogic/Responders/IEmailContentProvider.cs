namespace WeddingWebsite.BusinessLogic.Responders
{
    public interface IEmailContentProvider
    {
        IEmail Get(object item, IResponderDestination destination);
    }
}