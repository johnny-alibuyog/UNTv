using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTv.WP81.Data.Stores
{
    public interface IDataStore
    {
        Task<bool> Exists(string filename);
        Task<string> Get(string filename);
        Task Save(string filename, string data);
    }
}
