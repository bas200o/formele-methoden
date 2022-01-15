using csdot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formele_methoden
{
    public class Dfa
    {
        private List<CustomTransition> transitions;
        private List<string> startStates;
        private List<string> endStates;

        public Dfa(Ndfa givenNdfa)
        {
            transitions = new List<CustomTransition>();
            startStates = new List<string>();
            endStates = new List<string>();

            createDfa(givenNdfa);
        }

        private void createDfa(Ndfa givenNdfa)
        {
            Dictionary<string, string[]> dictOriginal = new Dictionary<string, string[]>();
            Dictionary<string, string[]> dictDfa = new Dictionary<string, string[]> ();


            // Add all the original states to a dictionary
            foreach(CustomTransition transition in givenNdfa.getTransitions())
            {
                if(!dictOriginal.ContainsKey(transition.getDestination()))
                {
                    string[] emptyArr = {"N/A", "N/A"};
                    dictOriginal.Add(transition.getDestination(), emptyArr);
                }
            }

            // Check all the combinations for the original states
            bool continueFirstLoop = true;
            while (continueFirstLoop)
            {
                continueFirstLoop = false;

                foreach (string key in dictOriginal.Keys)
                {
                    // Prevent the loop from closing if there still exists a N/A value in the dictionary
                    if (dictOriginal[key][0].Equals("N/A"))
                    {
                        //continueFirstLoop = true;

                        List<string> optionsA = givenNdfa.getConnections(key, "a");
                        string combination = "";

                        foreach (string option in optionsA)
                        {
                            combination = combination + option;
                        }
                        Console.WriteLine(combination);

                        //var stringArr = combination.Split('q');
                        //Array.Sort(stringArr);
                        //String v = "";
                        //foreach (string s in stringArr)
                        //{
                        //    v = v + "q" + s;
                        //}
                        //v = v.Remove(0, 1);
                        //strings.Add(v);
                        //Console.WriteLine(v);

                        //optionsASorted = optionsASorted.Distinct().ToList();

                        //foreach(string option in optionsASorted)
                        //{
                        //    Console.WriteLine(option);
                        //}

                        //List<string> optionsB = givenNdfa.getConnections(key, "b");

                        //Console.WriteLine("a: " + optionsA.ToString());
                        //Console.WriteLine("b: " + optionsB.ToString());
                    }
                }
            }


            foreach (string key in dictOriginal.Keys)
            {
                //Console.WriteLine("Key: " + key + " a: " + dictOriginal[key][0] + " b: " + dictOriginal[key][1]);
            }
        }

        public void addTransition(CustomTransition t)
        {
            transitions.Add(t);
        }

        public void markStartState(string state)
        {
            startStates.Add(state);
        }

        public void markEndState(string state)
        {
            endStates.Add(state);
        }

        public void drawGraph(string name, string path)
        {
            DotDocument dotDocument = new DotDocument();
            Graph graph = new Graph(name);
            graph.type = "digraph";

            // Initialize the default values within a dot file
            Edge initialEdge = new Edge();
            List<Transition> firstTs = new List<Transition>()
                    {
                        new Transition("rankdir = ", "LR;"),
                        new Transition("node_start", "[label = \"\", shape = none];")
                    };
            initialEdge.Transition = firstTs;
            graph.AddElement(initialEdge);

            // Initialize the connections to all the start states
            foreach (string state in startStates)
            {
                Edge startEdge = new Edge();
                List<Transition> startTs = new List<Transition>()
                    {
                        new Transition("node_start -> ", state + ";")
                    };
            }

            // Mark the end states with a special shape (double circle)
            foreach (string state in endStates)
            {
                Edge endEdge = new Edge();

                List<Transition> endTs = new List<Transition>()
                    {
                        new Transition(state.ToString(), "[label =" + state + ", shape = doublecircle];")
                    };

                endEdge.Transition = endTs;
                graph.AddElement(endEdge);
            }

            // Plot all the transitions between states
            foreach (CustomTransition trans in transitions)
            {
                Edge edge = new Edge();
                List<Transition> toAdd = new List<Transition>();

                toAdd.Add(new Transition(trans.getTransition(), trans.getLabel()));
                edge.Transition = toAdd;

                graph.AddElement(edge);
            }

            // Save the document to the root/graphs folder
            dotDocument.SaveToFile(graph, path);
            //Console.WriteLine("Graph has been drawn");
        }

        public void printTransitions()
        {
            foreach (CustomTransition t in transitions)
            {
                Console.WriteLine(t);
            }
        }
    }
}