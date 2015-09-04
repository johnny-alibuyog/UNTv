using System.Threading.Tasks;
using UNTv.WP81.DataProviders.Contracts.Messages;

namespace UNTv.WP81.DataProviders.Contracts.Services
{
    public interface ITelevisionService
    {
        Task<NewsResponse> Get(NewsRequest request);
        Task<VideosResponse> Get(VideosRequest request);
        Task<PublicServicesResponse> Get(PublicServicesRequest request);
        Task<TelevisionProgramsResponse> Get(TelevisionProgramsRequest request);
        Task<TelevisionProgramSchedulesResponse> Get(TelevisionProgramSchedulesRequest request);
    }
}
