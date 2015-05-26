using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InferenceEngine;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ForwardChain()
        {
            FileInput input = new FileInput("./t1.txt");
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            World MyWorld = new World(test.ParseProps(input.ReadFromFile()), temp.Length);
            ForwardChain solver = new ForwardChain(temp, MyWorld);
            solver.Start();

        }
        [TestMethod]
        public void BackwardChainT1()
        {
            FileInput input = new FileInput("./t1.txt");
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            World MyWorld = new World(test.ParseProps(input.ReadFromFile()), temp.Length);
            BackwardsChain solver = new BackwardsChain(temp, MyWorld);
            solver.Start();
        }

        [TestMethod]
        public void BackwardChainAndImp()
        {
            FileInput input = new FileInput("./t2.txt");
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            World MyWorld = new World(test.ParseProps(input.ReadFromFile()), temp.Length);
            BackwardsChain solver = new BackwardsChain(temp, MyWorld);
            solver.Start();
        }

    }
}
