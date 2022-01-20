using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using csdot;
using csdot.Attributes.DataTypes;

namespace formele_methoden
{
    public class Ndfa
    {
        private List<CustomTransition> transitions;
        private List<string> startStates;
        private List<string> endStates;

        public Ndfa()
        {
            transitions = new List<CustomTransition>();
            startStates = new List<string>();
            endStates = new List<string>();
        }

        public List<string> getConnectionsEpsilon(string state, string symbol)
        {
            HashSet<string> nextStates = new HashSet<string>();

            // Loop through all the transitions of the ndfa
            foreach(var transition in transitions)
            {
                // Check whether the transition came from the state which was specified
                if(transition.getOrigin() == state)
                {
                    // Check whether the transition symbol was an epsilon
                    if(transition.getSymbol() == "ε")
                    {
                        // Check whether the given symbol was an epsilon
                        if (symbol == "ε")
                        {
                            nextStates.Add(transition.getDestination());

                            // Add to the nextStates recursively
                            nextStates.UnionWith(getConnectionsEpsilon(transition.getDestination(), "ε"));
                        }
                        else
                        {
                            nextStates.UnionWith(getConnectionsEpsilon(transition.getDestination(), "ε"));
                        }
                    } 
                    // Check whether the synbol matches the given symbol
                    else if (symbol == transition.getSymbol())
                    {
                        nextStates.Add(transition.getDestination());

                        // Add to the nextStates recursively
                        nextStates.UnionWith(getConnectionsEpsilon(transition.getDestination(), "ε"));
                    }
                }
            }
            List<string> toReturn = nextStates.ToList(); 
            return toReturn;
        }

        public List<CustomTransition> getTransitions()
        {
            return this.transitions;
        }

        public List<string> getStartStates()
        {
            return this.startStates;
        }

        public List<string> getEndStates()
        {
            return this.endStates;
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
            foreach(string start in startStates)
            {
                Edge startEdge = new Edge();
                List<Transition> startTs = new List<Transition>()
                    {
                        new Transition("node_start -> ", start + ";")
                    };

                startEdge.Transition = startTs; 
                graph.AddElement(startEdge);
            }

            // Mark the end states with a special shape (double circle)
            foreach (string end in endStates)
            {
                Edge endEdge = new Edge();

                List<Transition> endTs = new List<Transition>()
                    {
                        new Transition(end, "[label =" + end + ", shape = doublecircle];")
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
