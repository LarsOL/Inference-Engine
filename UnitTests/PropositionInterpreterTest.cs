using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InferenceEngine;

namespace UnitTests
{
    [TestClass]
    
    public class PropositionInterpreterTest
    {
        // NOTE: NOT A STRICT TEST YET, ONLY CHECKS TO SEE THE METHOD FINISHES, NOT THE CORRECT ANSWER.

        Model model;
        PropositionInterpreter propintr;

        [TestInitialize()]
        public void setup()
        {
            model = new Model();
            propintr = new PropositionInterpreter(ref model);
        }

        [TestCleanup()]

        [TestMethod]
        public void NoBrackets_3feild()
        {
            string[] teststring = {"a&b"};
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void Test_hornPriority()
        {
            string[] teststring = { "p1&b=>p2" };
            Proposition[] result = propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void NoBrackets_4feild()
        {
            string[] teststring = { "a&b|c" };
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void NoBrackets_9feild()
        {
            string[] teststring = { "a&b|c=>d&e|f&g&h|i" };
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void NoBrackets_9feild_DoubleSymbols()
        {
            string[] teststring = { "a|g&c<=>d&c|f&g|b|c" };
            propintr.ParseProps(teststring);
            
        }

        [TestMethod]
        public void Brackets_3feild()
        {
            string[] teststring = { "(a&b)|c" };
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void Brackets_4feild()
        {
            string[] teststring = { "(a&b)|(c&d)" };
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void Brackets_6feild_DoubleSymbols()
        {
            string[] teststring = { "(a&b)|(c|d)<=>(b&c)" };
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void BracketsNested_5feild()
        {
            string[] teststring = { "((a|b)&(c|d))=>c" };
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void BracketsNested_8feild()
        {
            string[] teststring = { "((a|b)&(c|d))=>((e|f)&(g|h))" };
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void BracketsNested_8feild_DoubleSymbols()
        {
            string[] teststring = { "((a&h)|(c&d))=>((c|f)&(d|h))" };
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void MultiProps_Complicatied()
        {
            string[] teststring = { "(a&b)|c", "a&b|c=>d&e|f&g&h|i", "(a|b)&(c|d)=>(b|c)", "((a&b)|(c&d))=>((e&f)&(g|h))", "((a&h)|(c&d))=>((c|f)&(d|h))", "~(a|~B)" };
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void NotTest()
        {
            string[] teststring = {"~a|B&~c"};
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void NotTest_2feild()
        {
            string[] teststring = { "~a|~B" };
            propintr.ParseProps(teststring);
        }

        [TestMethod]
        public void NotInfrontOfBracket()
        {
            string[] teststring = { "~(a|~B)" };
            Proposition[] tester = propintr.ParseProps(teststring);
        }
        [TestMethod]
        public void HornTest()
        {
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "b&e => f" };
            Proposition[] tester = test.ParseProps(teststring);
        }
    }
}
