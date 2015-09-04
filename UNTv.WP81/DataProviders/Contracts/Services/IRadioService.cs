using System;
using System.Threading.Tasks;
using UNTv.WP81.DataProviders.Contracts.Messages;

namespace UNTv.WP81.DataProviders.Contracts.Services
{
    public interface IRadioService
    {
        Task<RadioProgramsResponse> Get(RadioProgramsRequest request);
        Task<RadioProgramSchedulesResponse> Get(RadioProgramSchedulesRequest request);
    }
}
