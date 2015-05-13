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
                null;
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

            int ParenthesisCount = 0;
            int deepest_Parenthesis = 0;
            bool containsParenthesis = false;
            int SetShift = 0;
            List<Operations> possible_linkers = new List<Operations>(); // before & after the first ( and before and after the last )
            List<int> Parenthesist_pos = new List<int>();


            for(int i = 0 ; i < PropositionString.Length ; i++)
            {
                
                if(PropositionString[i].Contains("("))
                {
                    
                    if(ParenthesisCount == 0) // found the start of a top level braket
                    {
                        if(i > 0) // Dont go out of array bounds
                        {
                            try{ // see if there is a valid operation linker
                            possible_linkers.Add(String2Operation(PropositionString[i-1])); 
                            }
                            catch (System.ArgumentException e)
                            {
                                // not vaild skip
                            }
                        }                        
                    }
                    
                    ParenthesisCount++;

                    if(i == PropositionString.Length - 1 && ParenthesisCount != 0)
                        throw new System.ArgumentException("Miss matched Parenthesis in Propostion");

                }
                else if(PropositionString[i].Contains(")"))
                {
                    ParenthesisCount--;

                    if(ParenthesisCount == 0) // found the end of a top level braket
                    {
                        if(i < PropositionString.Length - 1) // Dont go out of array bounds
                            {
                                try{ // see if there is a valid operation linker
                                possible_linkers.Add(String2Operation(PropositionString[i+1])); 
                                }
                                catch (System.ArgumentException e)
                                {
                                    // not vaild skip
                                }
                            }
                    }
                    
                    if(ParenthesisCount < 0)
                        throw new System.ArgumentException("Miss matched Parenthesis in Propostion");
                    if(i == PropositionString.Length - 1 && ParenthesisCount != 0)
                        throw new System.ArgumentException("Miss matched Parenthesis in Propostion");
                }


            }

            //If there are no brakets continure bellow

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
         
             
           
            //find highest brakets, left to right on even levels
            // pass whats inside brakets through
            // take inside brakets recurse through function
            return CurrentProp;
        }
    }
}
