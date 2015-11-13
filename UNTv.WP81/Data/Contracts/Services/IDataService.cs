using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNTv.WP81.Data.Contracts.Messages;

namespace UNTv.WP81.Data.Contracts.Services
{
    public interface IDataService
    {
        Task<TResponse> Get<TResponse>(IReturn<TResponse> request) where TResponse : class;
    }
}
