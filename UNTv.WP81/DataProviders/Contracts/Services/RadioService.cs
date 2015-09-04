using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNTv.WP81.DataProviders.Contracts.Messages;

namespace UNTv.WP81.DataProviders.Contracts.Services
{
    public class RadioService : ServiceBase, IRadioService
    {
        //private readonly int _count = 20;

        public RadioService()
        {
            this.BaseAddress = new Uri("http://www.untvradio.com/");
        }

        public async Task<RadioProgramsResponse> Get(RadioProgramsRequest request)
        {
            var query = string.Format("/api/programs/get_programs/?callback=?");
            return await Get<RadioProgramsResponse>(query);
        }

        public async Task<RadioProgramSchedulesResponse> Get(RadioProgramSchedulesRequest request)
        {
            var query = string.Format("/api/programs/get_all_programs/?callback=?");
            return await Get<RadioProgramSchedulesResponse>(query);
        }
    }
}
