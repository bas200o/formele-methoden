using csdot.Attributes.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formele_methoden
{
    class NDFA<T> : Automaton<T> where T : IComparable
    {
        public HashSet<Transition<T>> transitions;

        public SortedSet<T> states;
        public SortedSet<T> startStates;
        public SortedSet<T> finalStates;
        public SortedSet<char> alphabet;

        public NDFA(int n) : base(n)
        {
            this.transitions = base.transitions;
            this.states = base.states;
            this.startStates = base.startStates;
            this.finalStates = base.finalStates;
            this.alphabet = base.alphabet;
            fillAlphabet(n);
        }
        private void fillAlphabet(int n)
        {
            base.fillAlphabet(n);
        }

        public override void addTransition(Transition<T> t)
        {
            base.addTransition(t);
        }

        public override void defineAsStartState(T t)
        {
            base.defineAsStartState(t);
        }

        public override void defineAsFinalState(T t)
        {
            base.defineAsFinalState(t);
        }

        //Checks if input string is accepted in DFA
        public bool accept(String s)
        {
            Console.WriteLine("Next string going though the accept() method for NDFA: " + s);

            //checks if input string contains values from alphabet
            foreach (char c in s)
            {
                if (!alphabet.Contains(c)) return false;
            }

            // Creates a list of states starting with the startState
            List<T> iterationList = new List<T>();

            for (int i = 0; i < startStates.Count; i++)
            {
                iterationList.Add(startStates.ElementAt(i));

                for (int j = 0; j < s.Length; j++)
                {
                    iterationList = getNextStates(iterationList, s[j]);
                }

                if (finalStates.Contains(iterationList.Last()))
                {
                    return true;
                }
            }

            return false;
        }
        public new List<T> getNextStates(List<T> states, char c)
        {
            return base.getNextStates(states, c);
        }

        public HashSet<T> getNextStatesEpsilon(string state, char c, bool isUsed)
        {
            //Implement IsUsed

            HashSet<T> nextStates = new HashSet<T>();

            foreach (Transition<T> transition in transitions)
            {
                if (transition.fromState.Equals(state))
                {
                    /*var specificTrans1 = getTransitions(transition.toState.ToString(), c);*/
                    if (transition.symbol == Transition<T>.EPSILON)
                    {

                        if (c == Transition<T>.EPSILON)
                        {
                            nextStates.Add(transition.toState);
                            //Console.WriteLine("Test1");
                            nextStates.UnionWith(getNextStatesEpsilon(transition.toState.ToString(), Transition<T>.EPSILON, isUsed));
                        }
                        else
                        {
                            //Console.WriteLine("Test2");
                            nextStates.UnionWith(getNextStatesEpsilon(transition.toState.ToString(), c, isUsed));
                        }
                    }
                    else if (c == transition.symbol)
                    {
                        nextStates.Add(transition.toState);
                        //Console.WriteLine("Test3");
                        isUsed = true;
                        nextStates.UnionWith(getNextStatesEpsilon(transition.toState.ToString(), Transition<T>.EPSILON, isUsed));
                    }
                }
                
            }

            return nextStates;
        }

        public new void printTransitions()
        {
            base.printTransitions();
        }

        public void generateGraph(string output)
        {
            base.generateGraph(output);
        }

        public List<Transition<T>> getTransitions(string fromState, char symbol)
        {
            var allTransitions = new List<Transition<T>>();
            foreach (var trans in transitions)
            {
                if (trans.fromState.Equals(fromState) && (trans.symbol.Equals(symbol) || trans.symbol.Equals(Transition<T>.EPSILON)))
                {
                    allTransitions.Add(trans);
                }
            }
            return allTransitions;
        }

    }
}
