using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formele_methoden
{
    public class Regex
    {
        /// <summary>
        /// An enum class which holds all the supported operators within the Regex class
        /// </summary>
        public enum SupportedOperators{ PLUS, STAR, OR, DOT, ONE }

        /// <summary>
        /// The current operator within the regex
        /// </summary>
        private SupportedOperators currentOperator;

        /// <summary>
        /// The left and right part of the regex
        /// </summary>
        private Regex leftRegex= null;
        private Regex rightRegex = null;

        /// <summary>
        /// The current regex string
        /// </summary>
        private string currentRegex;

        /// <summary>
        /// A method to get/set the current operator outside of this class
        /// </summary>
        public SupportedOperators CurrentOperator { get => currentOperator; set => currentOperator = value; }

        /// <summary>
        /// A method to get/set the right/left part of the regex
        /// </summary>
        public Regex RightRegex { get => rightRegex; set => rightRegex = value; }
        public Regex LeftRegex { get => leftRegex; set => leftRegex = value; }

        /// <summary>
        /// A method to get/set the current regex
        /// </summary>
        public String CurrentRegex { get => currentRegex; set => currentRegex = value; }

        /// <summary>
        /// Constructor of the regex class
        /// </summary>
        /// <param name="givenRegex">The given refex which should be initialized</param>
        public Regex(string givenRegex)
        {
            currentRegex = givenRegex;
            currentOperator = SupportedOperators.ONE;
        }

        /// <summary>
        /// Constructor of the regex class
        /// </summary>
        public Regex()
        {
            currentRegex = "";
            currentOperator = SupportedOperators.ONE;
        }

        /// <summary>
        /// A method which adds a plus operator to the regex
        /// </summary>
        /// <returns>A new regex, a combination of the old regex and the new plus operator</returns>
        public Regex plus()
        {
            Regex toReturn = new Regex();
            toReturn.CurrentOperator = SupportedOperators.PLUS;

            // Initialize the old regex within thhe new regex
            toReturn.LeftRegex = this;

            return toReturn;
        }

        /// <summary>
        /// A method which adds a star operator to the regex
        /// </summary>
        /// <returns>A new regex, a combination of the old regex and the new star operator</returns>
        public Regex star()
        {
            Regex toReturn = new Regex();
            toReturn.CurrentOperator = SupportedOperators.STAR;

            // Initialize the old regex within thhe new regex
            toReturn.LeftRegex = this;

            return toReturn;
        }

        /// <summary>
        /// A method which can be used to add the 'or' operator to a regex
        /// </summary>
        /// <param name="givenRegex">The second part of the 'or' operator</param>
        /// <returns>A new regex, which combines the current and the new regex in an 'or' operation</returns>
        public Regex or(Regex givenRegex)
        {
            Regex toReturn = new Regex();
            toReturn.CurrentOperator = SupportedOperators.OR;

            // Initialize the left side of the 'or' operator (current regex)
            toReturn.LeftRegex = this;

            // Initialize the right side of the 'or' operator (given regex)
            toReturn.RightRegex = givenRegex;

            return toReturn;
        }

        /// <summary>
        /// A method which can be used to add the 'dot' operator to a regex
        /// </summary>
        /// <param name="givenRegex">The second part of the 'dot' operator</param>
        /// <returns>A new regex, which combines the current and the new regex in a 'dot' operation</returns>
        public Regex dot(Regex givenRegex)
        {
            Regex toReturn = new Regex();
            toReturn.CurrentOperator = SupportedOperators.DOT;

            // Initialize the left side of the 'dot' operator (current regex)
            toReturn.LeftRegex = this;

            // Initialize the right side of the 'dot' operator (given regex)
            toReturn.RightRegex = givenRegex;

            return toReturn;
        }

        /// <summary>
        /// A custom method, which allows for adding multiple items to a sorted set
        /// </summary>
        /// <param name="original">The original sorted set</param>
        /// <param name="toAdd">The sorted set, which should be added to the original</param>
        /// <returns>A sorted set containing the values of both the original and the new data</returns>
        private SortedSet<string> addAll(SortedSet<string> original, SortedSet<string> toAdd)
        {
            foreach (string s in toAdd)
            {
                original.Add(s);
            }

            return original;
        }

        /// <summary>
        /// A method which generates all acceptable combinations of a language in a sorted set
        /// </summary>
        /// <param name="maxSteps">The maximum amount of steps allowed</param>
        /// <returns>A sorted set, which contains all the accepted languages</returns>
        public SortedSet<string> getLanguage(int maxSteps)
        {
            SortedSet<string> toReturn = new SortedSet<string>(new SortByLength());
            SortedSet<string> emptyLanguage = new SortedSet<string>(new SortByLength());

            SortedSet<string> languageLeft, languageRight;

            switch(this.currentOperator)
            {
                default:
                    Console.WriteLine("This operator is not supported.");
                    break;

                case SupportedOperators.ONE:
                    // Add the regex to the return value
                    toReturn.Add(currentRegex);
                    break;

                case SupportedOperators.OR:
                    // Initialize the left language
                    if(leftRegex == null)
                    {
                        languageLeft = emptyLanguage;
                    } else
                    {
                        languageLeft = leftRegex.getLanguage(maxSteps - 1);
                    }

                    // Initialize the right language
                    if(rightRegex == null)
                    {
                        languageRight = emptyLanguage;
                    } else
                    {
                        languageRight = rightRegex.getLanguage(maxSteps - 1);
                    }

                    addAll(toReturn, languageLeft);
                    addAll(toReturn, languageRight);
                    break;

                case SupportedOperators.DOT:
                    // Initialize the left language
                    if (leftRegex == null)
                    {
                        languageLeft = emptyLanguage;
                    }
                    else
                    {
                        languageLeft = leftRegex.getLanguage(maxSteps - 1);
                    }

                    // Initialize the right language
                    if (rightRegex == null)
                    {
                        languageRight = emptyLanguage;
                    }
                    else
                    {
                        languageRight = rightRegex.getLanguage(maxSteps - 1);
                    }

                    // Combine the left & right string(s)
                    foreach(string stringLeft in languageLeft)
                    {
                        foreach(string stringRight in languageRight)
                        {
                            toReturn.Add(stringLeft + stringRight);
                        }
                    }
                    break;

                case SupportedOperators.STAR:
                case SupportedOperators.PLUS:
                    // Initialize the left language
                    if (leftRegex == null)
                    {
                        languageLeft = emptyLanguage;
                    }
                    else
                    {
                        languageLeft = leftRegex.getLanguage(maxSteps - 1);
                    }
                    addAll(toReturn, languageLeft);

                    // Loop through all the remaining steps left
                    for (int i = 0; i < maxSteps; i++)
                    {
                        // Add all possible new combinations
                        HashSet<string> languageTemp = new HashSet<string>(toReturn);
                        foreach (string s1 in languageLeft)
                        {
                            foreach (String s2 in languageTemp)
                            {
                                toReturn.Add(s1 + s2);
                            }
                        }
                    }

                    // If the operator is a star, no entry is valid too
                    if(this.currentOperator == SupportedOperators.STAR)
                    {
                        toReturn.Add("");
                    }
                    break;
            }

            return toReturn;
        }

        /// <summary>
        /// A method which generates all acceptable combinations of a language in a string
        /// </summary>
        /// <param name="maxSteps">The maximum of steps allowed</param>
        /// <returns>A string, which contains all the accepted languages</returns>
        public string getLanguageString(int maxSteps)
        {
            string toReturn = "";

            foreach(string s in getLanguage(maxSteps))
            {
                toReturn += s;
                toReturn += "\n";
            }

            return toReturn;
        }
    }

    /// <summary>
    /// A custom comparer, which allows sorted sets to be sorted, based on string length before string characters
    /// </summary>
    class SortByLength : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            // Check for potential null values
            if (x == null) return y == null ? 0 : 1;
            if (y == null) return -1;

            if(x.Length == y.Length)
            {
                return x.CompareTo(y);
            } 
            else
            {
                return x.Length - y.Length;
            }
        }
    }
}