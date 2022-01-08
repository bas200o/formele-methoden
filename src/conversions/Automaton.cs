using csdot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace formele_methoden
{
    class Automaton<T> : IComparable<Transition<T>> where T : IComparable
    {
        public HashSet<Transition<T>> transitions { get; set; }

        public SortedSet<T> states { get; set; }
        public SortedSet<T> startStates { get; set; }
        public SortedSet<T> finalStates { get; set; }
        public SortedSet<char> alphabet { get; set; }

        //Constructor Automaton

        public Automaton(int n)
        {
            this.transitions = new HashSet<Transition<T>>();
            this.states = new SortedSet<T>();
            this.startStates = new SortedSet<T>();
            this.finalStates = new SortedSet<T>();
            this.alphabet = new SortedSet<char>();
            fillAlphabet(n);
        }

        protected void fillAlphabet(int n)
        {
            string stockAlphabet = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < n; i++)
            {
                this.alphabet.Add(stockAlphabet.ElementAt(i));
            }
        }

        #region Automaton methods

        public virtual void addTransition(Transition<T> t)
        {
            transitions.Add(t);
            states.Add(t.fromState);
            states.Add(t.toState);
        }

        public virtual void defineAsStartState(T t)
        {
            // If already in states no problem because a SortedSet will remove duplicates.
            states.Add(t);
            startStates.Add(t);
        }

        public virtual void defineAsFinalState(T t)
        {
            // If already in states no problem because a HashSet will remove duplicates.
            states.Add(t);
            finalStates.Add(t);
        }
        #endregion

        public int CompareTo(Transition<T> other)
        {
            throw new NotImplementedException();
        }

        public List<T> getNextStates(List<T> states, char c)
        {
            List<T> nextStates = states;
            T lastState = nextStates.Last();

            foreach (Transition<T> transition in transitions)
            {
                if (transition.fromState.Equals(lastState) && transition.symbol.Equals(c))
                {
                    nextStates.Add(transition.toState);
                }
            }

            return nextStates;
        }
        public void printTransitions()
        {
            foreach (Transition<T> transition in this.transitions)
            {
                Console.WriteLine(transition.toString());
            }
        }

        public void generateGraph(string output)
        {
            Graph graph = new Graph("id");

            graph.strict = false;
            graph.type = "digraph";

            Edge firstEdge = new Edge();

            List<Transition> firstTs = new List<Transition>()
                    {
                        new Transition("rankdir = ", "LR;"),
                        new Transition("node_start", "[label = \"\", shape = none];")
                    };


            firstEdge.Transition = firstTs;
            graph.AddElement(firstEdge);

            foreach (var state in startStates)
            {
                Edge startEdges = new Edge();

                List<Transition> startTs = new List<Transition>()
                    {
                        new Transition("node_start -> ", state.ToString() + ";")
                    };

                startEdges.Transition = startTs;
                graph.AddElement(startEdges);
            }

            foreach (var state in finalStates)
            {
                Edge endEdges = new Edge();

                List<Transition> endTs = new List<Transition>()
                    {
                        new Transition(state.ToString(), "[label =" + state.ToString() + ", shape = doublecircle];")
                    };

                endEdges.Transition = endTs;
                graph.AddElement(endEdges);
            }

            foreach (var transition in transitions)
            {
                Edge tempEdge = new Edge();

                List<Transition> ts = new List<Transition>()
                    {
                        new Transition(transition.fromState.ToString(), "-> " + transition.toState.ToString() + " [\"label\"=\"" + transition.symbol + "\"];")
                    };

                tempEdge.Transition = ts;
                graph.AddElement(tempEdge);
            }

            DotDocument doc = new DotDocument();
            doc.SaveToFile(graph, output);
            Console.WriteLine("Writen file to: " + Directory.GetCurrentDirectory() + "/" + output);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "/usr/bin/dot";
                string fName = output.Substring(0, output.IndexOf(".dot"));
                startInfo.Arguments = "-Tpng " + output + " -o" + fName + ".png";

                Process.Start(startInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("C:/Program Files/Graphviz/bin/dot.exe");
                string fName = output.Substring(0, output.IndexOf(".dot"));
                startInfo.Arguments = "-Tpng " + output + " -o" + fName + ".png";

                Process.Start(startInfo);
            }
            else
            {
                Console.WriteLine("f voor mac");
                System.Environment.Exit(42);
            }



        }

        internal void addTransition<T>(Transition<T> transition) where T : IComparable
        {
            throw new NotImplementedException();
        }
    }
}
