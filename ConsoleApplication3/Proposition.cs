using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class Proposition
    {
        /// <summary>
        /// Proposition holds 3 items. The first is the reference
        /// to the first value, second is the logical operation being
        /// performed, third is the reference to the second value. NOT
        /// is a toggled bool and hadled differenly to the other 
        /// operations as it only effects one of the values.
        /// 
        /// default unset values
        /// int = -1
        /// references = null
        /// bool = false
        /// </summary>



//......Constructor.......................................................
        public Proposition(int LValue, Operations Operation, int RValue)
        {
            // _A and _B reference the key of the variables we came across. In standard alphabet _A = 4 means the 'D' variable in the input proposition
            _A = LValue;
            _B = RValue;

            _Operation = Operation;
        }
        // Overload for "NOT" proposition
        public Proposition(string Operation, int RValue)
        {
            if (Operation != "!")
                throw new System.NotSupportedException(); // Relationships need 2 variables
            else
            {
                _Operation = Operations.Negation;
                _A = RValue;
                _B = -1;
            }
        }
        // Defult constructor
        public Proposition()
        {
            _A = -1;
            ANotted = false;
            _ARef = null;

            _B = -1;
            BNotted = true;
            _BRef = null;

            _IsRoot = false;
            _Operation = Operations.NotSet;
        }

        public bool _IsRoot { get; set; }
//......A.................................................................
        public bool ANotted { get; set; }
        private int _A;
        private Proposition _ARef;
        public void setA(int A)
        {
            _ARef = null; 
            _A = A;
        }
        public void setA(Proposition A)
        {
            _ARef = A;
            _A = -1;
        }
        public int getA()
        {
            return _A;
        }
        public Proposition getARef()
        {
            return _ARef;
        }
        public bool AisSymbol()
        {
            if (_A == -1)
                return false;
            else
                return true;
        }
 //.....B.................................................................
        public bool BNotted { get; set; }
        private int _B;
        private Proposition _BRef;
        public void setB(int B)
        {
            _BRef = null;
            _B = B;
        }
        public void setB(Proposition B)
        {
            _BRef = B;
            _B = -1;
        }
        public int getB()
        {
            return _B;
        }
        public Proposition getBRef()
        {
            return _BRef;
        }
        public bool BisSymbol()
        {
            if (_B == -1)
                return false;
            else
                return true;
        }
//......Operation.........................................................
        private Operations _Operation;
        public void setO(string O)
        {
            switch (O)
            {
                case "!":
                    _Operation = Operations.Negation;     // NOT ... relationship shouldn't have 2 variables
                    break;
                case "&":
                    _Operation = Operations.Conjunction;  // AND
                    break;
                case "|":
                    _Operation = Operations.Disjunction;  // OR
                    break;
                case "=>":
                    _Operation = Operations.Implication;
                    break;
                case "<=>":
                    _Operation = Operations.Biconditional;
                    break;
            }
        }
        public Operations getO()
        {
            return _Operation;
        }
//......IsTrue............................................................
        public bool IsTrue(bool[] Arguements)
        {

            bool A = Arguements[_A];
            bool B = Arguements[_B];
            bool result = false;                         // Default false

            // NOT
            if (ANotted)
                A = !A;
            if (BNotted)
                B = !B;

            // Other Operations
            switch (_Operation)
            {
                case Operations.NotSet:
                    throw new System.NotSupportedException();
                case Operations.Negation:                // NOT
                    throw new System.NotSupportedException();
                case Operations.Conjunction:             // AND
                   result = A && B ;
                   break;
                case Operations.Disjunction:             // OR
                   result = A || B;
                   break;
                case Operations.Implication:             // True when either A value = 0 or B value = 1, thus false when neither occur
                   if ((A == true) && (B == false))
                       result = false;
                   else
                       result = true;
                   break;
                case Operations.Biconditional:           // True only when A value is equal to B value
                   if (A == B)
                       result = true;
                   else
                       result = false;
                   break;
           }
           return result;
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
