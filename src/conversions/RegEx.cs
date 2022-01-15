using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formele_methoden
{
    class RegEx
    {
        public Operator operate { get; set; }
        public String terminals { get; set; }
        public SortedSet<char> alphabet { get; set; }
        public enum Operator { PLUS, STAR, OR, DOT, ONE }

        public RegEx left;
        public RegEx right;

        public RegEx()
        {
            operate = Operator.ONE;
            terminals = "";
            this.alphabet = new SortedSet<char>();
            left = null;
            right = null;
        }
        public RegEx(String p)
        {
            operate = Operator.ONE;
            terminals = p;
            this.alphabet = new SortedSet<char>();
            foreach (char c in p)
            {
                this.alphabet.Add(c);
            }
            left = null;
            right = null;
        }


        public RegEx plus()
        {
            RegEx result = new RegEx();
            result.alphabet = alphabet;
            result.operate = Operator.PLUS;
            result.left = this;
            return result;
        }

        public RegEx star()
        {
            RegEx result = new RegEx();
            result.alphabet = alphabet;
            result.operate = Operator.STAR;
            result.left = this;
            return result;
        }

        public RegEx or(RegEx e2)
        {
            RegEx result = new RegEx();
            result.alphabet = alphabet;
            foreach (char c in e2.alphabet)
            {
                result.alphabet.Add(c);
            }
            result.operate = Operator.OR;
            result.left = this;
            result.right = e2;
            return result;
        }

        public RegEx dot(RegEx e2)
        {
            RegEx result = new RegEx();
            result.alphabet = alphabet;
            foreach (char c in e2.alphabet)
            {
                result.alphabet.Add(c);
            }
            result.operate = Operator.DOT;
            result.left = this;
            result.right = e2;
            return result;
        }

        public SortedSet<String> getLanguage(int maxSteps)
        {
            SortedSet<String> emptyLanguage = new SortedSet<String>();
            SortedSet<String> languageResult = new SortedSet<String>();

            SortedSet<String> languageLeft, languageRight;

            if (maxSteps < 1) return emptyLanguage;

            switch (this.operate)
            {
                case Operator.ONE:
                    {
                        languageResult.Add(terminals);
                        break;
                    }
                case Operator.OR:
                    {

                        languageLeft = left == null ? emptyLanguage : left.getLanguage(maxSteps - 1);
                        languageRight = right == null ? emptyLanguage : right.getLanguage(maxSteps - 1);
                        foreach (String language in languageLeft)
                        {
                            languageResult.Add(language);
                        }
                        foreach (String language in languageRight)
                        {
                            languageResult.Add(language);
                        }
                        break;
                    }
                case Operator.DOT:
                    {
                        languageLeft = left == null ? emptyLanguage : left.getLanguage(maxSteps - 1);
                        languageRight = right == null ? emptyLanguage : right.getLanguage(maxSteps - 1);
                        foreach (String s in languageLeft)
                            foreach (String t in languageRight)
                            {
                                languageResult.Add(s + t);
                            }
                        break;
                    }
                case Operator.STAR:
                case Operator.PLUS:
                    languageLeft = left == null ? emptyLanguage : left.getLanguage(maxSteps - 1);
                    foreach (String language in languageLeft)
                    {
                        languageResult.Add(language);
                    }
                    foreach (String language in languageLeft)
                    {
                        languageResult.Add(language);
                    }
                    for (int i = 1; i < maxSteps; i++)
                    {
                        HashSet<String> languageTemp = new HashSet<String>(languageResult);
                        foreach (String s in languageLeft)
                            foreach (String t in languageTemp)
                            {
                                languageResult.Add(s + t);
                            }
                    }
                    if (this.operate == Operator.STAR) languageResult.Add("");
                    break;

                default:
                    //Console.WriteLine("getLanguage is not defined for the operator: " + this.operate);
                    break;

            }
            return languageResult;
        }

        public void printLanguage(SortedSet<String> language)
        {
            foreach (string item in language)
            {
                //Console.WriteLine(item);
            }
        }

        public override string ToString()
        {
            string leftS = "", rightS = "", regS = "";
            if (left != null) leftS = left.ToString();
            if (right != null) rightS = right.ToString();

            switch (operate)
            {
                case Operator.PLUS:
                    regS = $"{leftS}+";
                    break;
                case Operator.STAR:
                    regS = $"{leftS}*";
                    break;
                case Operator.OR:
                    regS = $"({leftS}|{rightS})";
                    break;
                case Operator.DOT:
                    regS = $"({leftS}.{rightS})";
                    break;
                case Operator.ONE:
                    regS = terminals;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return regS;
        }
    }
}
