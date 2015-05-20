using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class ForwardChain
    {
        private Model _Model;
        private World _StartWorld;

        public ForwardChain(Model ProgramModel, World ProblemSpace)
        {
            _Model = ProgramModel;
            _StartWorld = ProblemSpace;

            
        } 
        public void WorkShizznitOut()
        {
            return;
        
        }
    }
}
