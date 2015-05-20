using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class KnowledgeBase
    {
        private List<Proposition> _Propostions;

        public int Length
        {
            get
            {
                return _Propostions.Count;
            }
        }

        public KnowledgeBase(InferenceEngine.Proposition[] Porpositions)
        {
            _Propostions.AddRange(Porpositions);
        }

        public bool? IsTrue(int PropositionNo, bool?[] Arguements)
        {
            return _Propostions[PropositionNo].IsTrue(Arguements);
        }

        public int[] Requirements(int PropositionNo)
        {
            return _Propostions[PropositionNo].Requirements().ToArray();
        }
        public void AddToKB(Proposition PTemp)
        {
            _Propostions.Add(PTemp);
        }
    }
}
