using formele_methoden;


namespace formele_methoden
{
    class Demo
    {

        /*
            Tests the ndfa converter
            @return returns a NDFA object that can be used to convert to a DFA
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