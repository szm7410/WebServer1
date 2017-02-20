using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        const string testStr = "aaaa";
        [TestMethod]
        public void TestMethod1()
        {
            string s = testStr;
        }
    }
}
