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
        [TestMethod]
        public void readFromfile()
        {
            FileInput Test = new FileInput("./file_input_test.txt");
            string[] ans = Test.ReadFromFile();
            string[] rightans = { "p2=>p3", "p3=>p1", "p1=>d", "p1&p3=>c", "~a", "b", "p2", "as", "a&v", "h|~gl", "5g&sd<=>t","d=>s" };
            CollectionAssert.AreEqual(ans,rightans);
        }

    }   
}
