using RSSFeed.Interfaces;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace RSSFeed.Classes
{
    public class Loader : ILoadable
    {
        private XmlReader reader;
        public StringBuilder Load(string url)
        {
            reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            StringBuilder response = new StringBuilder();
            foreach (SyndicationItem item in feed.Items)
            {
                response.Append(item.Title.Text + "\n");

            }
            return response;
        }
    }
}
