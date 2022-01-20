namespace formele_methoden
{
    public class Regex
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

        public Regex()
        {
            this._regex = "(a|b)*";
        }

        public Regex(string reg)
        {
            this._regex = reg;
        }

        public Ndfa re2nfa()
        {
            return re2nfa(this._regex);
        }


        public Ndfa re2nfa(string regex)
        {
            regex = regex + "1";
            Ndfa ndfa = new Ndfa();

            for (int i = 0; i < regex.Length; i++)
            {
                char c = regex[i];
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

                if (c == '*')
                {
                    if (startNode != null && endNode != null)
                    {
                        link(startNode, endNode, "");
                        link(endNode, startNode, "");
                    }
                }

                if (c == '+')
                {
                    if (startNode != null && endNode != null)
                    {
                        link(endNode, startNode, "");
                    }
                }

                if (c == 'a' || c == 'b')
                {
                    List<char> chars = new List<char>();

                    chars.Add(c);

                    while (true)
                    {
                        char t = regex[i + 1];
                        if (t == 'a' || t == 'b')
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

                if (c == '1')
                {
                    String n = "q" + i;
                    link(selectedNode, n, "");
                    this.finalNode = n;
                }
            }

            foreach (Bridge b in bridges)
            {
                ndfa.addTransition(new CustomTransition(b.startnode, b.endnode, b.key));
            }
            ndfa.markStartState(this.firstnode);
            ndfa.markEndState(this.finalNode);


            return ndfa;
        }

        public void link(String s, String e, String text)
        {
            if (text.Equals(""))
            {
                text = "ε";
            }
            this.bridges.Add(new Bridge(s, e, text));
        }

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
