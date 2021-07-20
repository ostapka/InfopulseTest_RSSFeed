using RSSFeed.Interfaces;
using System.Text;

namespace RSSFeedReaderTests.Classes
{
    class LoaderStub : ILoadable
    {
        public StringBuilder Load(string url)
        {
            StringBuilder response= new StringBuilder();
            response.Append("Hello");
            return response;
        }
    }
}
