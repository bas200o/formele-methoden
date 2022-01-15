using formele_methoden;
using System;
using System.Collections.Generic;

namespace formele_methoden
{
    class Lesson1
    {

        public void dfa()
        {
            DFA<string> automaton = new DFA<string>(2);

            automaton.addTransition(new Transition<string>("q0", 'a', "q1"));
            automaton.addTransition(new Transition<string>("q0", 'b'));

            automaton.addTransition(new Transition<string>("q1", 'a'));
            automaton.addTransition(new Transition<string>("q1", 'b', "q2"));

            automaton.addTransition(new Transition<string>("q2", 'a', "q1"));
            automaton.addTransition(new Transition<string>("q2", 'b', "q3"));

            automaton.addTransition(new Transition<string>("q3", 'a'));
            automaton.addTransition(new Transition<string>("q3", 'b'));

            automaton.defineAsStartState("q0");
            automaton.defineAsFinalState("q3");

            automaton.printTransitions();

            //Console.WriteLine(automaton.accept("bbbb"));

            automaton.generateGraph("Graphs/DFATest.dot");
        }

        public void ndfa()
        {
            NDFA<string> automaton = new NDFA<string>(2);

            automaton.addTransition(new Transition<string>("q1", 'a', "q2"));
            automaton.addTransition(new Transition<string>("q1", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q1", 'b', "q4"));

            automaton.addTransition(new Transition<string>("q2", 'b', "q1"));
            automaton.addTransition(new Transition<string>("q2", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q2", "q3"));

            automaton.addTransition(new Transition<string>("q3", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q3", 'b', "q5"));
            automaton.addTransition(new Transition<string>("q3", "q4"));

            automaton.addTransition(new Transition<string>("q4", 'a', "q5"));

            automaton.addTransition(new Transition<string>("q5", 'a', "q4"));
            

            automaton.defineAsStartState("q1");
            automaton.defineAsFinalState("q5");

            automaton.printTransitions();

            //Console.WriteLine(automaton.accept("bab"));

            automaton.generateGraph("Graphs/NDFATest.dot");
        }

        public void ndfaToDfa()
        {
            NDFA<string> automaton = new NDFA<string>(2);

            automaton.addTransition(new Transition<string>("q1", 'a', "q2"));
            automaton.addTransition(new Transition<string>("q1", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q1", 'b', "q4"));

            automaton.addTransition(new Transition<string>("q2", 'b', "q1"));
            automaton.addTransition(new Transition<string>("q2", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q2", "q3"));

            automaton.addTransition(new Transition<string>("q3", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q3", 'b', "q5"));
            automaton.addTransition(new Transition<string>("q3", "q4"));

            automaton.addTransition(new Transition<string>("q4", 'a', "q5"));

            automaton.addTransition(new Transition<string>("q5", 'a', "q4"));


            automaton.defineAsStartState("q1");
            automaton.defineAsStartState("q2");
            automaton.defineAsFinalState("q5");

            automaton.printTransitions();

            //Console.WriteLine(automaton.accept("bab"));

            automaton.generateGraph("Graphs/NDFAPreTest.dot");

            NDFAConverter nDFAConverter = new NDFAConverter();

            DFA<string> automaton2 = nDFAConverter.createDFA(automaton);

            automaton2.generateGraph("Graphs/DFAPostTest.dot");
        }

        public void regEx()
        {
            var a = new RegEx("a");
            var b = new RegEx("b");

            // expr1: "baa"
            var expr1 = new RegEx("baa");
            // expr2: "bb"
            var expr2 = new RegEx("bb");
            // expr3: "baa | baa"
            var expr3 = expr1.or(expr2);

            // all: "(a|b)*"
            var all = (a.or(b)).star();

            // expr4: "(baa | baa)+"
            var expr4 = expr3.plus();
            // expr5: "(baa | baa)+ (a|b)*"
            var expr5 = expr4.dot(all);

            ////Console.WriteLine("taal van (baa):\n" + expr1.languageToString(expr1.getLanguage(5)));
            ////Console.WriteLine("taal van (bb):\n" + expr2.languageToString(expr2.getLanguage(5)));
            ////Console.WriteLine("taal van (baa | bb):\n" + expr3.languageToString(expr3.getLanguage(5)));

            //Console.WriteLine("Language? van " + expr5.ToString() + ":");
            expr5.printLanguage(expr5.getLanguage(4));

            //NDFA<string> automaton = RegExConverter.CreateNDFA(all);

            //automaton.printTransitions();

            //automaton.generateGraph("Graphs/NDFAFromRegExTest.dot");

            ////Console.WriteLine("taal van (baa | bb)+:\n" + expr4.languageToString(expr4.getLanguage(5)));
            ////Console.WriteLine("taal van (baa | bb)+ (a|b)*:\n" + expr5.languageToString(expr5.getLanguage(6)));
        }

        public void reGexToDFA()
        {
            var a = new RegEx("a");
            var b = new RegEx("b");

            // expr1: "baa"
            var expr1 = new RegEx("baa");
            // expr2: "bb"
            var expr2 = new RegEx("bb");
            // expr3: "baa | bb"
            var expr3 = expr1.or(expr2);

            // all: "(a|b)*"
            var all = (a.or(b)).star();

            // expr4: "(baa | bb)+"
            var expr4 = expr3.plus();
            // expr5: "(baa | bb)+ (a|b)*"
            var expr5 = expr4.dot(all);

            NDFAConverter nDFAConverter = new NDFAConverter();

            //Console.WriteLine("Language? van " + all.ToString() + ":");
            all.printLanguage(all.getLanguage(4));

            NDFA<string> automaton = RegExConverter.CreateNDFA(all);

            automaton.generateGraph("Graphs/NDFAFromRegExTest.dot");

            DFA<string> automaton2 = nDFAConverter.createDFA(automaton);

            automaton2.generateGraph("Graphs/DFAfromNDFAtTest.dot");
        }


        public void minimalize()
        {
            DFA<string> automaton = new DFA<string>(2);

            automaton.addTransition(new Transition<string>("q1", 'a', "q2"));
            automaton.addTransition(new Transition<string>("q1", 'b', "q3"));

            automaton.addTransition(new Transition<string>("q2", 'a', "q6"));
            automaton.addTransition(new Transition<string>("q2", 'b', "q2"));

            automaton.addTransition(new Transition<string>("q3", 'a', "q4"));
            automaton.addTransition(new Transition<string>("q3", 'b', "q5"));

            automaton.addTransition(new Transition<string>("q4", 'a', "q2"));
            automaton.addTransition(new Transition<string>("q4", 'b', "q3"));

            automaton.addTransition(new Transition<string>("q5", 'a', "q8"));
            automaton.addTransition(new Transition<string>("q5", 'b', "q7"));

            automaton.addTransition(new Transition<string>("q6", 'a', "q7"));
            automaton.addTransition(new Transition<string>("q6", 'b', "q5"));

            automaton.addTransition(new Transition<string>("q7", 'a', "q5"));
            automaton.addTransition(new Transition<string>("q7", 'b', "q8"));

            automaton.addTransition(new Transition<string>("q8", 'a', "q9"));
            automaton.addTransition(new Transition<string>("q8", 'b', "q10"));

            automaton.addTransition(new Transition<string>("q9", 'a', "q9"));
            automaton.addTransition(new Transition<string>("q9", 'b', "q10"));

            automaton.addTransition(new Transition<string>("q10", 'a', "q10"));
            automaton.addTransition(new Transition<string>("q10", 'b', "q10"));

            automaton.defineAsStartState("q1");

            automaton.defineAsFinalState("q6");
            automaton.defineAsFinalState("q8");
            automaton.defineAsFinalState("q9");

            automaton.printTransitions();
            automaton.generateGraph("TestGraphs/DFApreMinimizeTest.dot");
            //Console.WriteLine("minimalization...");

            Minimalization minimalization = new Minimalization();

            var minimize = minimalization.minimalize(automaton);

            minimize.generateGraph("TestGraphs/DFAMinimizeTest.dot");
            //Console.WriteLine("minimalization complete");

            // Check if DFA accepts.
            ////Console.WriteLine(minimize.accept("ba"));
        }

        public void minimalizeReverse()
        {
            NDFA<string> automaton = new NDFA<string>(2);

            automaton.addTransition(new Transition<string>("q1", 'a', "q2"));
            automaton.addTransition(new Transition<string>("q1", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q1", 'b', "q4"));

            automaton.addTransition(new Transition<string>("q2", 'b', "q1"));
            automaton.addTransition(new Transition<string>("q2", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q2", "q3"));

            automaton.addTransition(new Transition<string>("q3", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q3", 'b', "q5"));
            automaton.addTransition(new Transition<string>("q3", "q4"));

            automaton.addTransition(new Transition<string>("q4", 'a', "q5"));

            automaton.addTransition(new Transition<string>("q5", 'a', "q4"));


            automaton.defineAsStartState("q1");
            automaton.defineAsStartState("q2");
            automaton.defineAsFinalState("q5");

            Minimalization minimalization = new Minimalization();
            NDFAConverter nDFAConverter = new NDFAConverter();


            automaton.printTransitions();
            automaton.generateGraph("Graphs/NDFApreReverseTest.dot");
            //Console.WriteLine("reversing...");

            var automaton2 = minimalization.reverseAutomaton(automaton);
            automaton2.printTransitions();
            //Console.WriteLine("reverse complete... Converting to DFA...");
            automaton2.generateGraph("Graphs/ReverseTest.dot");

            var automaton3 = nDFAConverter.createDFA(automaton2);
            automaton3.printTransitions();
            //Console.WriteLine("DFA complete");

            automaton3.generateGraph("Graphs/NormalizeTest.dot");
        }


    }
}
