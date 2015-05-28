using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InferenceEngine;
using System.IO;

namespace UnitTests
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class WholeProgramTests
    {
        FileInput input;
        Model model;
        PropositionInterpreter propintr ;
        World MyWorld;
        TruthTable Truthsolver;
        ForwardChain forwardsolver;
        BackwardsChain backwardsolver;
        string filePath;
        StringWriter strWriter;

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


        private void setup()
        {
            model = new Model();
            propintr = new PropositionInterpreter(ref model);
            input = new FileInput(filePath);
            MyWorld = new World(propintr.ParseProps(input.ReadFromFile()), model.Length);
            Truthsolver = new TruthTable(model, MyWorld);
            forwardsolver = new ForwardChain(model, MyWorld);
            backwardsolver = new BackwardsChain(model, MyWorld);
            strWriter = new StringWriter();
            Console.SetOut(strWriter);


        }

        private void clearsw()
        {
            StringBuilder sb = strWriter.GetStringBuilder();
            sb.Remove(0, sb.Length);
        }

        [TestMethod]
        public void All_t1()
        {
            filePath = "t1.txt";
            bool rightAns = true;
            setup();
            Truthsolver.solve();
            bool truth = (strWriter.ToString()[0] == 'Y'); // if it returns YES
            clearsw();
            backwardsolver.Start();
            bool backwards = (strWriter.ToString()[0] == 'Y'); // if it returns YES
            clearsw();
            forwardsolver.Start();
            bool forward = (strWriter.ToString()[0] == 'Y'); // if it returns YES

            Assert.Equals(truth, rightAns);
            Assert.Equals(truth, backwards);
            Assert.Equals(backwards, forward);
            

        }


        [TestMethod]
        public void All_t2()
        {
            filePath = "t2.txt";
            bool rightAns = true;
            setup();
            Truthsolver.solve();
            bool truth = (strWriter.ToString()[0] == 'Y'); // if it returns YES
            clearsw();
            backwardsolver.Start();
            bool backwards = (strWriter.ToString()[0] == 'Y'); // if it returns YES
            clearsw();
            forwardsolver.Start();
            bool forward = (strWriter.ToString()[0] == 'Y'); // if it returns YES

            Assert.Equals(truth, rightAns);
            Assert.Equals(truth, backwards);
            Assert.Equals(backwards, forward);


        }
    }
}
