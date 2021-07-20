using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSSFeed.Classes;
using RSSFeed.Interfaces;
using RSSFeedReaderTests.Classes;
using System;
using System.IO;
using System.Xml.Linq;

namespace RSSFeedReaderTests
{
    [TestClass]
    public class RSSFeedReaderTests
    {
        [TestMethod]
        public void RSSFeedReader_ctor()
        {
            // arrange
            IFile file = new ConfigurationFile();
            // act
            RSSFeedReader reader = new RSSFeedReader(file);
            // assert
            Assert.IsNotNull(reader);
        }
        [TestMethod]
        public void AddTest()
        {
            // arrange
            IFile file = new ConfigFileStubFalse();
            XDocument xDoc = new XDocument(
                new XElement("RSSFeeds",
                    new XElement("RSSFeed",
                        new XElement("name", "ON"),
                        new XElement("URL", "https://on.com"))));
            RSSFeedReader reader = new RSSFeedReader
            {
                File = file,
                Xdoc = new XDocument()
            };
            // act
            reader.Add("ON", "https://on.com");
            // assert
            Assert.IsTrue(XNode.DeepEquals(reader.Xdoc.Root, xDoc.Root));
        }
        [TestMethod]
        public void AddOtherTest()
        {
            // arrange
            IFile file = new ConfigFileStubTrue();
            XDocument xDoc = new XDocument(
                new XElement("RSSFeeds",
                    new XElement("RSSFeed",
                        new XElement("name", "ON"),
                        new XElement("URL", "https://on.com")),
                    new XElement("RSSFeed",
                        new XElement("name", "ONN"),
                        new XElement("URL", "https://onn.com"))));
            RSSFeedReader reader = new RSSFeedReader
            {
                File = file,
                Xdoc = new XDocument(
                new XElement("RSSFeeds",
                    new XElement("RSSFeed",
                        new XElement("name", "ON"),
                        new XElement("URL", "https://on.com"))))
            };
            // act
            reader.Add("ONN", "https://onn.com");
            // assert
            Assert.IsTrue(XNode.DeepEquals(reader.Xdoc.Root, xDoc.Root));
        }
        [TestMethod]
        public void RemoveTest()
        {
            // arrange
            IFile file = new ConfigFileStubTrue();
            RSSFeedReader reader = new RSSFeedReader();
            XDocument xDoc = new XDocument(
                new XElement("RSSFeeds",
                    new XElement("RSSFeed",
                        new XElement("name", "ON"),
                        new XElement("URL", "https://on.com"))));
            reader.Xdoc = new XDocument(
                new XElement("RSSFeeds",
                    new XElement("RSSFeed",
                        new XElement("name", "ON"),
                        new XElement("URL", "https://on.com")),
                    new XElement("RSSFeed",
                        new XElement("name", "ONN"),
                        new XElement("URL", "https://onn.com"))));
            reader.File = file;
            // act
            reader.Remove("ONN");
            // assert
            Assert.IsTrue(XNode.DeepEquals(reader.Xdoc.Root, xDoc.Root));
        }
        [ExpectedException(typeof(FileNotFoundException), "File configuration.xml isn't found.")]
        [TestMethod]
        public void RemoveExceptionTest()
        {
            // arrange
            IFile file = new ConfigFileStubFalse();
            RSSFeedReader reader = new RSSFeedReader
            {
                File = file
            };
            // act
            reader.Remove("ON");
            // assert
        }
        [TestMethod]
        public void DowloadTest()
        {
            // arrange
            RSSFeedReader reader = new RSSFeedReader
            {
                Xdoc = new XDocument(
                new XElement("RSSFeeds",
                    new XElement("RSSFeed",
                        new XElement("name", "ON"),
                        new XElement("URL", "https://on.com")))),
                Loader = new LoaderStub()
            };
            // act
            reader.Download("ON");
            // assert
            Assert.AreEqual(reader.Loader.Load("https://on.com").ToString(), "Hello");
        }
        [ExpectedException(typeof(NullReferenceException), "No feeds are available in the xml config file.")]
        [TestMethod]
        public void DownloadExceptionTest()
        {
            // arrange
            RSSFeedReader reader = new RSSFeedReader
            {
                Xdoc = new XDocument(new XElement("RSSFeeds"))
            };
            // act
            reader.Download();
            // assert
        }
        [ExpectedException(typeof(NullReferenceException), 
            "No feeds are available in the xml config file with this name.")]
        [TestMethod]
        public void DownloadExceptionOtherTest()
        {
            // arrange
            RSSFeedReader reader = new RSSFeedReader
            {
                Xdoc = new XDocument(
                new XElement("RSSFeeds",
                    new XElement("RSSFeed",
                        new XElement("name", "ON"),
                        new XElement("URL", "https://on.com"))))
            };
            // act
            reader.Download("OFF");
            // assert
        }
    }
}
