using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace InferenceEngine
{
    public class FileInput
    {
        string _FilePath;
        /// <summary>
        /// constructor, stores file path
        /// </summary>
        /// <param name="FilePath"></param>
        public FileInput(string FilePath)
        {
            _FilePath = FilePath;
        }

        /// <remarks>Last one is the goal,  NoPropositions White Space</remarks>
        /// <summary>
        /// reads file and returns a string array of rules for KB
        /// </summary>
        /// <remarks>Last one is the goal,  No White Space, we made a change, is this in master?</remarks>
        /// <returns></returns>
        public string[] ReadFromFile()
        {
		    try
		    {
			    //create file reading objects
                StreamReader reader = new StreamReader(_FilePath);

                // reads first line (Tell)
                reader.ReadLine();

                // read second line, rules for K.B
			    String Tell = reader.ReadLine();

                // reads thrid line (Ask)
                reader.ReadLine();

                // read 4th Line, Quiry for engine
                String Ask = reader.ReadLine().Trim();

                // remove white space and split tell into an array of rules
                String[] SplitTell = RemoveWhiteSpace(Tell).Split(';');

                // adds the quiry to the end of the array
                //String[] result = AddToEnd(SplitTell, Ask);
                SplitTell[SplitTell.Length - 1] =  Ask;
                //close file reader stream to release file
                reader.Close();

                //returns the result 
                return SplitTell;
		    }
            catch (FileNotFoundException ioEx)
            {
                Console.WriteLine(ioEx.Message);
            }
		    catch(IOException  e)
		    {
			    //There was an IO error, show ann error message
                System.Console.WriteLine("Error in reading \"" + _FilePath);
                Console.WriteLine(e.Message);
			    System.Environment.Exit(1);
		    }
		
		    //this code should be unreachable. This statement is simply to satisfy IDE.
            throw new Exception();
	    }

        /// <summary>
        /// makes a copy of a string with no white space
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public string RemoveWhiteSpace(string Input)
        {

            return new string(Input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }

        /// <summary>
        /// a function for adding on the goal to the end of the array
        /// </summary>
        /// <param name="input">string array</param>
        /// <param name="end">thing you want to add to the end</param>
        /// <returns></returns>
        public string[] AddToEnd(string[] input, string end)
        {

            string[] result = new string[(input.Count() + 1)];


            for (int i = 0; i < input.Count() - 1; i++)
            {
                result[i] = input[i];
            }

            result[input.Count()] = end;

            return result;
        }
    }
}
