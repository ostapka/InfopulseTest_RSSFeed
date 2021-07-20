using RSSFeed.Interfaces;
using System.Xml.Linq;

namespace RSSFeedReaderTests.Classes
{
    class ConfigFileStubTrue : IFile
    {
        public string FileName { get; set; } = "abc.xml";

        public bool FileExist()
        {
            return true;
        }

        public void FileSave(XDocument xdoc)
        {
            xdoc.ToString();
        }
    }
}
