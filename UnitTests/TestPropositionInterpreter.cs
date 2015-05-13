using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InferenceEngine;

namespace UnitTests
{
    [TestClass]
    
    public class TestPropositionInterpreter
    {
        // NOTE: NOT A STRICT TEST YET, ONLY CHECKS TO SEE THE METHOD FINISHES, NOT THE CORRECT ANSWER.
        [TestMethod]
        public void NoBrackets_B3feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = {"a^b"};
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void NoBrackets_4feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a^bvc" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void NoBrackets_9feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a^bvc=>d&e^fvg&h^i" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void NoBrackets_9feild_DoubleSymbols()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a^gvc=>d&c^fvg&b^c" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void Brackets_3feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a^b)vc" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void Brackets_4feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a^b)v(c^d)" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void Brackets_6feild_DoubleSymbols()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a^b)v(c^d)=>(b^c)" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void BracketsNested_5feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "((a^b)v(c^d))=>c" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void BracketsNested_8feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "((a^b)v(c^d))=>((e^f)&(gvh))" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void BracketsNested_8feild_DoubleSymbols()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "((a^h)v(c^d))=>((c^f)&(dvh))" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void MultiProps_Complicatied()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a^b)vc", "a^bvc=>d&e^fvg&h^i","(a^b)v(c^d)=>(b^c)", "((a^b)v(c^d))=>((e^f)&(gvh))","((a^h)v(c^d))=>((c^f)&(dvh))"};
            test.ParseProps(teststring);
        }

    }
}
