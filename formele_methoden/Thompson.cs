using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formele_methoden
{
    public class Thompson
    {
        private string _regex { get; set; }
        public String startNode = null;
        public String endNode = null;
        public String selectedNode = null;
        public List<String> connect2end = new List<string>();
        public Dictionary<int, string> endNodes = new Dictionary<int, string>();
        public List<Bridge> bridges = new List<Bridge>();
        public String firstnode = null;
        public String finalNode = null;

        /**
        Bridge stuct serves to Link 2 nodes together.
        Comparable to the CustomTransition class.
        */
        public struct Bridge
        {
            public string startnode { get; }
            public string endnode { get; }
            public string key { get; }

            public Bridge(string s, string e, string k)
            {
                startnode = s;
                endnode = e;
                key = k;
            }
        }

        // Default constructor
        public Thompson()
        {
            this._regex = "(a|b)*";
        }

        // Constructer with Custom regex string
        public Thompson(string givenRegex)
        {
            this._regex = givenRegex;
        }

        // Calls the class variable for regex
        public Ndfa generateThompsonNdfa()
        {
            return re2nfa(this._regex);
        }

        /// <summary>
        /// Converts regex string to a NDFA object.
        /// supports all lowercase letter alphabet
        /// </summary>
        /// <param name="regex">Regex string like (a|b)* supported opperators are | + *</param>
        public Ndfa re2nfa(string regex)
        {
            // Adds a 1 as end flag for the regex
            regex = regex + "1";
            Ndfa ndfa = new Ndfa();

            // Main loop for converting regex to NDFA
            for (int i = 0; i < regex.Length; i++)
            {
                char c = regex[i];

                // Checks if a new alphabet has started
                if (c == '(')
                {
                    startNode = "q" + i;
                    selectedNode = startNode;
                    if (endNode != null)
                    {
                        link(endNode, startNode, "");
                    }
                    else
                    {
                        firstnode = startNode;
                    }
                    continue;
                }

                // checks if alphabet has ended
                if (c == ')')
                {
                    endNode = "q" + i;
                    link(selectedNode, endNode, "");
                    connectEnds(connect2end, endNode);
                    connect2end = new List<string>();
                    selectedNode = endNode;
                    endNodes.Add(i, endNode);
                    continue;
                }

                // Makes 2 links from the start of the alphabet to the end
                if (c == '*')
                {
                    if (startNode != null && endNode != null)
                    {
                        link(startNode, endNode, "");
                        link(endNode, startNode, "");
                    }
                }

                // Makes a link from the back to the start of the alphabet
                if (c == '+')
                {
                    if (startNode != null && endNode != null)
                    {
                        link(endNode, startNode, "");
                    }
                }

                // Creates the alphabet nodes and links staring node to it
                if (Char.IsLower(c))
                {
                    List<char> chars = new List<char>();

                    chars.Add(c);

                    while (true)
                    {
                        char t = regex[i + 1];
                        if (Char.IsLower(t))
                        {
                            chars.Add(t);
                            i++;
                        }
                        else
                        {
                            String n = "q" + i;
                            link(selectedNode, n, new string(chars.ToArray()));

                            if (t != '|')
                                selectedNode = n;
                            else
                                connect2end.Add(n);

                            break;
                        }
                    }
                }

                // Marks the end of the regex sting
                if (c == '1')
                {
                    String n = "q" + i;
                    link(selectedNode, n, "");
                    this.finalNode = n;
                }
            }

            // Converts the bridge object to Customtransition
            foreach (Bridge b in bridges)
            {
                ndfa.addTransition(new CustomTransition(b.startnode, b.endnode, b.key));
            }
            ndfa.markStartState(this.firstnode);
            ndfa.markEndState(this.finalNode);

            return ndfa;
        }


        // Links 2 nodes with a lable for graphviz
        public void link(String s, String e, String text)
        {
            if (text.Equals(""))
            {
                text = "ε";
            }
            this.bridges.Add(new Bridge(s, e, text));
        }

        // Links all nodes with alphabet to the ending node
        public void connectEnds(List<string> nodes, String end)
        {
            if (!nodes.Any())
            {
                return;
            }

            foreach (string n in nodes)
            {
                link(n, end, "");
            }
        }
    }
}