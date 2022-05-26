using formele_methoden;
using System;

namespace Formele_Methoden
{
    class Program
    {
        static void Main(string[] args)
        {
            //regexDemo();
            //testDfaWordAccepted();
            testDfaStartsEndsContains();
        }

        static private void testDfaStartsEndsContains()
        {
            Dfa testDfaStartsWith = new Dfa();
            testDfaStartsWith.startsWith("aab");
            testDfaStartsWith.drawGraph("graph_2", "../../../graphs/starts_with.dot");

            Dfa testDfaEndsWith = new Dfa();
            testDfaEndsWith.endsWith("aabaaaab");
            testDfaEndsWith.drawGraph("graph_2", "../../../graphs/ends_with.dot");

            Dfa testDfaShouldContain = new Dfa();
            testDfaShouldContain.shouldContain("aab");
            testDfaShouldContain.drawGraph("graph_2", "../../../graphs/should_contain.dot");
        }

        static private void testDfaWordAccepted()
        {
            Ndfa testNdfa = new Ndfa();
            testNdfa.addTransition(new CustomTransition("q1", "q4", "a"));
            testNdfa.addTransition(new CustomTransition("q1", "q2", "b"));

            testNdfa.addTransition(new CustomTransition("q2", "q4", "a"));
            testNdfa.addTransition(new CustomTransition("q2", "q1", "b"));
            testNdfa.addTransition(new CustomTransition("q2", "q3", "b"));
            testNdfa.addTransition(new CustomTransition("q2", "q3", "ε"));

            testNdfa.addTransition(new CustomTransition("q3", "q5", "a"));
            testNdfa.addTransition(new CustomTransition("q3", "q5", "b"));

            testNdfa.addTransition(new CustomTransition("q4", "q2", "ε"));
            testNdfa.addTransition(new CustomTransition("q4", "q3", "a"));

            testNdfa.addTransition(new CustomTransition("q5", "q4", "a"));
            testNdfa.addTransition(new CustomTransition("q5", "q1", "b"));

            testNdfa.markStartState("q1");
            testNdfa.markEndState("q4");

            //testNdfa.printTransitions();
            testNdfa.drawGraph("graph_1", "../../../graphs/test_ndfa_word_accepted.dot");

            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/ndfa_2_dfa_one.dot");

            Console.WriteLine("ab is accepted: " + testDfa.checkWordAccepted("ab")); // Expected: False
            Console.WriteLine("bb is accepted: " + testDfa.checkWordAccepted("bb")); // Expected: False
            Console.WriteLine("bbab is accepted: " + testDfa.checkWordAccepted("bbab")); // Expected: False
            Console.WriteLine("b is accepted: " + testDfa.checkWordAccepted("b")); // Expected: False

            Console.WriteLine("bba is accepted: " + testDfa.checkWordAccepted("bba")); // Expected: True
            Console.WriteLine("abba is accepted: " + testDfa.checkWordAccepted("abba")); // Expected: True
            Console.WriteLine("aba is accepted: " + testDfa.checkWordAccepted("aba")); // Expected: True
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