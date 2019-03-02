namespace WeddingWebsite.BusinessLogic.Responders
{
    public interface IResponder<in TValue>
    {
        bool TrySend(TValue value, IResponderDestination destination);
    }
}