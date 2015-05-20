using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class World
    {
        private KnowledgeBase _KnowledgeBase;
        private Proposition _Goal;


        public bool?[] Arguments { get; set; }
        /*
        public bool[] Arguments
        {
            get
            {
                return Arguments;
            }
            set
            {
              //  if (Arguments == null || value.Length != Arguments.Length)
              //      throw new System.ArgumentException("Wrong size arguments array");
                Arguments = value;
            }
        } */
        public World(KnowledgeBase KnowledgeBase, Proposition Goal)
        {
            _KnowledgeBase = KnowledgeBase;
            _Goal = Goal;
        }

        public World(Proposition[] Propositions,int amount_args)
        {
            Proposition[] Temp = new Proposition[Propositions.Length -1];
            Array.Copy(Propositions, Temp, Temp.Length);
            _KnowledgeBase = new KnowledgeBase(Temp);
            _Goal = Propositions[Propositions.Length - 1];
            Arguments = new bool?[amount_args];
        }

        /// <summary>
        /// Check if the goal is vaild
        /// </summary>
        public bool IsTrue()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Check if (PropostionNo) is true in this world state
        /// -1 is goal check
        /// </summary>
        public bool? IsTrue(int PropositionNo)
        {
            int[] needed_ags;
            if(PropositionNo == -1)
            {
                needed_ags = _Goal.Requirements().ToArray();
            }
            else
            {
                needed_ags = _KnowledgeBase.Requirements(PropositionNo);
            }
               
            bool?[] grab_ags = new bool?[needed_ags.Length];
            for (int i = 0; i < needed_ags.Length; i++ )
            {
                grab_ags[i] = Arguments[needed_ags[i]];
            }
            if (PropositionNo == -1)
            {
                return _Goal.IsTrue( grab_ags);
            }
            else
            {
                return _KnowledgeBase.IsTrue(PropositionNo, grab_ags);
            }
        }

        public Proposition TryInfer(int PropositionNo)
        {
            int[] needed_ags;
            needed_ags = _KnowledgeBase.Requirements(PropositionNo);
            bool?[] grab_ags = new bool?[needed_ags.Length];
            for (int i = 0; i < needed_ags.Length; i++)
            {
                grab_ags[i] = Arguments[needed_ags[i]];
            }
            return _KnowledgeBase.TryInfer(PropositionNo, grab_ags);

        }

        public void FindPrimarys()
        {
            for (int i = 0; i < _KnowledgeBase.Length; i++)
            {
                if (_KnowledgeBase.Propostions[i].Single)
                {
                    SetArgument(true,_KnowledgeBase.Propostions[i].getA());
                }
            }
        }

        public int Length
        {
            get { return _KnowledgeBase.Length; }
        }

        public void SetArgument(bool argument, int symbol)
        {
            if(symbol < Arguments.Length-1 || symbol > 0) // out of bounds check
            {
                Arguments[symbol] = argument;
            }
        }

        public bool? GetArgument(int symbol)
        {
            if (symbol > Arguments.Length - 1 || symbol < 0) // out of bounds check
            {
                return Arguments[symbol];
            }
            else
            {
                throw new System.ArgumentException("Invalid symbol (out of bounds)");
            }
        }

        public void AddToKB(Proposition PTemp)
        {
            _KnowledgeBase.AddToKB(PTemp);
        }

    }
}
