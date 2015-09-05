using System;
using System.Threading.Tasks;
using UNTv.WP81.Data.Contracts.Messages;

namespace UNTv.WP81.Data.Contracts.Services
{
    public interface IRadioService
    {
        Task<RadioProgramMessage.Response> Get(RadioProgramMessage.Request request);
        Task<RadioProgramScheduleMessage.Response> Get(RadioProgramScheduleMessage.Request request);
    }
}
