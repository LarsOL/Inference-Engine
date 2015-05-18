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
            string[] teststring = {"a&b"};
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void NoBrackets_4feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a&b|c" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void NoBrackets_9feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a&b|c=>d&e|f&g&h|i" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void NoBrackets_9feild_DoubleSymbols()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "a|g&c<=>d&c|f&g|b|c" };
            test.ParseProps(teststring);
            
        }
        [TestMethod]
        public void Brackets_3feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a&b)|c" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void Brackets_4feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a&b)|(c&d)" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void Brackets_6feild_DoubleSymbols()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a&b)|(c|d)<=>(b&c)" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void BracketsNested_5feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "((a|b)&(c|d))=>c" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void BracketsNested_8feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "((a|b)&(c|d))=>((e|f)&(g|h))" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void BracketsNested_8feild_DoubleSymbols()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "((a&h)|(c&d))=>((c|f)&(d|h))" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void MultiProps_Complicatied()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a&b)|c", "a&b|c=>d&e|f&g&h|i","(a|b)&(c|d)=>(b|c)", "((a&b)|(c&d))=>((e&f)&(g|h))","((a&h)|(c&d))=>((c|f)&(d|h))"};
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void NotTest()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = {"~a|B&~c"};
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void NotTest_2feild()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "~a|~B" };
            test.ParseProps(teststring);
        }
        [TestMethod]
        public void NotInfrontOfBracket()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "~(a|~B)" };
            Proposition[] tester = test.ParseProps(teststring);
        }

    }
}
