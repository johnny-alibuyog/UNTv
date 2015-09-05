using System.Threading.Tasks;
using UNTv.WP81.Data.Contracts.Messages;

namespace UNTv.WP81.Data.Contracts.Services
{
    public interface IStore
    {
        Task<TResponse> Get<TResponse>(IReturn<TResponse> request) where TResponse : class;
    }
}
