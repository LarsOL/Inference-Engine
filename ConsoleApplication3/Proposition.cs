using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class Proposition
    {

        /// <param name="LValue">V</param>
        public Proposition(int LValue, string Operation, int RValue)
        {
            throw new System.NotImplementedException();
        }

        public Proposition()
        {
            _BRef = null;
            _ARef = null;
            _IsRoot = false;
            _A = -1;
            _B = -1;
            _Operation = Operations.NotSet;
        }

        public bool _IsRoot { get; set; }
        /// <summary>
        /// -1 is not set
        /// </summary>
        public int _A { get; set; }
        public Proposition _ARef { get; set; }
        /// <summary>
        /// -1 is not set
        /// </summary>
        public int _B { get; set; }
        public Proposition _BRef { get; set; }
        public Operations _Operation { get; set; }
        

        public bool IsTrue(bool[] Arguements)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// int is the symbol
        /// </summary>
        public int[] Requirements()
        {
            throw new System.NotImplementedException();
        }
    }
}
