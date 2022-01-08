using System;


namespace formele_methoden
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo demo = new Demo();

            RegEx regEx = demo.demoRegex();

            NDFA<String> nDFA1 = demo.regex2ndfa(regEx);

            DFA<string> dFA = demo.NDFA2DFA(nDFA1);
            DFA<String> miniDFA = demo.minimalize(dFA);
        }
    }

}

