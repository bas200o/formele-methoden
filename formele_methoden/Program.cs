using formele_methoden;
using System;

namespace Formele_Methoden
{
    class Program
    {
        static void Main(string[] args)
        {
            regexDemo();
            testDfaWordAccepted();
            testDfaStartsEndsContains();
            demoThompson();
            dfaMinimalizeExampleOne();
            dfaReverseExampleOne();
        }

        static private void demoThompson()
        {
            Thompson thompson = new Thompson("(a|b)*");
            Ndfa newNdfa = thompson.generateThompsonNdfa();

            newNdfa.drawGraph("graph_2", "../../../graphs/thompson_demo.dot");
        }

        static private void testDfaStartsEndsContains()
        {
            Dfa testDfaStartsWith = new Dfa();
            testDfaStartsWith.startsWith("aab");
            testDfaStartsWith.drawGraph("graph_2", "../../../graphs/starts_with.dot");

            Dfa testDfaEndsWith = new Dfa();
            testDfaEndsWith.endsWith("aab");
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

        /// <summary>
        /// A function which can be used to demonstrate the minimalization functionality within this application
        /// </summary>
        private static void dfaMinimalizeExampleOne()
        {
            /*
                Due to the non-implemented renaming system within the DFA, the minimizing of the DFA can not be done in one go
                When done in a single line, the dfa will struggle with proper state transitions, because of those not having been renamed to simpler names after the first toDfa(reverse(dfa))
                
                We will show a written example of this during the assessment, as we can specifically point out where it would go wrong.
                To display that the minimazation does work, however, we will demonstrate the steps seperately.
                This means that this specific demo works in the following way:
                    1. Initialize the DFA which should be minimalized (this DFA will be written to the following file: original_dfa.dot)
                    2. Reverse the DFA, creating a NDFA (this NDFA will be written to the following file: first_reversed_ndfa.dot)
                    3. Create a new DFA, based on the above-mentioned NDFA (this DFA will be written to the following file: first_reversed_dfa.dot)
                
                    4. Re-create the DFA result from the program programmatically, but manually rename the states to simpler examples, to ensure that the program can handle the state transitions properly.
                        The DFA, which we will re-create using code, is exactly the same as the DFA result, which can be found in first_reversed_dfa.dot
                    5. Reverse the new DFA, creating a NDFA (this NDFA will be written to the following file: second_reversed_ndfa.dot
                    6. Convert the latest NDFA to a DFA, which will be written to the following file: minimalized_dfa.dot
             */

            // Step 1
            Ndfa testNdfa = new Ndfa();
            testNdfa.addTransition(new CustomTransition("q0", "q0", "a"));
            testNdfa.addTransition(new CustomTransition("q0", "q1", "b"));

            testNdfa.addTransition(new CustomTransition("q1", "q2", "a"));
            testNdfa.addTransition(new CustomTransition("q1", "q1", "b"));

            testNdfa.addTransition(new CustomTransition("q2", "q0", "a"));
            testNdfa.addTransition(new CustomTransition("q2", "q3", "b"));

            testNdfa.addTransition(new CustomTransition("q3", "q4", "a"));
            testNdfa.addTransition(new CustomTransition("q3", "q1", "b"));

            testNdfa.addTransition(new CustomTransition("q4", "q5", "a"));
            testNdfa.addTransition(new CustomTransition("q4", "q3", "b"));

            testNdfa.addTransition(new CustomTransition("q5", "q0", "a"));
            testNdfa.addTransition(new CustomTransition("q5", "q3", "b"));

            testNdfa.markStartState("q0");
            testNdfa.markEndState("q4");
            testNdfa.markEndState("q2");

            testNdfa.drawGraph("ndfa", "../../../graphs/debuggraph.dot");

            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("dfa", "../../../graphs/original_dfa.dot");

            // Step 2
            Ndfa rerversedNdfaOne = testDfa.getReverse();
            rerversedNdfaOne.drawGraph("reversed_ndfa_1", "../../../graphs/first_reversed_ndfa.dot");

            // Step 3
            Dfa reversedDfaOne = new Dfa(rerversedNdfaOne);
            reversedDfaOne.drawGraph("reversed_dfa_1", "../../../graphs/first_reversed_dfa.dot");

            // Step 4
            Ndfa newTestNdfa = new Ndfa();
            newTestNdfa.addTransition(new CustomTransition("q0", "q1", "a"));

            newTestNdfa.addTransition(new CustomTransition("q1", "q2", "b"));

            newTestNdfa.addTransition(new CustomTransition("q2", "q2", "a"));
            newTestNdfa.addTransition(new CustomTransition("q2", "q2", "b"));

            newTestNdfa.markStartState("q0");
            newTestNdfa.markEndState("q2");

            Dfa newTestDfa = new Dfa(newTestNdfa);

            // Step 5
            Ndfa reversedNdfaTwo = newTestDfa.getReverse();
            reversedNdfaTwo.drawGraph("reversed_ndfa_2", "../../../graphs/second_reversed_ndfa.dot");

            // Step 6
            Dfa dfaMinimalized = new Dfa(reversedNdfaTwo);
            dfaMinimalized.drawGraph("reversed_ndfa_2", "../../../graphs/minimalized_dfa.dot");
        }

        /// <summary>
        /// A function which can be used to demonstrate the reverse functionality within this application
        /// </summary>
        private static void dfaReverseExampleOne()
        {
            Ndfa testNdfa = new Ndfa();
            testNdfa.addTransition(new CustomTransition("q0", "q1", "a"));
            testNdfa.addTransition(new CustomTransition("q0", "q2", "b"));

            testNdfa.addTransition(new CustomTransition("q1", "q1", "a"));
            testNdfa.addTransition(new CustomTransition("q1", "q1", "b"));

            testNdfa.addTransition(new CustomTransition("q2", "q2", "a"));
            testNdfa.addTransition(new CustomTransition("q2", "q2", "b"));

            testNdfa.markStartState("q0");
            testNdfa.markEndState("q1");

            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/debuggraph.dot");

            Ndfa reversedNdfa = testDfa.getReverse();
            reversedNdfa.drawGraph("graph_3", "../../../graphs/dfa_reversed_one.dot");
        }

    }
}