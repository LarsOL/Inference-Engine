using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = " " + true;
            System.Console.WriteLine(test);
            bool[] arguements = new bool[3];
            for (int i = 0; i < (1 << 3); i++)
            {  // modifed from https://stackoverflow.com/questions/12488876/all-possible-combinations-of-boolean-variables
                string line = "";
                for (int j = 0; j < 3; j++)
                {
                    
                    arguements[j] = ((i & (1 << j)) != 0);
                    if (arguements[j])
                    {
                        line += "T   ";
                    }
                    else
                    {
                        line += "F   ";
                    }

                
                }
                System.Console.WriteLine(line);
            
            }
           int  num = Convert.ToInt32(Console.ReadLine());

        }
    }
}
