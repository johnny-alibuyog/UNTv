using System.Threading.Tasks;

namespace UNTv.WP81.Common.IO
{
    public interface ITextWriter
    {
        Task Write(string filename, string content);
    }
}
