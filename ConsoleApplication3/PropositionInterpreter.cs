using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication3
{
    public class PropositionInterpreter
    {
        public PropositionInterpreter(string[] Propositions)
        {
            throw new System.NotImplementedException();
        }

        public Proposition[] ParseProps()
        {
            throw new System.NotImplementedException();
        }

        private string[] SplitString(string Proposition)
        {
            string pattern = "(v|=>|&|" + System.Text.RegularExpressions.Regex.Escape("^") + ")";
            return System.Text.RegularExpressions.Regex.Split(Proposition, pattern);
        }

        private Proposition generateProps(string[] Proposition)
        {
            throw new System.NotImplementedException();
        }
    }
}
