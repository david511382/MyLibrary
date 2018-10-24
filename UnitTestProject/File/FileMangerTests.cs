using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Tests
{
    [TestClass()]
    public class FileMangerTests
    {
        [TestMethod()]
        public void GetDownPathTest()
        {
            string orgPath = "C:\\Users\\david\\Desktop\\Git\\MyLibrary\\MyFileTests\\bin\\Debug";
            string file = "test.txt";
            string path = FileManger.GetDownPath(orgPath, file);

            Assert.AreEqual("C:\\Users\\david\\Desktop\\Git\\MyLibrary\\MyFileTests\\bin\\Debug\\test.txt", path);
        }

        [TestMethod()]
        public void GetUpPathTest()
        {
            string orgPath = "C:\\Users\\david\\Desktop\\Git\\MyLibrary\\MyFileTests\\bin\\Debug";
            string path = FileManger.GetUpPath(orgPath);
            Assert.AreEqual("C:\\Users\\david\\Desktop\\Git\\MyLibrary\\MyFileTests\\bin\\Debug", path);

            path = FileManger.GetUpPath(orgPath, 1);
            Assert.AreEqual("C:\\Users\\david\\Desktop\\Git\\MyLibrary\\MyFileTests\\bin", path);

            path = FileManger.GetUpPath(orgPath, 2);
            Assert.AreEqual("C:\\Users\\david\\Desktop\\Git\\MyLibrary\\MyFileTests", path);
        }

        [TestMethod()]
        public void GetCurrentPathTest()
        {
            string path = FileManger.GetCurrentPath();
            Assert.AreEqual(AppDomain.CurrentDomain.BaseDirectory, path);
        }
    }

}