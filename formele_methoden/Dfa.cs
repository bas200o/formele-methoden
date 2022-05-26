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
        // Lists which will be used to keep track of the transitions, start and end states
        private List<CustomTransition> transitions;
        private List<string> startStates;
        private List<string> endStates;

        // Lists which will be used to keep track of the start and end states of the given ndfa
        private List<string> ndfaStartStates;
        private List<string> ndfaEndStates;

        /// <summary>
        /// A function which can be used to initialize a DFA
        /// </summary>
        /// <param name="givenNdfa">The NDFA which should be converted to a DFA</param>
        public Dfa(Ndfa givenNdfa)
        {
            // Initialize the empty variables
            transitions = new List<CustomTransition>();
            startStates = new List<string>();
            endStates = new List<string>();

            // Initialize the start and end states of the NDFA
            ndfaStartStates = givenNdfa.getStartStates();
            ndfaEndStates = givenNdfa.getEndStates();

            // Convert the NDFA to a DFA
            createDfa(givenNdfa);
        }

        /// <summary>
        /// A constructor of the DFA to generate an empty DFA
        /// </summary>
        public Dfa()
        {
            // Initialize the empty variables
            transitions = new List<CustomTransition>();
            startStates = new List<string>();
            endStates = new List<string>();
            ndfaStartStates = new List<string>();
            ndfaEndStates = new List<string>();
        }

        /// <summary>
        /// A function which can be used to generate a DFA, which ends with a specific suffix
        /// </summary>
        /// <param name="givenString">The characters the DFA should end with</param>
        public void endsWith(string givenString)
        {
            Ndfa newNdfa = new Ndfa();

            // Add the core transitions
            for (int i = 0; i < givenString.Length; i++)
            {
                // Initialize the current and next state
                string currentState = "q" + (i + 1);
                string nextState = "q" + (i + 2);

                // Add the transition from current to next state
                string symbol = givenString[i].ToString();
                newNdfa.addTransition(new CustomTransition(currentState, nextState, symbol));

                // Add the transition back to the base state
                string returnSymbol = symbol == "a" ? "b" : "a";
                newNdfa.addTransition(new CustomTransition(currentState, "q1", returnSymbol));
            }

            // Connect the end back to the start state
            string endState = "q" + (givenString.Length + 1);
            newNdfa.addTransition(new CustomTransition(endState, "q1", "a"));
            newNdfa.addTransition(new CustomTransition(endState, "q1", "b"));

            // Mark the start and end states
            newNdfa.markStartState("q1");
            newNdfa.markEndState(endState);
            ndfaStartStates.Add("q1");
            ndfaEndStates.Add(endState);

            // Initialize a DFA, based on the ndfa
            createDfa(newNdfa);
        }

        /// <summary>
        /// A method which can be used to generate a DFA, which starts with a specific prefix
        /// </summary>
        /// <param name="givenString">The characters the DFA should start with</param>
        public void startsWith(string givenString)
        {
            Ndfa newNdfa = new Ndfa();

            // Add the core transitions (starts with)
            for(int i = 0; i < givenString.Length; i++)
            {
                // Initialize the current and next state
                string currentState = "q" + (i + 1);
                string nextState = "q" + (i + 2);

                // Add the transition from current to next state
                string symbol = givenString[i].ToString();
                newNdfa.addTransition(new CustomTransition(currentState, nextState, symbol));
            }

            // Loop the end state to itself
            string endState = "q" + (givenString.Length + 1);
            newNdfa.addTransition(new CustomTransition(endState, endState, "a"));
            newNdfa.addTransition(new CustomTransition(endState, endState, "b"));

            // Mark the start and end states
            newNdfa.markStartState("q1");
            newNdfa.markEndState(endState);
            ndfaStartStates.Add("q1");
            ndfaEndStates.Add(endState);

            // Initialize a DFA, based on the ndfa
            createDfa(newNdfa);
        }

        /// <summary>
        /// A function which can be used to get the reverse of a DFA
        /// </summary>
        /// <returns>A NDFA object which contains the reversed DFA</returns>
        public Ndfa getReverse()
        {
            // Initialize the variables to initialize the ndfa
            List<string> allNodes = new List<string>();
            Ndfa toReturn = new Ndfa();
            List<CustomTransition> newTransitions = new List<CustomTransition>();

            // Check the amount of nodes currently available
            foreach (CustomTransition trans in this.transitions)
            {
                // Add all the nodes to the list, tracking the nodes
                allNodes.Add(trans.getDestination());
                allNodes.Add(trans.getOrigin());
            }

            // Initialize the amount of unique nodes, which will be used to be able to name new states which will be added
            allNodes = allNodes.Distinct().ToList();
            string endNodeName = "q" + allNodes.Count;

            // Step 1. Connect all the end nodes to a singular, final state
            foreach (string endState in this.endStates)
            {
                this.transitions.Add(new CustomTransition(endState, endNodeName, "ε"));
            }

            // Clear the endstates list, and re-add the 'true' end state
            this.endStates = new List<string> { endNodeName };

            // Step 2. Swap the start and end state
            toReturn.markStartState(this.endStates[0]);
            toReturn.markEndState(this.startStates[0]);

            // Step 3. Reverse the transitions
            foreach (CustomTransition trans in this.transitions)
            {
                CustomTransition toAdd = new CustomTransition(trans.getDestination(), trans.getOrigin(), trans.getSymbol());
                newTransitions.Add(toAdd);
            }

            // Step 4. Remove unreachable nodes
            foreach (string node in allNodes)
            {
                bool toRemove = true;

                // Check whether a transition exists, which has the current node as a destination
                foreach(CustomTransition transition in newTransitions)
                {
                    if(transition.getDestination().Equals(node) && !transition.getOrigin().Equals(node))
                    {
                        toRemove = false;
                    }
                }

                List<CustomTransition> transToRemove = new List<CustomTransition>();

                if (toRemove)
                {
                    // Loop through all the transitions
                    foreach (CustomTransition transition in newTransitions)
                    {
                        bool isStartState = toReturn.getStartStates().Contains(node);

                        // Check whether the state is reachable, start states are excluded from the check
                        if (transition.getOrigin().Equals(node) && !isStartState)
                        {
                            transToRemove.Add(transition);
                        }
                    }
                }

                // Remove all the transitions, which have been marked for removal
                foreach(CustomTransition trans in transToRemove)
                {
                    newTransitions.Remove(trans);
                }
            }

            // Add all the 'proper' transitions to the final ndfa
            foreach(CustomTransition transition in newTransitions)
            {
                toReturn.addTransition(transition);
            }

            return toReturn;
        }

        /// <summary>
        /// A function which can be used to combine node strings into a new node
        /// </summary>
        /// <param name="givenList">The list of nodes</param>
        /// <returns>A sorted string containing the new combinated node</returns>
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
            stringArr = stringArr.Distinct().ToArray();
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

        /// <summary>
        /// A function which can be used to split a combined node into a list of smaller nodes
        /// </summary>
        /// <param name="givenString">The combined node string</param>
        /// <returns>A list of strings, which contains the seperate node of the combined given node</returns>
        private List<string> getListFromCombination(string givenString)
        {
            List<string> toReturn = new List<string>();

            // Create a string array, containing only the numbers
            var stringArr = givenString.Split('q');
            Array.Sort(stringArr);

            // Add the nodes to the return list
            foreach (string s in stringArr)
            {
                toReturn.Add("q" + s);
            }

            // Remove the first entry due to it being empty
            toReturn.RemoveAt(0);
          
            return toReturn;
        }

        /// <summary>
        /// A function which can be used to convert a NDFA to a DFA
        /// </summary>
        /// <param name="givenNdfa">The given NDFA</param>
        private void createDfa(Ndfa givenNdfa)
        {
            Dictionary<string, string[]> dictDfa = new Dictionary<string, string[]>();

            // Add the start node(s) to the dfa dictionary to begin the table
            List<string> startNodes = givenNdfa.getStartStates();
            string combination = getCombinationOfList(startNodes);
            string[] emptyArr = { "N/A", "N/A" };
            dictDfa.Add(combination, emptyArr);

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
                            string optionACleaned = getCombinationOfList(givenNdfa.getNextStates(s, "a", false));
                            string optionBCleaned = getCombinationOfList(givenNdfa.getNextStates(s, "b", false));

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

            // Add all the existing transitions to the private list
            foreach (string key in dictDfa.Keys)
            {
                this.transitions.Add(new CustomTransition(key, dictDfa[key][0], "a"));
                this.transitions.Add(new CustomTransition(key, dictDfa[key][1], "b"));

                //Console.WriteLine("Node: " + key + " a: " + dictDfa[key][0] + " b: " + dictDfa[key][1]);
            }

            // Loop through all the current transitions
            foreach(CustomTransition trans in this.transitions)
            {
                string originalNode = trans.getOrigin();
                string destinationNode = trans.getDestination();

                // Check whether the current transition contains start states
                foreach(string startNode in this.ndfaStartStates)
                {
                    // Check whether the original node contains a start node
                    if(originalNode.Equals(startNode))
                    {
                        markStartState(originalNode);
                    }

                    // Check whether the destination node contains a start node
                    if(destinationNode.Equals(startNode))
                    {
                        markStartState(destinationNode);  
                    }
                }

                // Check whether the current transition contains end states
                foreach(string endNode in this.ndfaEndStates)
                {
                    // Check whether the original node contains an end state
                    if (originalNode.Contains(endNode))
                    {
                        markEndState(originalNode);
                    }

                    // Check whether the destination node contains an end node
                    if (destinationNode.Contains(endNode))
                    {
                        markEndState(destinationNode);
                    }
                }
            }

            // Keep only the unique values within the states
            this.startStates = this.startStates.Distinct().ToList();
            this.endStates = this.endStates.Distinct().ToList();
        }

        /// <summary>
        /// A function which can be used to mark a node as a start state
        /// </summary>
        /// <param name="state">The node which should be marked as a start state</param>
        public void markStartState(string state)
        {
            startStates.Add(state);
        }

        /// <summary>
        /// A function which can be used to mark a node as an end state
        /// </summary>
        /// <param name="state">The node which should be marked as an end state</param>
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
            foreach (string start in startStates)
            {
                Edge startEdge = new Edge();
                List<Transition> startTs = new List<Transition>()
                    {
                        new Transition("node_start -> ", start + ";")
                    };

                startEdge.Transition = startTs;
                graph.AddElement(startEdge);
            }

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

        /// <summary>
        /// A function which can be used to check whether a specific word would be accepted by the dfa
        /// </summary>
        /// <param name="givenWord">The word which should be checked</param>
        /// <returns></returns>
        public bool checkWordAccepted(string givenWord)
        {
            // Initialize the first state (should always be q1)
            string currentState = "q1";

            // Loop through the characters of the give word
            foreach(char currentChar in givenWord)
            {
                // Loop through all the transitions within the dfa
                foreach(CustomTransition trans in this.transitions)
                {
                    if(trans.getOrigin() == currentState && trans.getSymbol() == currentChar.ToString())
                    {
                        // Look up what the new state would be, based on the current state and the current symbol
                        currentState = trans.getDestination();
                        break;
                    }
                }
            }

            // Check whether the state where the word ends up in is an end state
            return endStates.Contains(currentState);
        }
    }
}