using System;
using System.Collections.Generic;
using System.Linq;

namespace formele_methoden
{
    class DFA<T> : Automaton<T> where T : IComparable
    {
        public HashSet<Transition<T>> transitions;

        public SortedSet<T> states;
        public SortedSet<T> startStates;
        public SortedSet<T> finalStates;
        public SortedSet<char> alphabet;

        public DFA(int n) : base(n)
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
            states.Add(t);
            if (startStates.Count == 0)
            {
                startStates.Add(t);
            }
            else
            {
                Console.WriteLine("Cannot define [" + t.ToString() + "] as startState, as a starting state is already defined.");
            }
        }

        public override void defineAsFinalState(T t)
        {
            base.defineAsFinalState(t);
        }

        public bool IsDFA()
        {
            bool isDFA = true;

            foreach (T from in states)
                foreach (char symbol in alphabet)
                {
                    isDFA = isDFA && GetToStates(from, symbol).Count == 1;
                    if (!isDFA)
                        return false;
                }

            return true;
        }

        public List<T> GetToStates(T from, char symbol)
        {
            List<T> toStates = new List<T>();

            foreach (Transition<T> t in transitions)
                if (t.fromState.Equals(from) && t.symbol.Equals(symbol))
                    toStates.Add(t.toState);

            return toStates;
        }

        //Checks if input string is accepted in DFA
        public bool accept(String s)
        {
            Console.WriteLine("Next string going though the accept() method for DFA: " + s);

            //checks if input string contains values from alphabet
            foreach (char c in s)
            {
                if (!alphabet.Contains(c)) return false;
            }

            // Creates a list of states starting with the startState
            List<T> iterationList = new List<T>();
            iterationList.Add(startStates.First());

            for (int i = 0; i < s.Length; i++)
            {
                iterationList = getNextStates(iterationList, s[i]);
            }

            if (finalStates.Contains(iterationList.Last()))
            {
                return true;
            }
            return false;
        }

        public new SortedSet<T> getBetweenStates()
        {
            SortedSet<T> between = new SortedSet<T>();

            foreach(T t in this.states)
            {
                if ( ! (this.startStates.Contains(t) || this.finalStates.Contains(t)) )
                {
                    between.Add(t);
                }
            }

            return between;
        }

        public new List<T> getNextStates(List<T> states, char c)
        {
            return base.getNextStates(states, c);
        }

        public string getNextState(string state, char c)
        {
            string nextState = "";

            foreach (Transition<T> transition in transitions)
            {
                if (transition.fromState.Equals(state) && transition.symbol.Equals(c))
                {
                    nextState = transition.toState.ToString();
                }
            }
            return nextState;
        } 

        public new void printTransitions()
        {
            base.printTransitions();
        }

        public void generateGraph(string output)
        {
            base.generateGraph(output);
        }
    }
}
