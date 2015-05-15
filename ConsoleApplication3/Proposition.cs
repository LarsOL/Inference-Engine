using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class Proposition
    {
        /// <summary>
        /// unset int = -1
        /// unset references = null
        /// </summary>


        public Proposition(int LValue, string Operation, int RValue)
        {
            // _A and _B reference the key of the variables we came across. In standard alphabet _A = 4 means the 'D' variable in the input proposition
            _A = LValue;
            _B = RValue;

            setO(Operation);
        }
        
        /// <summary>
        /// Overload for "NOT" proposition
        /// </summary>
        /// <param name="Operation"></param>
        /// <param name="RValue"></param>
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
        
        /// <summary>
        /// Basic defult constructor
        /// </summary>
        public Proposition()
        {
            _BRef = null;
            _ARef = null;
            _IsRoot = false;
            _A = -1;
            _B = -1;
        }

        public bool _IsRoot { get; set; }

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
        
        /// <summary>
        /// 
        /// </summary>
        public Operations _Operation;
        public void setO(string O)
        {
            switch (O)
            {
                case "!":
                    throw new System.NotSupportedException(); // The NOT relationship shouldn't have 2 variables
                case "&":
                    _Operation = Operations.Conjunction;
                    break;
                case "|":
                    _Operation = Operations.Disjunction;
                    break;
                case "=>":
                    _Operation = Operations.Implication;
                    break;
                case "<=>":
                    _Operation = Operations.Biconditional;
                    break;
            }
        }
        

        public bool IsTrue(bool[] Arguements)
        {
           bool result;
           switch (_Operation)
           {
               case Operations.Negation:                // NOT
                   result = !Arguements[_A];
                   break;
               case Operations.Conjunction:             // AND
                   result = Arguements[_A] && Arguements[_B] ;
                   break;
               case Operations.Disjunction:             // OR
                   result = Arguements[_A] || Arguements[_B];
                   break;
               case Operations.Implication:             // True when either A value = 0 or B value = 1, thus false when neither occur
                   if ((Arguements[_A] == true) && (Arguements[_B] == false))
                       result = false;
                   else
                       result = true;
                   break;
               case Operations.Biconditional:           // True only when A value is equal to B value
                   if (Arguements[_A] == Arguements[_B])
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
