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
        public TruthTable(ref Model ProgramModel, ref World ProblemSpace)
        {
            _Model = ProgramModel;
            _ProblemSpace = ProblemSpace;
        }

       public string graphical_solve()
        {
            string answer = "";
            // make heading table
            for (int i = 0; i < _Model.Length; i++)
            {
                answer += _Model.GetName(i) + "  |  ";
            }

            for (int i = 0; i < _ProblemSpace.NoPropositions(); i++)
            {
                answer += "Prop #" + i + "  |  ";
            }
            answer += "Goal | Goal holds in World";
            answer += System.Environment.NewLine;
            // done 
            bool[] arguments = new bool[_Model.Length];
            for (int i = 0; i < (1 << _Model.Length); i++) // for each combination
            {  // modifed from https://stackoverflow.com/questions/12488876/all-possible-combinations-of-boolean-variables
                for(int j = 0; j < _Model.Length; j++) // set that combination of arguements
                {
                    arguments[j] = ((i & (1 << j)) != 0);
                    answer += arguments [j] + "  |  "; // c# adds the word True of False for bool type
                }

                _ProblemSpace._Arguments = arguments; // set the world state

                bool Everything_true = true;
                for (int j = 0; j < _ProblemSpace.NoPropositions(); j++) // check each proposition in the knowledge base 
                {
                    answer += _ProblemSpace.IsTrue(j) + "  |  " ;
                    Everything_true = Everything_true && _ProblemSpace.IsTrue(j);

                }
                answer += _ProblemSpace.IsTrue(-1)+ "  |  "; //goal proposition
                Everything_true = Everything_true && _ProblemSpace.IsTrue(-1);
                answer += Everything_true; // does goal and knowgelde base hold for this world?

                answer += System.Environment.NewLine;
            }

            return answer;
        }
    }
}
