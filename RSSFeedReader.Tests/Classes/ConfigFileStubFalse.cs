using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSSFeed.Interfaces;
using System.Xml.Linq;

namespace RSSFeedReaderTests.Classes
{
    public class ConfigFileStubFalse : IFile
    {

        public TestContext TestContext { get; set; }
        public string FileName { get; set; } = "abc.xml";

        public bool FileExist()
        {
            return false;
        }

        public void FileSave(XDocument xdoc)
        {
            xdoc.ToString();
        }
    }
}
