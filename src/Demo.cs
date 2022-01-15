using formele_methoden;


namespace formele_methoden
{
    class Demo
    {

        public RegEx demoRegex()
        {
            var a = new RegEx("a");
            var b = new RegEx("b");

            // a_or_b_star: "(a|b)*"
            RegEx a_or_b_star = (a.or(b)).star();

            return a_or_b_star;
        }


        public NDFA<string> regex2ndfa(RegEx regEx)
        {
            NDFAConverter nDFAConverter = new NDFAConverter();
            regEx.printLanguage(regEx.getLanguage(4));
            NDFA<string> nDFA = RegExConverter.CreateNDFA(regEx);
            if (!Directory.Exists("TestGraphs/"))
            {
                Directory.CreateDirectory("TestGraphs/");
            }

            nDFA.generateGraph("TestGraphs/NDFA.dot");
            return nDFA;
        }


        /*
            Generates testing ndfa
        */
        public NDFA<string> demoNDFA1()
        {
            NDFA<string> nDFA = new NDFA<string>(2);

            nDFA.addTransition(new Transition<string>("q1", 'a', "q2"));
            nDFA.addTransition(new Transition<string>("q1", 'a', "q3"));
            nDFA.addTransition(new Transition<string>("q1", 'b', "q4"));

            nDFA.addTransition(new Transition<string>("q2", 'b', "q1"));
            nDFA.addTransition(new Transition<string>("q2", 'a', "q3"));
            nDFA.addTransition(new Transition<string>("q2", "q3"));

            nDFA.addTransition(new Transition<string>("q3", 'a', "q3"));
            nDFA.addTransition(new Transition<string>("q3", 'b', "q5"));
            nDFA.addTransition(new Transition<string>("q3", "q4"));

            nDFA.addTransition(new Transition<string>("q4", 'a', "q5"));

            nDFA.addTransition(new Transition<string>("q5", 'a', "q4"));

            nDFA.defineAsStartState("q1");
            nDFA.defineAsFinalState("q5");

            if (!Directory.Exists("TestGraphs/"))
            {
                Directory.CreateDirectory("TestGraphs/");
            }

            nDFA.generateGraph("TestGraphs/NDFA.dot");

            return nDFA;
        }

        /*
            Generates testing ndfa
        */
        public NDFA<string> demoNDFA2()
        {
            NDFA<string> nDFA = new NDFA<string>(2);

            nDFA.addTransition(new Transition<string>("q0", 'a', "q1"));
            nDFA.addTransition(new Transition<string>("q0", 'a', "q2"));
            nDFA.addTransition(new Transition<string>("q0", 'b', "q2"));
            nDFA.addTransition(new Transition<string>("q0", 'a', "q3"));
            nDFA.addTransition(new Transition<string>("q0", 'b', "q3"));

            nDFA.addTransition(new Transition<string>("q1", 'a', "q1"));
            nDFA.addTransition(new Transition<string>("q1", 'b', "q3"));
            nDFA.addTransition(new Transition<string>("q1", 'a', "q2"));
            nDFA.addTransition(new Transition<string>("q1", 'b', "q2"));

            nDFA.addTransition(new Transition<string>("q2", 'b', "q2"));
            nDFA.addTransition(new Transition<string>("q2", 'b', "q3"));
            nDFA.addTransition(new Transition<string>("q2", 'b', "q4"));

            nDFA.addTransition(new Transition<string>("q3", 'a', "q4"));
            nDFA.addTransition(new Transition<string>("q3", 'b', "q4"));
            nDFA.addTransition(new Transition<string>("q3", 'b', "q3"));

            nDFA.defineAsStartState("q0");
            nDFA.defineAsFinalState("q1");

            if (!Directory.Exists("TestGraphs/"))
            {
                Directory.CreateDirectory("TestGraphs/");
            }

            nDFA.generateGraph("TestGraphs/NDFA2.dot");

            return nDFA;
        }


        public DFA<string> NDFA2DFA(NDFA<String> nDFA)
        {
            NDFAConverter nDFAConverter = new NDFAConverter();
            DFA<string> dfaAutomat = nDFAConverter.createDFA(nDFA);

            if (!Directory.Exists("TestGraphs/"))
            {
                Directory.CreateDirectory("TestGraphs/");
            }
            dfaAutomat.generateGraph("TestGraphs/DFA.dot");

            return dfaAutomat;
        }

        public DFA<String> minimalize(DFA<String> dFA)
        {
            Minimalization minimalization = new Minimalization();

            DFA<String> miniDFA = minimalization.minimalize(dFA);
            miniDFA.generateGraph("TestGraphs/miniDFA.dot");

            return miniDFA;
        }



    }
}