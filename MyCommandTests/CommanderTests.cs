using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCommand;
using MyFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommand.Tests
{
    [TestClass()]
    public class CommanderTests
    {
        [TestMethod()]
        public void excTest()
        {
            string name = "excTest.bat";
            string path = FileManger.GetCurrentPath();
            path = FileManger.GetUpPath(path, 2);
            path = FileManger.GetDownPath(path, "Files");
            path = FileManger.GetDownPath(path, name);

            Commander.exc(path);
        }
    }
}