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
        // Lists which will be used to keep track of the current transitions, start and end states
        private List<CustomTransition> transitions;
        private List<string> startStates;
        private List<string> endStates;

        public Ndfa()
        {
            // Initialize the lists
            transitions = new List<CustomTransition>();
            startStates = new List<string>();
            endStates = new List<string>();
        }

        /// <summary>
        /// A method which can be used to determine which states are reachable from a given state, using a specific character
        /// </summary>
        /// <param name="state">The original state</param>
        /// <param name="symbol">The symbol which can be used</param>
        /// <param name="isUsed">A boolean which tracks whether the symbol has been used, specifically for the recursive part</param>
        /// <returns>A list of strings, containing nodes which are reachable</returns>
        public List<string> getNextStates(string state, string symbol, bool isUsed)
        {
            HashSet<string> nextStates = new HashSet<string>();

            // Loop through all the transitions
            foreach (CustomTransition transition in transitions)
            {
                // Check whether the current transition's origin is the given state
                if (transition.getOrigin().Equals(state))
                {
                    // Check whether the current transition's symbol is an epsilon
                    if (transition.getSymbol() == "ε")
                    {
                        if (symbol == "ε")
                        {
                            // If the originally given symbol was an epsilon, add the transition to the list, and call the method again
                            nextStates.Add(transition.getDestination());
                            nextStates.UnionWith(getNextStates(transition.getDestination(), "ε", isUsed));
                        }
                        else
                        {
                            // Call the method again
                            nextStates.UnionWith(getNextStates(transition.getDestination(), symbol, isUsed));
                        }
                    }
                    else if (symbol == transition.getSymbol())
                    {
                        // If the given symbol matches the current transition's symbol, add the transitions destination node to the list
                        nextStates.Add(transition.getDestination());

                        // Update the boolean to ensure that only epsilon values will be allowed after this point
                        isUsed = true;

                        // Call the method again
                        nextStates.UnionWith(getNextStates(transition.getDestination(), "ε", isUsed));
                    }
                }

            }

            // Convert the states to a list
            List<string> toReturn = nextStates.ToList();

            // Return the list containing the reachable states
            return toReturn;
        }

        /// <summary>
        /// A function which can be used to get the current start states
        /// </summary>
        /// <returns>A list, containing the strings of all the start states</returns>
        public List<string> getStartStates()
        {
            return this.startStates;
        }

        /// <summary>
        /// A function which can be used to get the end states
        /// </summary>
        /// <returns>A list, containing the strings of all the end states</returns>
        public List<string> getEndStates()
        {
            return this.endStates;
        }

        /// <summary>
        /// A function which can be used to add a transition to the list with transitions
        /// </summary>
        /// <param name="t">The transition which should be added</param>
        public void addTransition(CustomTransition t)
        {
            transitions.Add(t);
        }

        /// <summary>
        /// A function which can be used to mark a specific state as a start state
        /// </summary>
        /// <param name="state">The name of the node which should be marked as a start state</param>
        public void markStartState(string state)
        {
            startStates.Add(state);
        }

        /// <summary>
        /// A function which can be used to mark a specific state as an end state
        /// </summary>
        /// <param name="state">The name of the node which should be marked as an end state</param>
        public void markEndState(string state)
        {
            endStates.Add(state);
        }

        /// <summary>
        /// A function which can be used to create a graphviz file, displaying the current NDFA
        /// </summary>
        /// <param name="name">The title of the graph</param>
        /// <param name="path">The path which should be used to save the dot file</param>
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

        /// <summary>
        /// A function which can be used to print all the transitions within the current NDFA
        /// </summary>
        public void printTransitions()
        {
            foreach (CustomTransition t in transitions)
            {
                Console.WriteLine(t);
            }
        }
    }
}
