package com.company;

import java.util.ArrayList;
import java.util.HashMap;

public class RE2NDFA {

    public Linker linker = new Linker();

    public String regex;

    public String startNode = null;
    public String endNode = null;
    public String selectedNode = null;
    public Digraph digraph = new Digraph("ndfa");
    public ArrayList<String> connect2end = new ArrayList<>();
    public HashMap<Integer, String> endNodes = new HashMap<>();

    public RE2NDFA(String regex) {
        this.regex = regex;
    }

    public int convert() {

        for (int i = 0; i < regex.length(); i++) {
            char c = regex.charAt(i);
            if (c == '(') {
                startNode = "q" + i;
                selectedNode = startNode;
                digraph.addNode(selectedNode);
                if (endNode != null) {
                    linker.link(digraph, endNode, startNode, "");
                } else {
                    //Mark as start
                }


                continue;
            }

            if (c == ')') {
                endNode = "q" + i;
                digraph.addNode(endNode);
                linker.link(digraph, selectedNode, endNode, "");
                linker.connectEnds(digraph, connect2end, endNode);
                connect2end = new ArrayList<>();
                selectedNode = endNode;
                endNodes.put(i, endNode);
                continue;
            }

            if (c == '*') {
                if (startNode != null && endNode != null) {
                    linker.link(digraph, startNode, endNode, "");
                    linker.link(digraph, endNode, startNode, "");
                }
            }

            if (c == '+') {
                if (startNode != null && endNode != null) {
                    linker.link(digraph, endNode, startNode, "");
                }
            }


            if (c == 'a' || c == 'b') {
                ArrayList<Character> chars = new ArrayList<>();

                chars.add(c);

                while (true) {
                    char t = regex.charAt(i + 1);
                    if (t == 'a' || t == 'b') {
                        chars.add(t);
                        i++;
                    } else {
                        String n = "q" + i;
                        digraph.addNode(n);
                        linker.link(digraph, selectedNode, n, chars.toString());

                        if (t != '|')
                            selectedNode = n;
                        else
                            connect2end.add(n);


                        break;
                    }
                }
            }
        }

        for (int i = regex.length() - 1; i > 0; i--) {
            char c = regex.charAt(i);
            if (c == ')') {
                try {
                    if (regex.charAt(i + 1) == '*') {
                        digraph.markEnd(endNodes.get(i));
                    } else {
                        digraph.markEnd(endNodes.get(i));
                        break;
                    }
                } catch (StringIndexOutOfBoundsException e) {
                    digraph.markEnd(endNodes.get(i));
                    break;
                }
            }
        }

        digraph.generate("graph.dot");
        return 1;
    }
}
