using formele_methoden;
using System;

namespace Formele_Methoden
{
    class Program
    {
        static void Main(string[] args)
        {
            ndfaDemoOne();
            //ndfaDemoTwo();
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
    }
}