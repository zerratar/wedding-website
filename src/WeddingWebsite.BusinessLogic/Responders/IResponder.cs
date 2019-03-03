using System.Threading.Tasks;

namespace WeddingWebsite.BusinessLogic.Responders
{
    public interface IResponder
    {        
    }

    public interface IResponder<in TValue> : IResponder
    {
        Task<bool> TrySendAsync(TValue model, IResponderDestination destination);
    }
}