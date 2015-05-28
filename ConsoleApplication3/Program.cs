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
                    args = new[] { "BC", "./t1.txt" };
                }

                if (args.Length != 2)
                {
                    throw new System.ArgumentException("Wrong amount of arguments. Usage: Program.exe <method> <file path>");
                }
                
                FileInput input = new FileInput(args[1]);
                Model model = new Model();
                PropositionInterpreter propintr = new PropositionInterpreter(ref model);
                World MyWorld = new World(propintr.ParseProps(input.ReadFromFile()), model.Length);

                switch (args[0].ToUpper())
                {
                    case "TT":
                        TruthTable Truthsolver = new TruthTable(model, MyWorld);
                        Truthsolver.solve();
                        break;
                    case "FC":
                        ForwardChain forwardsolver = new ForwardChain(model, MyWorld);
                        forwardsolver.Start();
                        break;
                    case "BC":
                        BackwardsChain backwardsolver = new BackwardsChain(model, MyWorld);
                        backwardsolver.Start();
                        break;
                    case "GRAPHICAL_TT":
                        TruthTable GraphicalTruthsolver = new TruthTable(model, MyWorld);
                        GraphicalTruthsolver.pretty_output();
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
