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

        private string getCombinationOfList(List<string> givenList)
        {
            string combination = "";

            // Append the strings to eachother
            foreach (string option in givenList)
            {
                combination = combination + option;
            }

            // Remove 'fuik' from the final string if merging
            combination = combination.Replace("Fuik", "");

            // Create a string array, containing only the numbers
            var stringArr = combination.Split('q');
            Array.Sort(stringArr);

            // Append the sorted array
            String v = "";
            foreach (string s in stringArr)
            {
                v = v + "q" + s;
            }

            // Remove the first q, due to it being double
            v = v.Remove(0, 1);

            // If the string is empty, the transition does not exist
            if (v.Equals(""))
            {
                v = "Fuik";
            }

            return v;
        }

        private List<string> getListFromCombination(string givenString)
        {
            List<string> toReturn = new List<string>();

            // Create a string array, containing only the numbers
            var stringArr = givenString.Split('q');
            Array.Sort(stringArr);

            foreach (string s in stringArr)
            {
                toReturn.Add("q" + s);
            }

            toReturn.RemoveAt(0);
          
            return toReturn;
        }

        private void createDfa(Ndfa givenNdfa)
        {
            Dictionary<string, string[]> dictDfa = new Dictionary<string, string[]>();

            // Add the start node(s) to the dfa dictionary to begin the table
            List<string> startNodes = givenNdfa.getStartStates();
            if(startNodes.Count == 1)
            {
                string[] emptyArr = { "N/A", "N/A" };
                dictDfa.Add(startNodes[0], emptyArr);
            } 
            else
            {
                // TODO: Implement multiple start states
                Console.WriteLine("This feature has not been implemented yet!");
            }

            bool continueFirstLoop = true;
            while(continueFirstLoop)
            {
                continueFirstLoop = false;

                foreach (string node in dictDfa.Keys.ToList())
                {
                    // Check whether a transition has not been initialized fully yet
                    if(dictDfa[node][0].Equals("N/A"))
                    {
                        // Prevent the loop from breaking, as new values could be added
                        continueFirstLoop = true;

                        // Get a list containing all the node-strings of the current node-collection
                        List<string> stringsToCheck = getListFromCombination(node);

                        List<string> optionsA = new List<string>();
                        List<string> optionsB = new List<string>();

                        // Check for each node in the collection which transitions they have, and add it to their list
                        foreach(string s in stringsToCheck)
                        {
                            string optionACleaned = getCombinationOfList(givenNdfa.getConnections(s, "a"));
                            string optionBCleaned = getCombinationOfList(givenNdfa.getConnections(s, "b"));

                            optionsA.Add(optionACleaned);
                            optionsB.Add(optionBCleaned);
                        }

                        // Append all the sorted node-strings
                        string optionsAMerged = getCombinationOfList(optionsA);
                        string optionsBMerged = getCombinationOfList(optionsB);

                        // Set the value of the note to the possible nodes-collection
                        dictDfa[node][0] = optionsAMerged;
                        dictDfa[node][1] = optionsBMerged;

                        // Add new node-collections to the dictionary
                        string[] emptyArray = { "N/A", "N/A" };
                        if(!dictDfa.ContainsKey(optionsAMerged))
                        {
                            dictDfa.Add(optionsAMerged, (string[])emptyArray.Clone());
                        }

                        if (!dictDfa.ContainsKey(optionsBMerged))
                        {
                            dictDfa.Add(optionsBMerged, (string[])emptyArray.Clone());
                        }
                    }
                }
            }

            foreach (string key in dictDfa.Keys)
            {
                this.transitions.Add(new CustomTransition(key, dictDfa[key][0], "a"));
                this.transitions.Add(new CustomTransition(key, dictDfa[key][1], "b"));

                Console.WriteLine("Node: " + key + " a: " + dictDfa[key][0] + " b: " + dictDfa[key][1]);
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