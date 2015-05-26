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
            bool debugging = true; //DEBUG FLAG
            try
            {
                if (debugging)// debugging overide REMOVE
                {
                    args = new[] { "TT", "./t2.txt" };
                }

                if (args.Length != 2)
                {
                    throw new System.ArgumentException("Wrong amount of arguments. Usage: Program.exe <method> <file path>");
                }
                
                FileInput input = new FileInput(args[1]);
                Model temp = new Model();
                PropositionInterpreter test = new PropositionInterpreter(ref temp);
                World MyWorld = new World(test.ParseProps(input.ReadFromFile()), temp.Length);

                switch (args[0].ToUpper())
                {
                    case "TT":
                        TruthTable Truthsolver = new TruthTable(temp, MyWorld);
                        Truthsolver.solve();
                        break;
                    case "FC":
                        ForwardChain forwardsolver = new ForwardChain(temp, MyWorld);
                        forwardsolver.WorkShizznitOut();
                        break;
                    case "BC":
                        BackwardsChain backwardsolver = new BackwardsChain(temp, MyWorld);
                        backwardsolver.WorkShiznitOut();
                        break;
                    default:
                        throw new System.ArgumentException("Invaild method. Usage: Program.exe <method> <file path>");

                }

                if (debugging)// debugging overide REMOVE
                {
                    System.Console.ReadKey();
                }
                
            }
            catch (Exception e)
            {
                System.IO.TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine("Error: " + e.Message);
                if (debugging)// debugging overide REMOVE
                {
                    errorWriter.WriteLine(Environment.NewLine + "In Object: " + e.StackTrace); //DEBUGGING
                }
                
                System.Environment.Exit(1); // something failed
            }
        }
    }
}
