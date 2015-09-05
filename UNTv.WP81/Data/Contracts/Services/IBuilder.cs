using UNTv.WP81.Data.Contracts.Messages;

namespace UNTv.WP81.Data.Contracts.Services
{
    public interface IBuilder
    {
        string BuildUri<T>(IReturn<T> request) where T : class;
        string BuildFilename<T>(IReturn<T> request) where T : class;
    }
}
