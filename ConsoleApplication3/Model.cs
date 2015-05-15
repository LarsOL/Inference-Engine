using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class Model
    {
        private List<string> _Names;

        public Model()
        {
            _Names = new List<string>();
        }

        public void SetName(string Name)
        {
            if (Name != "|" || Name != "<=>" || Name != "=>" || Name != "&" || Name != "~" || Name != "(" || Name != ")")
            {
                if (!_Names.Contains(Name)) //if name is not already in list 
                {
                    _Names.Add(Name);
                }
            }
            else
                throw new System.ArgumentException("Not a symbol varible");
            
          
        }

        public int GetIndexOfName(String Name)
        {
            return _Names.IndexOf(Name); //-1 on not found
        }

        public string GetName(int index)
        {
            if(index < 0 || index > _Names.Count)
                throw new System.ArgumentException("Out of array bounds (model)");
            return _Names[index];
        }
    }
}
