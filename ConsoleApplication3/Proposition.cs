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

            this.Operation = Operation;
        }
        // Defult constructor
        public Proposition()
        {
            _A = -1;
            ANotted = false;
            _ARef = null;

            _B = -1;
            BNotted = false;
            _BRef = null;

            _IsRoot = false;
            Operation = Operations.NotSet;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns>True means b is a ref, false b is a symbol</returns>
        public bool IsBref()
        {
            return _BRef != null;
        }
        public bool IsAref()
        {
            return _ARef != null;
        }
        public bool BisSymbol()
        {
            if (_B == -1)
                return false;
            else
                return true;
        }
//......Operation.........................................................
        public Operations Operation { get; set; }
        
//......IsTrue............................................................
        public bool? IsTrue(bool?[] Arguements)
        {
            bool? left, right;
            int leftSize = 1;
            if (IsAref())
            {
                leftSize = _ARef.Requirements().Count;
                bool?[] leftarr = new bool?[leftSize];
                Array.Copy(Arguements, 0, leftarr,0, leftarr.Length);
                left = _ARef.IsTrue(leftarr);
            }
            else
            {
                left = Arguements[0];
            }

            bool?[] rightarr = new bool?[Arguements.Length - leftSize];
            Array.Copy(Arguements, leftSize, rightarr, 0, rightarr.Length);
            if (IsBref())
            {
                right = _BRef.IsTrue(rightarr);
            }
            else
            {
                right = rightarr[0];
            }
             
            // NOT
            if (ANotted)
                left = !left;
            if (BNotted)
                right = !right;

            // Other Operations
            switch (Operation)
            {
                case Operations.NotSet:
                    throw new System.NotSupportedException();
                case Operations.Negation:                // NOT
                    throw new System.NotSupportedException();
                case Operations.Conjunction:             // AND
                   return left & right ;
                case Operations.Disjunction:             // OR
                   return left | right;
                case Operations.Implication:             // True when either A value = 0 or B value = 1, thus false when neither occur
                   if ((left == true) && (right == false))
                       return false;
                   else if ((!left|right)== true)
                       return true;
                   else
                       return null;

                case Operations.Biconditional:           // True only when A value is equal to B value
                   if ((left == null) || (right == null))
                       return null;
                   else if (left == right)
                       return true;
                   else
                       return false;
                default:
                   throw new System.ArgumentException("Something went wrong ...... oh noes");
           }
        }

        /// <summary>
        /// int is the symbol
        /// </summary>
        public List<int> Requirements()
        {
            List<int> requirements = new List<int>();
            if (IsAref())
            {
                requirements.AddRange(_ARef.Requirements());
            }
            else
            {
                requirements.Add(_A);
            }
            if (IsBref())
            {
                requirements.AddRange(_BRef.Requirements());
            }
            else
            {
                requirements.Add(_B);
            }
            return requirements;
        }

        public Proposition TryInfer(bool?[] Arguements)
        {
            if ((_ARef != null) && (_ARef.IsTrue(Arguements) == true))
                if (_BRef != null)
                    return _BRef;
                else
                    return _B;


                ;
            return null;
        }

        
    }
}
