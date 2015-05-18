using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class KnowledgeBase
    {
        private Proposition[] _Propostions;

        public KnowledgeBase(InferenceEngine.Proposition[] Porpositions)
        {
            _Propostions = Porpositions;
        }

        public void IsTrue(int PropositionNo, bool[] Arguements)
        {
            _Propostions[PropositionNo].IsTrue(Arguements);
        }
    }
}
