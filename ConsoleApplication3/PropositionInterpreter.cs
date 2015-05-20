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

        /// <summary>
        /// Returns a array of parsed propositions as a Proposition[]
        /// Goal prop is last one
        /// </summary>
        /// <param name="Propositions">Takes in a String[] of propostion in english, assumes last one is goal</param>
        /// <returns></returns>
        public Proposition[] ParseProps(string[] Propositions)
        {
            Proposition[] result = new Proposition[Propositions.Length];
            for(int i = 0 ; i < Propositions.Length;i++)
            {
                string[] splitted = SplitString(Propositions[i]);
                result[i] = fringecases(splitted);
                
               
            }
            return result;

        }

        private Proposition fringecases(String[] Prop)
        {
            if (Prop.Length == 1)
            {
                return SetProp(Prop); // single arguement prop no need to parse
            }
            else
            {
                int inferCount = 0;
                int inferPos =0;
                for (int i = 0; i < Prop.Length; i++)
                { 
                    if(Prop[i].Contains("=>"))
                    {
                        inferCount++;
                        inferPos = i;
                    }
                }
                if(inferCount == 1)
                {
                    Proposition CurrentProp = new Proposition();
                    string[] left = new string[inferPos]; //room for left of infer
                    string[] right = new string[Prop.Length - left.Length - 1]; // room for what is left minus infer sign
                    Array.Copy(Prop, 0, left, 0, left.Length); 
                    Array.Copy(Prop, inferPos + 1, right, 0, right.Length);
                    CurrentProp = SetProp(left, Operations.Implication, right);
                    return CurrentProp;
                }
                else
                {
                    return generateProp(Prop); //multi arguement prop, everything bracketed properly
                }
                    

            }
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
        /// Wrapeper around setprop that accepts a single prop
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private Proposition SetProp(string[] prop)
        {
            return SetProp(prop, Operations.NotSet, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="oper"></param>
        /// <param name="right">If right is null make a single prop</param>
        /// <returns></returns>
        private Proposition SetProp(string[] left ,Operations oper, string[] right) 
        {

            Proposition returnProp = new Proposition();

            
            if(left.Length == 1){
                if(left[0][0] == '~') // "~A"
                {
                   left[0] = left[0].Substring(1);
                   returnProp.ANotted = true;
                }
                returnProp.setA(String2Symbol(left[0]));
            }
            else
            {
                returnProp.setA(generateProp(left));
            }

            if (right != null) // multi argument prop
            {
                if (right.Length == 1)
                {
                    if (right[0][0] == '~') // "~A"
                    {
                        right[0] = right[0].Substring(1);
                        returnProp.BNotted = true;
                    }
                    returnProp.setB(String2Symbol(right[0]));
                }
                else
                {
                    returnProp.setB(generateProp(right));
                }
                returnProp.Operation = oper;
            }

           

            return returnProp;
        }

        private Proposition generateProp(string[] PropositionString)
        {

           Proposition CurrentProp = new Proposition();
                  
           if(PropositionString.Length  == 3){ // base case
                return SetProp(new[] {PropositionString[0]},String2Operation(PropositionString[1]),new[] {PropositionString[2]});
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
                
                if(PropositionString[i].Contains("("))
                {
                    
                    if(ParenthesisCount == 0) // found the start of a top level braket
                    {
                        Parenthesist_pos[0] = i;
                        if(i > 0) // Dont go out of array bounds
                        {
                            try{ // see if there is a valid operation linker
                            possible_linkers[0] = String2Operation(PropositionString[i-1]);

                            if (possible_linkers[0] == Operations.Negation) // if it is a not, look past it
                            {
                                if (i > 1) 
                                {
                                    possible_linkers[0] = String2Operation(PropositionString[i - 2]); 
                                }
                                else
                                {
                                    possible_linkers[0] = Operations.NotSet;
                                }
                            }
                            }
                            catch (System.ArgumentException)
                            {
                                throw new System.ArgumentException("An operation should always come before a opening bracket");
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

                        if (Parenthesist_pos[0] == 1 && Parenthesist_pos[1] == PropositionString.Length - 1 && PropositionString[0] == "~")// brackets around whole thing but the not
                        {
                            string[] stripbrackets = new string[PropositionString.Length - 3];
                            Array.Copy(PropositionString, 2, stripbrackets, 0, stripbrackets.Length);
                            CurrentProp = SetProp(stripbrackets);
                            CurrentProp.ANotted = true;
                            return CurrentProp;
                        }
                       
                        if(i < PropositionString.Length - 1) // Dont go out of array bounds
                            {
                                try{ // see if there is a valid operation linker
                                possible_linkers[1] = String2Operation(PropositionString[i+1]); 
                                }
                                catch (System.ArgumentException)
                                {
                                    throw new System.ArgumentException("An operation should always come after a closing bracket");
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
                            CurrentProp = SetProp(left,possible_linkers[1],right);
                            if (Parenthesist_pos[0] > 0 && PropositionString[Parenthesist_pos[0]- 1][0] == '~')
                            {
                                CurrentProp.ANotted = true;
                            }
                            return CurrentProp;
                        }

                        if (possible_linkers[1] == Operations.NotSet) // if brakets are on right side
                        {
                            string[] right = new string[Parenthesist_pos[1] - Parenthesist_pos[0] - 1]; //room what is in brakets
                            string[] left = new string[PropositionString.Length - right.Length - 3]; // room for what is left (- 2 for prop & 0 indexed array)
                            Array.Copy(PropositionString, 0, left, 0, left.Length); // put what is in brakets in left
                            Array.Copy(PropositionString, Parenthesist_pos[0] + 1, right, 0, right.Length); // put what is left in right
                            CurrentProp = SetProp(left, possible_linkers[0], right);
                            if (Parenthesist_pos[0] > 0 &&  PropositionString[Parenthesist_pos[0] -1 ][0] == '~')
                            {
                                CurrentProp.BNotted = true;
                            }
                            return CurrentProp;
                        }
                        
                    }
                    
                    if(ParenthesisCount < 0)
                        throw new System.ArgumentException("Too many closing brackets in Prop");
                    
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
            CurrentProp = SetProp(new[] { PropositionString[0] }, String2Operation(PropositionString[1]), rightprop);
          

          
            return CurrentProp;
        }
    }
}
