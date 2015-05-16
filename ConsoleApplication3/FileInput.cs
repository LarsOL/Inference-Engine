using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class FileInput
    {
        string _FilePath;
        public FileInput(string FilePath)
        {
            _FilePath = FilePath;
        }

        /// <remarks>Last one is the goal,  NoPropositions White Space</remarks>
        /// <returns></returns>
        public string[] ReadFromFile()
        {
            throw new System.NotImplementedException();
        }
    }
}
