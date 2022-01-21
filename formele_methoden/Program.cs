using formele_methoden;
using System;

namespace Formele_Methoden
{
    class Program
    {
        //TODO: Implement minimalisation -- toDfa(reverse(toDfa(reverse(dfa))))
        //TODO: Check for multiple start states
        //TODO: Clean up / work out examples and make proper demo class
        static void Main(string[] args)
        {
            
            regexDemo();
            //ndfaDemoOne();
            //ndfaDemoTwo();
            //ndfaDemoEpsilonSimple();
            ndfaDemoEpsilonTwo();

            //dfaMinimalize();
            //testing();
        }

        private static void testing()
        {
            Ndfa testNdfa = new Ndfa();
            testNdfa.addTransition(new CustomTransition("q0", "q0", "a"));
            testNdfa.addTransition(new CustomTransition("q0", "q5", "a"));
            testNdfa.addTransition(new CustomTransition("q0", "q2", "a"));

            testNdfa.addTransition(new CustomTransition("q1", "q1", "b"));
            testNdfa.addTransition(new CustomTransition("q1", "q0", "b"));
            testNdfa.addTransition(new CustomTransition("q1", "q3", "b"));

            testNdfa.addTransition(new CustomTransition("q2", "q1", "a"));

            testNdfa.addTransition(new CustomTransition("q3", "q4", "b"));
            testNdfa.addTransition(new CustomTransition("q3", "q5", "b"));
            testNdfa.addTransition(new CustomTransition("q3", "q2", "b"));

            testNdfa.addTransition(new CustomTransition("q4", "q3", "a"));

            testNdfa.addTransition(new CustomTransition("q5", "q4", "a"));

            testNdfa.addTransition(new CustomTransition("q6", "q4", "ε"));
            testNdfa.addTransition(new CustomTransition("q6", "q2", "ε"));

            testNdfa.markStartState("q6");
            testNdfa.markEndState("q0");

            testNdfa.drawGraph("ndfa", "../../../graphs/testing.dot");

            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("dfa", "../../../graphs/testing_dfa.dot");
        }

        private static void dfaMinimalize()
        {
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

            testNdfa.drawGraph("ndfa", "../../../graphs/graph_1.dot");

            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("dfa", "../../../graphs/graph_2.dot");

            Ndfa rerversedNdfaOne = testDfa.getReverse();
            rerversedNdfaOne.drawGraph("reversed_ndfa_1", "../../../graphs/graph_3.dot");

            Dfa reversedDfaOne = new Dfa(rerversedNdfaOne);
            reversedDfaOne.drawGraph("reversed_dfa_1", "../../../graphs/graph_4.dot");
        }

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

            testNdfa.drawGraph("graph_1", "../../../graphs/graph_1.dot");

            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/graph_2.dot");

            Ndfa reversedNdfa = testDfa.getReverse();
            reversedNdfa.drawGraph("graph_3", "../../../graphs/graph_3.dot");
        }

        private static void dfaReverseExampleTwo()
        {
            Ndfa testNdfa = new Ndfa();
            testNdfa.addTransition(new CustomTransition("q0", "q1", "a"));
            testNdfa.addTransition(new CustomTransition("q0", "q2", "b"));

            testNdfa.addTransition(new CustomTransition("q1", "q1", "a"));
            testNdfa.addTransition(new CustomTransition("q1", "q2", "b"));

            testNdfa.addTransition(new CustomTransition("q2", "q2", "a"));
            testNdfa.addTransition(new CustomTransition("q2", "q2", "b"));

            testNdfa.markStartState("q0");
            testNdfa.markEndState("q2");

            testNdfa.drawGraph("graph_1", "../../../graphs/graph_1.dot");

            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/graph_2.dot");

            Ndfa reversedNdfa = testDfa.getReverse();
            reversedNdfa.drawGraph("graph_3", "../../../graphs/graph_3.dot");
        }

        private static void ndfaDemoEpsilonSheetFour()
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
            testNdfa.drawGraph("graph_1", "../../../graphs/graph_1.dot");


            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/graph_2.dot");
        }
        
        private static void ndfaDemoEpsilonSimple()
        {
            Ndfa testNdfa = new Ndfa();
            testNdfa.addTransition(new CustomTransition("q0", "q1", "ε"));
            testNdfa.addTransition(new CustomTransition("q0", "q0", "a"));

            testNdfa.addTransition(new CustomTransition("q1", "q2", "b"));
            testNdfa.addTransition(new CustomTransition("q1", "q2", "ε"));

            testNdfa.markStartState("q0");
            testNdfa.markEndState("q2");

            //testNdfa.printTransitions();
            testNdfa.drawGraph("graph_1", "../../../graphs/graph_1.dot");


            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/graph_2.dot");
        }

        private static void ndfaDemoTwo()
        {
            Ndfa testNdfa = new Ndfa();
            testNdfa.addTransition(new CustomTransition("q0", "q1", "a"));
            testNdfa.addTransition(new CustomTransition("q0", "q0", "a"));

            testNdfa.addTransition(new CustomTransition("q1", "q2", "b"));

            testNdfa.markStartState("q0");
            testNdfa.markEndState("q2");

            //testNdfa.printTransitions();
            testNdfa.drawGraph("graph_1", "../../../graphs/graph_1.dot");


            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/graph_2.dot");
        }

        private static void ndfaDemoOne()
        {
            Ndfa testNdfa = new Ndfa();
            testNdfa.addTransition(new CustomTransition("q0", "q1", "a"));
            testNdfa.addTransition(new CustomTransition("q0", "q2", "a"));
            testNdfa.addTransition(new CustomTransition("q0", "q2", "b"));
            testNdfa.addTransition(new CustomTransition("q0", "q3", "a"));
            testNdfa.addTransition(new CustomTransition("q0", "q3", "b"));

            testNdfa.addTransition(new CustomTransition("q1", "q1", "a"));
            testNdfa.addTransition(new CustomTransition("q1", "q2", "a"));
            testNdfa.addTransition(new CustomTransition("q1", "q2", "b"));
            testNdfa.addTransition(new CustomTransition("q1", "q3", "b"));

            testNdfa.addTransition(new CustomTransition("q2", "q2", "b"));
            testNdfa.addTransition(new CustomTransition("q2", "q4", "b"));
            testNdfa.addTransition(new CustomTransition("q2", "q3", "b"));

            testNdfa.addTransition(new CustomTransition("q3", "q3", "b"));
            testNdfa.addTransition(new CustomTransition("q3", "q4", "a"));
            testNdfa.addTransition(new CustomTransition("q3", "q4", "b"));

            testNdfa.markStartState("q0");
            testNdfa.markEndState("q1");

            //testNdfa.printTransitions();
            testNdfa.drawGraph("graph_1", "../../../graphs/graph_1.dot");

            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/graph_2.dot");
        }

        private static void ndfaDemoEpsilonTwo()
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
            testNdfa.drawGraph("graph_1", "../../../graphs/graph_1.dot");


            Dfa testDfa = new Dfa(testNdfa);
            testDfa.drawGraph("graph_2", "../../../graphs/graph_2.dot");
        }
    }
}