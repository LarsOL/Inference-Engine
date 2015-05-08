using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication3
{
    public enum Operations
    {
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
