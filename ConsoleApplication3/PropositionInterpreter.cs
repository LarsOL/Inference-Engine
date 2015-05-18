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
            Proposition[] result = new Proposition[Propositions.Length];
            for(int i = 0 ; i < Propositions.Length;i++)
            {
                string[] splitted = SplitString(Propositions[i]);
               result[i] = generateProp(splitted);
            }
            return result;

        }

        private void String2Symbols(string[] Proposition)
        {
            foreach(string single in Proposition){
                // if it isnt a operator, it is a symbol and add to our table
                String2Symbol(single);
            }
        }

        private int String2Symbol(string InputString){
        
          _Model.SetName(InputString);
          return _Model.GetIndexOfName(InputString);
           
        }

        private Operations String2Operation(string InputString){
            switch(InputString)
            {
                case "&":
                    return Operations.Conjunction;                  
                case "|":
                    return Operations.Disjunction;
                case "~":
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
            string pattern = "(<=>|=>|&|" + System.Text.RegularExpressions.Regex.Escape("|") + "|" + System.Text.RegularExpressions.Regex.Escape("(") + "|" + System.Text.RegularExpressions.Regex.Escape(")")  + ")";
            string[] ans = System.Text.RegularExpressions.Regex.Split(Proposition, pattern) ;
            ans = ans.Where(x => !string.IsNullOrEmpty(x)).ToArray(); // deal with symbols next to each other making empty strings
            return ans;
        }
        /// <summary>
        /// Parse for the not and set the side
        /// </summary>
        /// <param name="SetingProp"></param>
        /// <param name="srtPropostion"></param>
        /// <param name="side">true is left side, false is right</param>

        private Proposition SetProp(ref Proposition SetingProp ,string srtPropostion,bool side) 
        {
            if(srtPropostion[0] == '~')
            {
                srtPropostion.Substring(1);
                if(side)
                {
                    SetingProp.IsnottedA = true;
                }
                else
                {
                    SetingProp.IsnottedB = true;
                }
            }

            if(side)
            {
                SetingProp.setA(String2Symbol(srtPropostion))
            }
            else
            {
                SetingProp.setB(String2Symbol(srtPropostion))
            }
        }

        private List<int> NotsPosition(string[] PropositionString)
        {
            List<int> Position = new List<int>;
            for(int i = 0; i < PropositionString.Length; i++)
            {
                if (PropositionString[i] == "~")
                    Position.Add(i);
            }
            return Position;
        }

        private Proposition generateProp(string[] PropositionString)
        {
                     
           Proposition CurrentProp = new Proposition();

           List<int> PositionNots = NotsPosition(PropositionString);
           int AmountNots = PositionNots.Count;

           if(PropositionString.Length  == 3){ // base case
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
            Operations[] possible_linkers = new Operations[2]{Operations.NotSet,Operations.NotSet}; // before & after the first ( and before and after the last )
            int[] Parenthesist_pos = new int[2];


            for(int i = 0 ; i < PropositionString.Length ; i++) //find high level brakets, srtring together prop left to right
            {
                
                if(PropositionString[i] == "(")
                {
                    
                    if(ParenthesisCount == 0) // found the start of a top level braket
                    {
                        Parenthesist_pos[0] = i;
                        if(i > 0) // Dont go out of array bounds
                        {
                            try{ // see if there is a valid operation linker
                            possible_linkers[0] = String2Operation(PropositionString[i-1]); 
                            }
                            catch (System.ArgumentException e)
                            {
                                // not vaild skip
                            }
                        }                        
                    }
                    
                    ParenthesisCount++;


                }
                else if(PropositionString[i] == ")")
                {
                    ParenthesisCount--;

                    if(ParenthesisCount == 0) // found the end of a top level braket
                    {
                        Parenthesist_pos[1] = i;
                        if (Parenthesist_pos[0] == 0 && Parenthesist_pos[1] == PropositionString.Length - 1)// strip brackets if they are around the whole term
                        {
                             string[] stripbrackets = new string[PropositionString.Length - 2]; 
                             Array.Copy(PropositionString, 1, stripbrackets, 0, stripbrackets.Length);
                             return generateProp(stripbrackets);
                           
                        }
                       
                        if(i < PropositionString.Length - 1) // Dont go out of array bounds
                            {
                                try{ // see if there is a valid operation linker
                                possible_linkers[1] = String2Operation(PropositionString[i+1]); 
                                }
                                catch (System.ArgumentException e)
                                {
                                    // not vaild skip
                                }
                            }
                        if (possible_linkers[0] == Operations.NotSet && possible_linkers[1] == Operations.NotSet)
                        {
                             throw new System.ArgumentException("Parenthesis Or code error, no prop detected before or after highest lvl brakets");
                        }
                        // recurse - take  high level brackets put it on one side of prop, put whats left on other side
                        if(possible_linkers[0] == Operations.NotSet) // if brakets are on left side
                        {
                            string[] left = new string[Parenthesist_pos[1]- Parenthesist_pos[0] - 1]; //room what is in brakets
                            string[] right = new string[PropositionString.Length - left.Length - 3]; // room for what is left (- 1 for prop symbol -2 for brakets )
                            Array.Copy(PropositionString, Parenthesist_pos[0] + 1, left, 0, left.Length); // put what is in brakets in left
                            Array.Copy(PropositionString, Parenthesist_pos[1] + 2, right, 0, right.Length); // put what is left in right
                            CurrentProp._ARef = generateProp(left);
                            CurrentProp._Operation = possible_linkers[1];
                            if(right.Length == 1) // if it is next to a symbol not prop
                            {
                                CurrentProp._B = String2Symbol(right[0]);
                            }
                            else
                            {
                                CurrentProp._BRef = generateProp(right);
                            }
                            return CurrentProp;
                        }

                        if (possible_linkers[1] == Operations.NotSet) // if brakets are on right side
                        {
                            string[] right = new string[Parenthesist_pos[1] - Parenthesist_pos[0] - 2]; //room what is in brakets
                            string[] left = new string[PropositionString.Length - right.Length - 2]; // room for what is left (- 2 for prop & 0 indexed array)
                            Array.Copy(PropositionString, 0, left, 0, left.Length); // put what is in brakets in left
                            Array.Copy(PropositionString, Parenthesist_pos[0] + 1, right, 0, right.Length); // put what is left in right
                            CurrentProp._BRef = generateProp(right);
                            CurrentProp._Operation = possible_linkers[0];
                            if (left.Length == 1) // if it is next to a symbol not prop
                            {
                                CurrentProp._A= String2Symbol(left[0]);
                            }
                            else
                            {
                                CurrentProp._ARef = generateProp(right);
                            }
                            return CurrentProp;
                            
                        }
                        
                    }
                    
                    if(ParenthesisCount < 0)
                        throw new System.ArgumentException("Miss matched Parenthesis in Propostion");
                    
                }
                if(i == PropositionString.Length - 1 && ParenthesisCount != 0)
                     throw new System.ArgumentException("Miss matched Parenthesis in Propostion");

            }

            if(ParenthesisCount != 0){
                throw new System.ArgumentException("Miss matched Parenthesis in Propostion (Code logic error)");
            }
            //If there are no brakets continue bellow

            string[] rightprop = new string[PropositionString.Length - 2];
            Array.Copy(PropositionString, 2, rightprop, 0, PropositionString.Length - 2);
            CurrentProp._A = String2Symbol(PropositionString[0]);
            CurrentProp._Operation = String2Operation(PropositionString[1]);
            CurrentProp._BRef = generateProp(rightprop);

            
            //find highest brakets, left to right on even levels
            // pass whats inside brakets through
            // take inside brakets recurse through function
            return CurrentProp;
        }
    }
}
