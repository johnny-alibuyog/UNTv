using System.Threading.Tasks;

namespace UNTv.WP81.DataProviders.Services
{
    public interface ITelevisionService
    {
        Task<NewsResponse> Get(NewsRequest request);
        Task<VideosResponse> Get(VideosRequest request);
        Task<TelevisionProgramsResponse> Get(TelevisionProgramsRequest request);
        Task<TelevisionProgramSchedulesResponse> Get(TelevisionProgramSchedulesRequest request);
    }
}
