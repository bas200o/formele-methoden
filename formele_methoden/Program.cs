using formele_methoden;
using System;

namespace Formele_Methoden
{
    class Program
    {
        static void Main(string[] args)
        {
            // Regex demo
            regexDemoOne();

            // Convert a NDFA to a DFA
            ndfaToDfaExampleOne();
            ndfaToDfaExampleTwo();

            // Convert a DFA to a NDFA (reverse the DFA)
            dfaReverseExampleOne();

            // Minimize a dfa -- can be done using the following: toDfa(reverse(toDfa(reverse(dfa))))
            dfaMinimalizeExampleOne();
        }

        /// <summary>
        /// A function which can be used to demonstrate the RegEx functionality within this application
        /// </summary>
        private static void regexDemoOne()
        {
            Regex r = new Regex("(a|b)+(z)*(a|b)");
            Ndfa ndfa = r.re2nfa();
            ndfa.drawGraph("regexNDFA", "../../../graphs/regex_2_ndfa.dot");
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

        /// <summary>
        /// A function which can be used to demonstrate the first ndfa to dfa functionality within this application
        /// </summary>
        private static void ndfaToDfaExampleOne()
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
            testNdfa.drawGraph("graph_1", "../../../graphs/debuggraph.dot");


            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/ndfa_2_dfa_one.dot");
        }

        /// <summary>
        /// A function which can be used to demonstrate the second ndfa to dfa functionality within this application
        /// </summary>
        private static void ndfaToDfaExampleTwo()
        {
            Ndfa testNdfa = new Ndfa();
            testNdfa.addTransition(new CustomTransition("q1", "q2", "a"));
            testNdfa.addTransition(new CustomTransition("q1", "q3", "a"));
            testNdfa.addTransition(new CustomTransition("q1", "q4", "b"));

            testNdfa.addTransition(new CustomTransition("q2", "q3", "a"));
            testNdfa.addTransition(new CustomTransition("q2", "q3", "ε"));
            testNdfa.addTransition(new CustomTransition("q2", "q1", "b"));

            testNdfa.addTransition(new CustomTransition("q3", "q3", "a"));
            testNdfa.addTransition(new CustomTransition("q3", "q4", "ε"));
            testNdfa.addTransition(new CustomTransition("q3", "q5", "b"));

            testNdfa.addTransition(new CustomTransition("q4", "q5", "a"));

            testNdfa.addTransition(new CustomTransition("q5", "q4", "a"));

            testNdfa.markStartState("q1");
            testNdfa.markEndState("q5");

            //testNdfa.printTransitions();
            testNdfa.drawGraph("graph_1", "../../../graphs/debuggraph.dot");


            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/ndfa_2_dfa_two.dot");
        }
    }
}