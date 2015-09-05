using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNTv.WP81.Data.Contracts.Messages;

namespace UNTv.WP81.Data.Contracts.Services
{
    public class RadioService : ServiceBase, IRadioService
    {
        //private readonly int _count = 20;

        public RadioService()
        {
            this.BaseAddress = new Uri("http://www.untvradio.com/");
        }

        public async Task<RadioProgramMessage.Response> Get(RadioProgramMessage.Request request)
        {
            var query = string.Format("/api/programs/get_programs/?callback=?");
            return await Get<RadioProgramMessage.Response>(query, request.ToString());
        }

        public async Task<RadioProgramScheduleMessage.Response> Get(RadioProgramScheduleMessage.Request request)
        {
            var query = string.Format("/api/programs/get_all_programs/?callback=?");
            return await Get<RadioProgramScheduleMessage.Response>(query);
        }
    }
}
