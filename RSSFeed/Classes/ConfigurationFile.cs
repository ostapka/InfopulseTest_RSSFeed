using RSSFeed.Interfaces;
using System.Configuration;
using System.IO;
using System.Xml.Linq;

namespace RSSFeed.Classes
{
    public class ConfigurationFile : IFile
    {
        public string FileName { get; set; } = ConfigurationManager.AppSettings.Get("fileName");
        public bool FileExist()
        {
            return File.Exists(FileName);
        }
        public void FileSave(XDocument xdoc)
        {
            xdoc.Save(FileName);
        }
    }
}
