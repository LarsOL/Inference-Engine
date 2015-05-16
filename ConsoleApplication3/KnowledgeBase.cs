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

        public KnowledgeBase(InferenceEngine.Proposition[] Porpositions)
        {
            throw new System.NotImplementedException();
        }

        public bool IsTrue(int PropositionNo, bool[] Arguements)
        {
            throw new System.NotImplementedException();
        }

        public int[] Requirements(int PropositionNo)
        {
            throw new System.NotImplementedException();
        }
    }
}
