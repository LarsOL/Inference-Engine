using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class KnowledgeBase
    {
        private Proposition[] _Propostions;

        public int Length
        {
            get 
            {
                return _Propostions.Length;
            }
        }

        public KnowledgeBase(InferenceEngine.Proposition[] Propositions)
        {
            _Propostions = Propositions;
        }

        public bool IsTrue(int PropositionNo, bool[] Arguements)
        {
            return _Propostions[PropositionNo].IsTrue(Arguements);
        }

        public int[] Requirements(int PropositionNo)
        {
            return _Propostions[PropositionNo].Requirements().ToArray();
        }
    }
}
