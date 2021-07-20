using System.Text;

namespace RSSFeed.Interfaces
{
    public interface ILoadable
    {
        StringBuilder Load(string url);
    }
}
