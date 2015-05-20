using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class Program
    {
        static void Main(string[] args)
        {

            FileInput input = new FileInput("./t1.txt");
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            World MyWorld = new World(test.ParseProps(input.ReadFromFile()), temp.Length);
            ForwardChain solver = new ForwardChain(temp, MyWorld);
            solver.WorkShizznitOut();

            System.Console.ReadKey();

            /*
            FileInput input = new FileInput("./sillyness.txt");
            Model temp = new Model();
            PropositionInterpreter test = new PropositionInterpreter(ref temp);
            string[] teststring = { "(a|(~a&b))&((e<=>f)=>b)", "f=>a" };
            World MyWorld = new World(test.ParseProps(input.ReadFromFile()),temp.Length);
            TruthTable solver = new TruthTable( temp, MyWorld);
            solver.pretty_output();*/
        }
    }
}
