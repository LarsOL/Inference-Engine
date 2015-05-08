using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InferenceEngine;

namespace UnitTests
{
    [TestClass]
    public class TestPropositionInterpreter
    {
        [TestMethod]
        public void Basic3field_NoBrackets()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = {"a^b"};
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void Basic4field_NoBrackets()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a^bvc" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void Basic10field_NoBrackets()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a^bvc=>d&e^fvg&h^i" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void Basic10field_NoBrackets_DoubleSymbols()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a^gvc=>d&c^fvg&b^c" };
            test.ParseProps(teststring);
        }

    }
}
