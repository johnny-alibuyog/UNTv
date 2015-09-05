using System.Threading.Tasks;
using UNTv.WP81.Data.Contracts.Messages;

namespace UNTv.WP81.Data.Contracts.Services
{
    public interface ITelevisionService
    {
        Task<NewsMessage.Response> Get(NewsMessage.Request request);
        Task<VideoMessage.Response> Get(VideoMessage.Request request);
        Task<PublicServiceMessage.Response> Get(PublicServiceMessage.Request request);
        Task<TelevisionProgramMessage.Response> Get(TelevisionProgramMessage.Request request);
        Task<TelevisionProgramScheduleMessage.Response> Get(TelevisionProgramScheduleMessage.Request request);
    }
}
