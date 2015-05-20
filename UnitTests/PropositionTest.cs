using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InferenceEngine;

namespace UnitTests
{
    [TestClass]
    public class PropositionTest
    {
        [TestMethod]
        public void testIntConstruction()
        {
            Proposition temp = new Proposition(2, Operations.Conjunction, 3);
            Operations O = Operations.Conjunction;
            Assert.AreEqual(temp.Operation, O, "Operations Error");
            Assert.AreEqual(temp.getA(), 2, "'A' error");
            Assert.AreEqual(temp.getB(), 3, "'B' error");
        }

//        [TestMethod]
        public void testReferenceConstruction()
        {
            Proposition tempA = new Proposition(2, Operations.Conjunction, 3);
            Proposition tempB = new Proposition(9, Operations.Conjunction, 6);
//            Proposition temp = new Proposition(tempA, Operations.Conjunction, tempB);
            
        }

        [TestMethod]
        public void testOperations()
        {
            Proposition tempAnd = new Proposition(2, Operations.Conjunction, 3);
            Assert.AreEqual(Operations.Conjunction, tempAnd.Operation, "AND Error");

            Proposition tempOr = new Proposition(2, Operations.Disjunction, 3);
            Assert.AreEqual(Operations.Disjunction, tempOr.Operation, "OR Error");
           
//          Proposition tempNot = new Proposition("!", 3);
//          Assert.AreEqual(Operations.Negation, tempNot.getO(), "NOT Error");

            Proposition tempImp = new Proposition(2, Operations.Implication, 3);
            Assert.AreEqual(Operations.Implication, tempImp.Operation, "Implication Error");

            Proposition tempBic = new Proposition(2, Operations.Biconditional, 3);
            Assert.AreEqual(Operations.Biconditional, tempBic.Operation, "Biconditional Error");
            
            Proposition tempNS = new Proposition();
            Assert.AreEqual(Operations.NotSet, tempNS.Operation, "Not Set Error");
        }

//       [TestMethod]
        public void testIsTrueNot()
        {
            //IMPLEMENT!
        }

        [TestMethod]
        public void Single_flag()
        {
            Proposition temp = new Proposition();
            temp.setA(0);
            Assert.AreEqual(temp.Single, true);
        }
        
        [TestMethod]
        public void testIsTrueAnd()
        {
            Proposition tempAnd = new Proposition(0, Operations.Conjunction, 1);
            bool?[] andTT = {true,true};
            Assert.AreEqual(true, tempAnd.IsTrue(andTT), "T&T error");

            bool?[] andTF = {true, false};
            Assert.AreEqual(false, tempAnd.IsTrue(andTF), "T&F error");

            bool?[] andFT = {false, true};
            Assert.AreEqual(false, tempAnd.IsTrue(andTF), "F&T error");

            bool?[] andFF = {false, false};
            Assert.AreEqual(false, tempAnd.IsTrue(andFF), "F&F error");
        }

        [TestMethod]
        public void testIsTrueOr()
        {
            Proposition tempOr = new Proposition(0, Operations.Disjunction, 1);
            bool?[] OrTT = {true, true };
            Assert.AreEqual(true, tempOr.IsTrue(OrTT), "T|T error");

            bool?[] OrTF = { true, false };
            Assert.AreEqual(true, tempOr.IsTrue(OrTF), "T|F error");

            bool?[] OrFT = { false, true };
            Assert.AreEqual(true, tempOr.IsTrue(OrTF), "F|T error");

            bool?[] OrFF = { false, false };
            Assert.AreEqual(false, tempOr.IsTrue(OrFF), "F|F error");
        }

        [TestMethod]
        public void testIsTrueImp()
        {
            Proposition tempImp = new Proposition(0, Operations.Implication, 1);
            bool?[] ImpTT = {true, true};
            Assert.AreEqual(true, tempImp.IsTrue(ImpTT), "T=>T error");

            bool?[] ImpTF = {true, false};
            Assert.AreEqual(false, tempImp.IsTrue(ImpTF), "T=>F error");

            bool?[] ImpFT = {false, true};
            Assert.AreEqual(true, tempImp.IsTrue(ImpFT), "F=>T error");

            bool?[] ImpFF = {false, false};
            Assert.AreEqual(true, tempImp.IsTrue(ImpFF), "F=>F error");
        }

       // [TestMethod]
        public void testIsTrueBic()
        {
            Proposition tempBic = new Proposition(0, Operations.Biconditional, 1);
            bool?[] BicTT = {true, true};
            Assert.AreEqual(true, tempBic.IsTrue(BicTT), "T<=>T error");

            bool?[] BicTF = {true, false};
            Assert.AreEqual(false, tempBic.IsTrue(BicTF), "T<=>F error");

            bool?[] BicFT = {false, true};
            Assert.AreEqual(false, tempBic.IsTrue(BicFT), "F<=>T error");

            bool?[] BicFF = {false, false};
            Assert.AreEqual(true, tempBic.IsTrue(BicFF), "F<=>F error");
        }

    }
}
