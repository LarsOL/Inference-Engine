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
        public bool? WorkShizznitOut()
        {
            bool?[] arguments = new bool?[_Model.Length];
            _StartWorld.Arguments = arguments;
            _StartWorld.FindPrimarys();
            bool Stuck = false;
            string path = "";
            while ((_StartWorld.IsTrue(-1) == null) && (!Stuck))
            {
                Stuck = true;
                for (int i = 0; i < _StartWorld.Length; i++)
                {
                    Proposition temp;
                    if ((temp = _StartWorld.TryInfer(i)) != null)
                    {
                        _StartWorld.AddToKB(temp);
                        //path += _Model.GetName(temp.getA()) +", ";
                        _StartWorld.FindPrimarys();
                        Stuck = false;
                    }
                }
            }
            if((_StartWorld.IsTrue(-1)==true))
            {
                System.Console.Write("YES: ");
                for (int i = 0; i < _StartWorld.Arguments.Length;i++ )
                {
                    if (_StartWorld.Arguments[i] == true)
                    {
                        if(i != 0)
                        {
                            path += ", ";
                        }
                        path += _Model.GetName(i);

                    }
                }
                System.Console.Write(path);
            
            }
            else
            {
                System.Console.Write("NO");
            }
            return (_StartWorld.IsTrue(-1));
        }
    }
}
