using System.Xml.Linq;

namespace RSSFeed.Interfaces
{
    public interface IFile
    {
        string FileName { get; set; }
        bool FileExist();
        void FileSave(XDocument xdoc);

    }
}
