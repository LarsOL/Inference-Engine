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
            bool?[] arguments = new bool?[_Model.Length];
            _StartWorld.Arguments = arguments;
            _StartWorld.FindPrimarys();
            bool Stuck = false;
            while ((_StartWorld.IsTrue(-1) == null) && (!Stuck))
            {
                Stuck = true;
                for (int i = 0; i < _StartWorld.Length; i++)
                {
                    Proposition temp;
                    if ((temp = _StartWorld.TryInfer(i)) != null)
                    {
                        _StartWorld.AddToKB(temp);
                        _StartWorld.FindPrimarys();
                        Stuck = false;
                    }
                }
            }
            return;
        }
    }
}
