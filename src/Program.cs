using System;


namespace formele_methoden
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo demo = new Demo();
            NDFA<String> nDFA1 = demo.demoNDFA1();
            
            DFA<string> dFA = demo.NDFA2DFA(nDFA1);
            //DFA<String> miniDFA = demo.minimalize(dFA);

            Lesson1 l = new Lesson1();
            l.minimalize();

            
        }
    }

}

