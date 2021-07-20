using RSSFeed.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RSSFeed.Classes
{
    public class RSSFeedReader
    {
        private IFile file;
        private XDocument xdoc;
        private XElement rssFeeds;
        private ILoadable loader = new Loader();
        public XDocument Xdoc
        {
            get
            {
                return xdoc;
            }
            set
            {
                xdoc = value;
            }
        }
        public IFile File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
            }
        }
        public ILoadable Loader
        {
            get
            {
                return loader;
            }
            set
            {
                loader = value;
            }
        }
        public RSSFeedReader()
        {
        }

        public RSSFeedReader(IFile file)
        {
            this.file = file;
            if (!this.file.FileExist())
            {
                xdoc = new XDocument();
            }
            else
            {
                xdoc = XDocument.Load(file.FileName);
            }
        }
        public void Add(string name, string URL)
        {
            //Create element
            XElement rssFeed = new XElement("RSSFeed");
            XElement rssFeedName = new XElement("name", name);
            XElement rssFeedURL = new XElement("URL", URL);
            //Add element
            rssFeed.Add(rssFeedName);
            rssFeed.Add(rssFeedURL);
            if (!file.FileExist())
            {
                //Create root element
                rssFeeds = new XElement("RSSFeeds");
            }
            else
            {
                //Load root element
                rssFeeds = xdoc.Root;
                xdoc = new XDocument();
            }
            //Add element to root element
            rssFeeds.Add(rssFeed);
            xdoc.Add(rssFeeds);
            file.FileSave(xdoc);
        }
        public void Remove(string name)
        {
            if (file.FileExist())
            {
                xdoc.Element("RSSFeeds").Elements("RSSFeed").Where(x => x.Element("name").Value == name).Remove();
                rssFeeds = xdoc.Root;
                file.FileSave(xdoc);
            }
            else
            {
                throw new FileNotFoundException("File configuration.xml isn't found.");
            }
        }
        public void Download()
        {
            if (!xdoc.Root.IsEmpty)
            {
                foreach (var url in xdoc.Element("RSSFeeds").Elements("RSSFeed").
                    Select(x => x.Element("URL").Value))
                {
                    Task task = Task.Run(() => Console.WriteLine("----------------------------------------\n" +
                        loader.Load(url) + "----------------------------------------"));
                }
            }
            else
            {
                throw new NullReferenceException("No feeds are available in the xml config file.");
            }

        }
        public void Download(string name)
        {
            string url = xdoc.Element("RSSFeeds").Elements("RSSFeed").Where(x => x.Element("name").Value == name)
                .Select(x => x.Element("URL").Value).FirstOrDefault();
            if (url != null)
                Console.WriteLine(loader.Load(url));
            else
                throw new NullReferenceException("No feeds are available in the xml config file with this name.");
        }
    }
}
