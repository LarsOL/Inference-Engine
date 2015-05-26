using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public class BackwardsChain
    {
        List<Proposition> pastProps = new List<Proposition>();
        private Model _Model;
        private World _StartWorld;

        public BackwardsChain(Model ProgramModel, World ProblemSpace)
        {
            _Model = ProgramModel;
            _StartWorld = ProblemSpace;
            
        } 
        /// <summary>
        /// Control function that takes the result and prints it out
        /// </summary>
        public void WorkShiznitOut()
        {
            if (IsOriginal(_StartWorld._Goal))
            {
                //work is done fool
            }

            List<int> result = ShiznitOfDoom(_StartWorld._Goal);
            if(result.Count()==0)
            {
                Console.WriteLine("No Path Found");
            }
            else
            {
                Console.WriteLine("Path Found ");
                foreach (int item in result)
                {

                    Console.Write(_Model.GetName(item));
                }
            }
        }

        /// <summary>
        /// A DFS to find paths that return only original propositions from the test file
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<int> ShiznitOfDoom(Proposition value)
        {
            List<int> result = new List<int>();
           
            List<Proposition> temp;
            // Singualr Proposition
            if(value.Single)
            {
                if (IsOriginal(value))
                {
                    result = new List<int>();
                    result.Add(value.getA());
                    return result;
                }
                else if ((temp = FindReferal(value)).Count != 0)
                {
                    foreach (Proposition Refered in temp)
                    {
                        result = ShiznitOfDoom(Refered);
                        if (result.Count != 0)
                        {
                            result.Add(Refered.getB());
                        }
                    }
                }
                else
                {
                    return new List<int>();
                }
            }
            // Implication Proposition
            else if(value.Operation == Operations.Implication)
            {
                if(value.IsAref())
                {
                    result = ShiznitOfDoom(value.getARef());
                }
                else if (IsOriginal(value.getA()))
                {
                    result = new List<int>();
                    result.Add(value.getA());
                    return result;
                }
                else if((temp = FindReferal(value.getA())).Count != 0)
                    {
                        foreach (Proposition Refered in temp)
                        {
                            result = ShiznitOfDoom(Refered);
                            if (result.Count != 0)
                            {
                                result.Add(Refered.getB());
                            }
                        }
                    return result;
                }
                else
                {
                    return new List<int>();
                }

            }
            // And Proposition
            else if (value.Operation == Operations.Conjunction)
            {
                List<int> MiniListA = new List<int>();
                List<int> MiniListB = new List<int>();
                // Find A referals
                // if a ref
                if(value.IsAref())
                {
                    MiniListA = ShiznitOfDoom(value.getARef());
                }
                // If A is a Singular Original
                else if (IsOriginal(value.getA()))
                {
                    MiniListA = new List<int>();
                    MiniListA.Add(value.getA());

                }
                // Else find referals of A
                else if ((temp = FindReferal(value.getA())).Count != 0)
                    {
                        foreach (Proposition Refered in temp)
                        {
                            MiniListA = ShiznitOfDoom(Refered);
                            if (MiniListA.Count() != 0)
                            {
                                MiniListA.Add(value.getA());
                            }
                        }
                }
                // If no referals
                else
                {
                    return new List<int>();
                }

                // Find B referals
                // If b ref
                if (value.IsBref())
                {
                    MiniListB = ShiznitOfDoom(value.getBRef());
                }
                // If B is a Singular Original
                else if (IsOriginal(value.getB()))
                {
                    MiniListB = new List<int>();
                    MiniListB.Add(value.getB());
                }
                // else find referals
                else if ((temp = FindReferal(value.getB())).Count != 0)
                    {
                        foreach (Proposition Refered in temp)
                        {
                            MiniListB = ShiznitOfDoom(Refered);
                            if (MiniListB.Count() != 0)
                            {
                                MiniListB.Add(value.getB());
                            }
                        }
                }
                // If no referals 
                else
                {
                    return new List<int>();
                }

                // If nether null then add together and return
                if((MiniListA.Count() == 0) || (MiniListB.Count() == 0))
                {
                    return new List<int>();
                }

                result = new List<int>();
                result.AddRange(MiniListA);
                result.AddRange(MiniListB);
                return result;


            }
            // OR Proposition
            else if (value.Operation == Operations.Disjunction)
            {
                List<int> MiniListA = new List<int>();
                List<int> MiniListB = new List<int>();
                // Find referals of A
                // If A ref
                if (value.IsAref())
                {
                    MiniListA = ShiznitOfDoom(value.getARef());
                }
                // If A is a Singular Original 
                else if (IsOriginal(value.getA()))
                {
                    MiniListA = new List<int>();
                    MiniListA.Add(value.getA());

                }
                // Else find referals
                else if ((temp = FindReferal(value.getA())).Count != 0)
                    {
                        foreach (Proposition Refered in temp)
                        {
                            MiniListA = ShiznitOfDoom(Refered);
                            if (MiniListA.Count() != 0)
                            {
                                MiniListA.Add(value.getA());
                            }
                        }
                }

                //Find referals of B
                // If B ref
                if (MiniListA.Count() == 0)
                {
                    if (value.IsBref())
                    {
                        MiniListB = ShiznitOfDoom(value.getBRef());
                    }
                    // If B is a Singular Original
                    else if (IsOriginal(value.getB()))
                    {
                        MiniListB = new List<int>();
                        MiniListB.Add(value.getB());

                    }
                    // Else find referals
                    else if ((temp = FindReferal(value.getB())).Count != 0)
                    {
                        foreach (Proposition Refered in temp)
                        {
                            MiniListB = ShiznitOfDoom(Refered);
                            if (MiniListB.Count() != 0)
                            {
                                MiniListB.Add(value.getB());
                            }
                        }
                    }
                }

                // If both empty, return empty
                if (MiniListA.Count == 0 && MiniListB.Count == 0)
                {
                    return new List<int>();
                }
                //else add together and return
                result = new List<int>();
                result.AddRange(MiniListA);
                result.AddRange(MiniListB);
                return result;

            }

            // Finally return the result
            return result;


        }

        /// <summary>
        /// Find propositions that infer value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<Proposition> FindReferal(Proposition value)
        {
            List<Proposition> Referals = new List<Proposition>();
            // Check against each proposition in KB
            foreach (Proposition item in _StartWorld._KnowledgeBase.Propostions)
            {
                // For a proposition that refers to value
                if (item.getB() == value.getA())
                {
                    bool beenThere = false;
                    // If found check that proposition has not already been used
                    foreach (Proposition past in pastProps)
                    {
                        if (past == item)
                            beenThere = true;
                    }
                    // If it hasn't. USE IT
                    if(!beenThere)
                    {
                        pastProps.Add(item);
                        Referals.Add(item);
                    }
                }
            }
            return Referals; 
        }
        public List<Proposition> FindReferal(int value)
        {
            List<Proposition> Referals = new List<Proposition>();
            // Check against each proposition in KB
            foreach (Proposition item in _StartWorld._KnowledgeBase.Propostions)
            {
                // For a proposition that refers to value
                if (item.getB() == value)
                {
                    bool beenThere = false;
                    // If found check that proposition has not already been used
                    foreach (Proposition past in pastProps)
                    {
                        if (past == item)
                            beenThere = true;
                    }
                    // If it hasn't. USE IT
                    if (!beenThere)
                    {
                        pastProps.Add(item);
                        Referals.Add(item);
                    }
                }
            }
            return Referals;
        }

        /// <summary>
        /// Finds if value is a single original proposition from the test file
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsOriginal(Proposition value)
        {
            foreach (Proposition item in _StartWorld._KnowledgeBase.Propostions)
            {
                if (item.Single && (item.getA() == value.getA()))
                {
                    if (item.ANotted)
                    {
                        return false;
                    }
                    
                    return true;
                }
            }
            return false;
        }
        public bool IsOriginal(int value)
        {
            foreach (Proposition item in _StartWorld._KnowledgeBase.Propostions)
            {
                if (item.Single && (item.getA() == value))
                {
                    if(item.ANotted)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }   
}
