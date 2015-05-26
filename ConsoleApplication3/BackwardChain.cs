using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class BackwardChain
    {
        private Model _Model;
        private World _StartWorld;

        public BackwardChain(Model ProgramModel, World ProblemSpace)
        {
            _Model = ProgramModel;
            _StartWorld = ProblemSpace;
        }

        public void WorkShiznitOut()
        {
            bool?[] arguments = new bool?[_Model.Length];
            _StartWorld.Arguments = arguments;


        }
    }
}
