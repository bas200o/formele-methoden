using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formele_methoden
{
    class RegExConverter
    {
        public static NDFA<string> CreateNDFA(RegEx reg)
        {
            var automaton = new NDFA<string>(reg.alphabet.Count);
            string leftState = "q0", rightState = "q1";
            int stateCounter = 1;

            automaton.defineAsStartState(leftState);
            automaton.defineAsFinalState(rightState);

            ModifyAutomaton(reg, ref automaton, ref stateCounter, leftState, rightState);

            return automaton;
        }

        private static void ModifyAutomaton(RegEx reg, ref NDFA<string> a, ref int c, string leftState, string rightState)
        {
            switch (reg.operate)
            {
                case RegEx.Operator.PLUS:
                    Rule5(reg, ref a, ref c, leftState, rightState);
                    break;
                case RegEx.Operator.STAR:
                    Rule6(reg, ref a, ref c, leftState, rightState);
                    break;
                case RegEx.Operator.OR:
                    Rule4(reg, ref a, ref c, leftState, rightState);
                    break;
                case RegEx.Operator.DOT:
                    Rule3(reg, ref a, ref c, leftState, rightState);
                    break;
                case RegEx.Operator.ONE:
                    Rule1_2(reg, ref a, ref c, leftState, rightState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void Rule1_2(RegEx reg, ref NDFA<string> a, ref int c, string leftState, string rightState)
        {
            var j = 1;
            for (int i = 0; i < reg.terminals.Length; i++)
            {
                var symbol = reg.terminals.ElementAt(i);

                if (i == (reg.terminals.Length - 1))
                {
                    a.alphabet.Add(symbol);
                    a.addTransition(new Transition<string>(leftState, symbol, rightState));
                } else
                {
                    var newRightState = "q" + (Int32.Parse(leftState.Split('q')[1]) + 1 + j);
                    j = 0;
                    c += 1;
                    a.alphabet.Add(symbol);
                    a.addTransition(new Transition<string>(leftState, symbol, newRightState));
                    leftState = newRightState;
                    
                }
            }
        }

        public static void Rule3(RegEx reg, ref NDFA<string> a, ref int c, string leftState, string rightState)
        {
            var newState = "q" + (c + 1).ToString();
            c++;
            ModifyAutomaton(reg.left, ref a, ref c, leftState, newState);
            ModifyAutomaton(reg.right, ref a, ref c, newState, rightState);
        }

        public static void Rule4(RegEx reg, ref NDFA<string> a, ref int c, string leftState, string rightState)
        {
            var newLeftState = "q" + (c + 1).ToString();
            var newRightState = "q" + (c + 2).ToString();
            c+=2;
            a.addTransition(new Transition<string>(leftState, Transition<String>.EPSILON, newLeftState));
            a.addTransition(new Transition<string>(newRightState, Transition<String>.EPSILON, rightState));
            ModifyAutomaton(reg.left, ref a, ref c, newLeftState, newRightState);

            newLeftState = "q" + (c + 1).ToString();
            newRightState = "q" + (c + 2).ToString();
            c+=2;
            a.addTransition(new Transition<string>(leftState, Transition<String>.EPSILON, newLeftState));
            a.addTransition(new Transition<string>(newRightState, Transition<String>.EPSILON, rightState));
            ModifyAutomaton(reg.right, ref a, ref c, newLeftState, newRightState);
        }

        public static void Rule5(RegEx reg, ref NDFA<string> a, ref int c, string leftState, string rightState)
        {
            var newLeftState = "q" + (c + 1).ToString();
            var newRightState = "q" + (c + 2).ToString();
            c += 2;

            a.addTransition(new Transition<string>(leftState, Transition<int>.EPSILON, newLeftState));
            a.addTransition(new Transition<string>(newRightState, Transition<int>.EPSILON, rightState));
            a.addTransition(new Transition<string>(newRightState, Transition<int>.EPSILON, newLeftState));
            ModifyAutomaton(reg.left, ref a, ref c, newLeftState, newRightState);
        }

        public static void Rule6(RegEx reg, ref NDFA<string> a, ref int c, string leftState, string rightState)
        {
            var newLeftState = "q" + (c + 1).ToString();
            var newRightState = "q" + (c + 2).ToString();
            c+=2;

            a.addTransition(new Transition<string>(leftState, Transition<string>.EPSILON, rightState));
            a.addTransition(new Transition<string>(leftState, Transition<string>.EPSILON, newLeftState));
            a.addTransition(new Transition<string>(newRightState, Transition<string>.EPSILON, rightState));
            a.addTransition(new Transition<string>(newRightState, Transition<string>.EPSILON, newLeftState));
            ModifyAutomaton(reg.left, ref a, ref c, newLeftState, newRightState);
        }
    }
}

