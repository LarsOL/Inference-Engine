using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class TruthTable
    {
        private Model _Model;
        private World _ProblemSpace;
        public TruthTable( Model ProgramModel, World ProblemSpace)
        {
            _Model = ProgramModel;
            _ProblemSpace = ProblemSpace;
        }

        private void prettycolours(bool? agr)
        {
            if(agr==true){
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if(agr==false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            System.Console.Write(agr);
            Console.ResetColor();
        }

        public void pretty_output()
        {
           bool? break_early = false; // debugging
           
            // make heading table
            for (int i = 0; i < _Model.Length; i++)
            {
                System.Console.Write(_Model.GetName(i) + "\t| ");
            }

            for (int i = 0; i < _ProblemSpace.Length; i++)
            {
               System.Console.Write( "Prp" + i + "\t| ");
            }
            System.Console.Write("KB\t| Goal\t| KB & Goal");
             System.Console.Write( System.Environment.NewLine);
            // done  
            bool? is_valid = true;
            bool?[] arguments = new bool?[_Model.Length];
            for (int i = 0; i < (1 << _Model.Length); i++) // for each combination
            {  // modifed from https://stackoverflow.com/questions/12488876/all-possible-combinations-of-boolean-variables
                for(int j = 0; j < _Model.Length; j++) // set that combination of arguements
                {
                    arguments[j] = ((i & (1 << j)) != 0);
                    prettycolours(arguments[j]);
                    System.Console.Write("\t| "); // c# adds the word True of False for bool type
                }

                _ProblemSpace.Arguments = arguments; // set the world state

                bool? Knowledge_true = true;
               
                for (int j = 0; j < _ProblemSpace.Length; j++) // check each proposition in the knowledge base 
                {
                    prettycolours(_ProblemSpace.IsTrue(j));
                    System.Console.Write("\t| "); 
                    Knowledge_true = Knowledge_true & _ProblemSpace.IsTrue(j);

                }
                prettycolours(Knowledge_true);
                System.Console.Write("\t| "); // is KB true in this world
                bool? goal_true = _ProblemSpace.IsTrue(-1);
                prettycolours(goal_true);
                System.Console.Write("\t| ");//goal proposition
                prettycolours(Knowledge_true & goal_true); //Does both goal and KB hold
                is_valid = is_valid & !(Knowledge_true & !goal_true); // invalid when KB is true and goal is false
                if ((!is_valid & break_early)==true) { // problem already invalid, no need to continue
                    return;
                }
                // does goal and knowgelde base hold for this world?
                System.Console.Write(System.Environment.NewLine);
            }
            System.Console.Write("Goal is vaild in this Knowledge base is: ");
            prettycolours(is_valid);
            return;
        }
        
       public string graphical_solve()
        {
            string answer = "";
            bool break_early = false; // debugging
           
            // make heading table
            for (int i = 0; i < _Model.Length; i++)
            {
                answer += _Model.GetName(i) + "\t| ";
            }

            for (int i = 0; i < _ProblemSpace.Length; i++)
            {
                answer += "Prp " + i + "\t| ";
            }
            answer += "KB\t| Goal\t| KB & Goal";
            answer += System.Environment.NewLine;
            // done  
            bool? is_valid = true;
            bool?[] arguments = new bool?[_Model.Length];
            for (int i = 0; i < (1 << _Model.Length); i++) // for each combination
            {  // modifed from https://stackoverflow.com/questions/12488876/all-possible-combinations-of-boolean-variables
                for(int j = 0; j < _Model.Length; j++) // set that combination of arguements
                {
                    arguments[j] = ((i & (1 << j)) != 0);
                    answer += arguments[j] + "\t| "; // c# adds the word True of False for bool type
                }

                _ProblemSpace.Arguments = arguments; // set the world state

                bool? Knowledge_true = true;
               
                for (int j = 0; j < _ProblemSpace.Length; j++) // check each proposition in the knowledge base 
                {
                    answer += _ProblemSpace.IsTrue(j) + "\t| " ;
                    Knowledge_true = Knowledge_true & _ProblemSpace.IsTrue(j);
                }
                answer += Knowledge_true + "\t| ";  // is KB true in this world
                bool? goal_true = _ProblemSpace.IsTrue(-1);
                answer += goal_true + "\t| "; //goal proposition
                answer += Knowledge_true & goal_true; //Does both goal and KB hold
                is_valid = is_valid & !(Knowledge_true & !goal_true); // invalid when KB is true and goal is false
                if ((!is_valid & break_early)== true) { // problem already invalid, no need to continue
                    return answer;
                }
                // does goal and knowgelde base hold for this world?
                answer += System.Environment.NewLine;
            }

            return answer;
        }

       public int solve()
       {
           bool break_early = true; // debugging

           // done  
           bool? is_valid = true;
           int NoVaild = 0;
           bool?[] arguments = new bool?[_Model.Length];

           for (int i = 0; i < (1 << _Model.Length); i++) // for each combination
           {  // modifed from https://stackoverflow.com/questions/12488876/all-possible-combinations-of-boolean-variables
               for (int j = 0; j < _Model.Length; j++) // set that combination of arguements
               {
                   arguments[j] = ((i & (1 << j)) != 0);
               }

               _ProblemSpace.Arguments = arguments; // set the world state

               bool? Knowledge_true = true;

               for (int j = 0; j < _ProblemSpace.NoPropositions(); j++) // check each proposition in the knowledge base 
               {
                   Knowledge_true = Knowledge_true & _ProblemSpace.IsTrue(j);
               }
               bool? goal_true = _ProblemSpace.IsTrue(-1);
               if ((bool)(Knowledge_true & goal_true))
               {
                   NoVaild++;
               }
               is_valid = is_valid & !(Knowledge_true & !goal_true); // invalid when KB is true and goal is false
               if ((!is_valid & break_early) == true)
               { // problem already invalid, no need to continue
                   System.Console.WriteLine("NO");
                   return 0;
               }               
           }

           System.Console.WriteLine("YES:" + NoVaild);
           return NoVaild;
       }
    }
}
