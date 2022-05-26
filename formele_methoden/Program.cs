using formele_methoden;
using System;

namespace Formele_Methoden
{
    class Program
    {
        static void Main(string[] args)
        {
            regexDemo();
        }

        static private void regexDemo()
        {
            Regex a = new Regex("a");
            Regex b = new Regex("b");

            Regex all = (a.or(b)).star();

            Regex aab = new Regex("aab");
            Regex aa = new Regex("aa");
            
            Regex expression1 = aab.or(aa); // (aab|aa)
            Regex expression2 = expression1.plus(); // (aab|aa)+
            Regex expression3 = expression2.dot(all); // (aab|aa)+(a|b)*

            Console.WriteLine("All accepted combinations of (a|b)*\n" + all.getLanguageString(5) + "\n\n");
            Console.WriteLine("All non accepted combinations of (a|b)*\n" + all.getFaultyLanguageString(5) + "\n\n");

            Console.WriteLine("All accepted combinations of (aab|aa)\n" + expression1.getLanguageString(5) + "\n\n");
            Console.WriteLine("All non accepted combinations of (aab|aa)\n" + expression1.getFaultyLanguageString(5) + "\n\n");

            Console.WriteLine("All accepted combinations of (aab|aa)+\n" + expression2.getLanguageString(5) + "\n\n");
            Console.WriteLine("All non accepted combinations of (aab|aa)+\n" + expression2.getFaultyLanguageString(5) + "\n\n");

            Console.WriteLine("All accepted combinations of (aab|aa)+(a|b)*\n" + expression3.getLanguageString(5) + "\n\n");
            Console.WriteLine("All non accepted combinations of (aab|aa)+(a|b)*\n" + expression3.getFaultyLanguageString(5) + "\n\n");
        }
    }
}