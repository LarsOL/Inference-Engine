using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class PropositionInterpreter
    {
         private Model _Model;
        
        public PropositionInterpreter(ref Model ProgramModel)
        {

            _Model = ProgramModel;
        }

        public Proposition[] ParseProps(string[] Propositions)
        {
            foreach (string prop in Propositions)
            {
                string[] splitted = SplitString(prop);
                Proposition temp = generateProp(splitted);
                int a = 1 + 1;
            }
            return null;

        }

        private void String2Symbols(string[] Proposition)
        {
            foreach(string single in Proposition){
                // if it isnt a operator, it is a symbol and add to our table
                String2Symbol(single);
            }
        }

        private int String2Symbol(string InputString){
        
            if (InputString != "^" || InputString != "v" || InputString != "=>" || InputString != "&" || InputString != "!" || InputString != "(" || InputString != ")"){
                    _Model.SetName(InputString);
                    return _Model.GetIndexOfName(InputString);
               }
            else
                throw new System.ArgumentException("Not a symbol varible");
            
        }

        private Operations String2Operation(string InputString){
            switch(InputString)
            {
                case "^":
                    return Operations.Conjunction;             
                case "&":
                    return Operations.Conjunction;                  
                case "v":
                    return Operations.Disjunction;
                case "!":
                    return Operations.Negation;                
                case "=>":
                    return Operations.Implication;
                case "<=>":
                    return Operations.Biconditional;             

            }
            throw new System.ArgumentException("Not a operation varible");
        }
        

 

        private string[] SplitString(string Proposition)
        {
            string pattern = "(v|=>|&|" + System.Text.RegularExpressions.Regex.Escape("^") + "|" + System.Text.RegularExpressions.Regex.Escape("(") + "|" + System.Text.RegularExpressions.Regex.Escape(")")  + ")";
            return System.Text.RegularExpressions.Regex.Split(Proposition, pattern) ;
        }

        private Proposition generateProp(string[] PropositionString)
        {
            int ParenthesisCount = 0;
            bool containsParenthesis = false;

           Proposition CurrentProp = new Proposition();
           
                

            if(PropositionString.Length == 3){ // base case
                CurrentProp._A = String2Symbol(PropositionString[0]);
                CurrentProp._Operation = String2Operation(PropositionString[1]);
                CurrentProp._B = String2Symbol(PropositionString[2]);
                return CurrentProp;
            }
            if (PropositionString.Length <= 3)
            {
                throw new System.ArgumentException("Less than 3 agruements in base case");
            }

            foreach (string single in PropositionString)
            {
                if(single.Contains("("))
                {
                    containsParenthesis = true;
                    break;
                }
            }

            if(!containsParenthesis) // if on same bracket lvl string together
            {
                string[] right = new string[PropositionString.Length - 2];
                Array.Copy(PropositionString, 2, right, 0, PropositionString.Length - 2);
                CurrentProp._A = String2Symbol(PropositionString[0]);
                CurrentProp._Operation = String2Operation(PropositionString[1]);
                CurrentProp._BRef = generateProp(right);

                /* info on leaf nodes
                string[] left = new string[3];
                string[] right = new string[PropositionString.Length -2];
                Array.Copy(PropositionString,0,left,0,3);
                Array.Copy(PropositionString,2,right,0,PropositionString.Length-2);
                CurrentProp._ARef = generateProp(left);
                CurrentProp._BRef = generateProp(right);
                 */
            }
             
           
            //find highest brakets, left to right on even levels
            // pass whats inside brakets through
            // take inside brakets recurse through function
            return CurrentProp;
        }
    }
}
