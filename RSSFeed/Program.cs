using RSSFeed.Classes;
using System;

namespace RSSFeed
{
    class Program
    {
        static void Main()
        {
            Run();
            Console.ReadKey();
        }
        static void Run()
        {
            RSSFeedReader reader = new RSSFeedReader(new ConfigurationFile());
            reader.Add("CNN", "http://rss.cnn.com/rss/edition.rss");
            reader.Add("BBC", "http://feeds.bbci.co.uk/news/rss.xml?edition=uk");
            reader.Download();
        }
    }
}
