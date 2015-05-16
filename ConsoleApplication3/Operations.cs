using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public enum Operations
    {
        /// <summary>
        /// Enum Not set yet
        /// </summary>
        NotSet,
        /// <summary>
        /// Not
        /// </summary>
        Negation,
        /// <summary>
        /// And
        /// </summary>
        Conjunction,
        /// <summary>
        /// Or
        /// </summary>
        Disjunction,
        /// <summary>
        /// False - S1 = T, S2 = F
        /// </summary>
        Implication,
        /// <summary>
        /// Both way Implication
        /// </summary>
        Biconditional,
       
    }
}
