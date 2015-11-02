using System.Threading.Tasks;

namespace UNTv.WP81.Common.IO
{
    public interface ITextReader
    {
        Task<string> Read(string filename, string path = null);
    }
}
