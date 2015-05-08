using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication3
{
    public class Proposition
    {
        private Proposition _ARef;
        private Proposition _BRef;
        private bool _A;
        private bool _B;
        private bool _IsRoot;

        public Proposition(bool Isroot)
        {
            throw new System.NotImplementedException();
        }

        public bool IsTrue(bool[] Arguements)
        {
            throw new System.NotImplementedException();
        }

        public Symbols[] Requirements()
        {
            throw new System.NotImplementedException();
        }
    }
}
