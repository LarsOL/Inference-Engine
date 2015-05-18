using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InferenceEngine;

namespace UnitTests
{
    [TestClass]
    public class FileTests
    {
        [TestMethod]
        public void RemoveWhiteSpaceTest()
        {
            FileInput Test = new FileInput("");
            string TestString = Test.RemoveWhiteSpace("a b c");

            Assert.AreEqual(TestString, "abc", "White space not removed!");
        }
        [TestMethod]
        public void AddToEndTest()
        {
            FileInput Test = new FileInput("");
            string[] TestArray1 = "a b".Split(' ');
            string[] TestArray2 = Test.AddToEnd(TestArray1, "c");
            Assert.AreEqual(TestArray2.Length,3);
            Assert.AreEqual(TestArray2[2], "c");
        }
    }
}
