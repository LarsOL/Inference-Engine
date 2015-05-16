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

        public bool[] _Arguments
        {
            get
            {
                return _Arguments;
            }
            set
            {
                if (value.Length != _Arguments.Length)
                    throw new System.ArgumentException("Wrong size arguments array");
                _Arguments = value;
            }
        }
        public World(KnowledgeBase KnowledgeBase, Proposition Goal)
        {
            _KnowledgeBase = KnowledgeBase;
            _Goal = Goal;
        }

        public World(Proposition[] Propositions)
        {
            Proposition[] Temp = new Proposition[Propositions.Length -1];
            Array.Copy(Propositions, Temp, Temp.Length);
            _KnowledgeBase = new KnowledgeBase(Propositions);
            _Goal = Propositions[Propositions.Length - 1];
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
        public bool IsTrue(int PropositionNo)
        {
            int[] needed_ags;
            if(PropositionNo == -1)
            {
                needed_ags = _Goal.Requirements();
            }
            else
            {
                needed_ags = _KnowledgeBase.Requirements(PropositionNo);
            }
               
            bool[] grab_ags = new bool[needed_ags.Length];
            for (int i = 0; i < needed_ags.Length; i++ )
            {
                grab_ags[i] = _Arguments[needed_ags[i]];
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

        public int NoPropositions()
        {
            return _KnowledgeBase.Length;
        }

        public void SetArgument(bool argument, int symbol)
        {
            if(symbol > _Arguments.Length-1 || symbol < 0) // out of bounds check
            {
                _Arguments[symbol] = argument;
            }
        }

        public bool GetArgument(int symbol)
        {
            if (symbol > _Arguments.Length - 1 || symbol < 0) // out of bounds check
            {
                return _Arguments[symbol];
            }
            else
            {
                throw new System.ArgumentException("Invalid symbol (out of bounds)");
            }
        }

    }
}
