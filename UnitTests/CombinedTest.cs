using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InferenceEngine;

namespace UnitTests
{
    /// <summary>
    /// Summary description for CombinedTest
    /// </summary>
    [TestClass]
    public class CombinedTest
    {
        public CombinedTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestParserRequirements()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = {"a&b"};
            Proposition[] tester = test.ParseProps(teststring);
            List<int> symbols = tester[0].Requirements();
            List<int> rightAns =  new List<int>{0, 1 };
            CollectionAssert.AreEqual(symbols, rightAns);
        }

        [TestMethod]
        public void TestParserIsTrue()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a&b" };
            Proposition[] tester = test.ParseProps(teststring);
            bool ans = tester[0].IsTrue(new[] {true,false});
            Assert.AreEqual(ans, false);
        }
        [TestMethod]
        public void TestParserRequirements_semicomplicated()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a|(b&~a)" };
            Proposition[] tester = test.ParseProps(teststring);
            List<int> symbols = tester[0].Requirements();
            List<int> rightAns = new List<int> { 0, 1 ,0};
            CollectionAssert.AreEqual(symbols, rightAns);
        }

        [TestMethod]
        public void TestParserIsTrue_semicomplicated()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a|(b&~a)" };
            Proposition[] tester = test.ParseProps(teststring);
            bool ans = tester[0].IsTrue(new[] { true, false ,true});
            Assert.AreEqual(ans, true);
        }
         [TestMethod]
        public void TestParserRequirements_complicated()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a|(~a&d))&((e<=>f)=>d)" };
            Proposition[] tester = test.ParseProps(teststring);
            List<int> symbols = tester[0].Requirements();
            List<int> rightAns = new List<int> { 0, 0,1 ,2,3,1 };
            CollectionAssert.AreEqual(symbols, rightAns);
        }

        [TestMethod]
        public void TestParserIsTrue_complicated()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a|(~a&d))&((e<=>f)=>d)" };
            Proposition[] tester = test.ParseProps(teststring);
            bool ans = tester[0].IsTrue(new[] { false, false,true,false,true,true });
            Assert.AreEqual(ans, true);
            ans = tester[0].IsTrue(new[] { false, false, false, false, true, false });
            Assert.AreEqual(ans, false);
            ans = tester[0].IsTrue(new[] { true, true, false, true, true, false });
            Assert.AreEqual(ans, false);
        }
    }
}
