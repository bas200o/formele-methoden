package com.company;

import guru.nidi.graphviz.attribute.Color;
import guru.nidi.graphviz.attribute.Label;
import guru.nidi.graphviz.engine.Format;
import guru.nidi.graphviz.engine.Graphviz;
import guru.nidi.graphviz.model.Graph;
import guru.nidi.graphviz.model.MutableGraph;
import guru.nidi.graphviz.model.Node;

import javax.swing.*;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;

import static guru.nidi.graphviz.model.Factory.*;

public class Main {

    public static void main(String[] args) throws IOException {

        String regex = "(a|b)(a|b)(a)*(a|b)(a|b)*(a|b)";


        String startNode = null;
        String endNode = null;
        String selectedNode = null;
        Digraph digraph = new Digraph("ndfa");
        ArrayList<String> connect2end = new ArrayList<>();
        HashMap<Integer, String> endNodes = new HashMap<>();


        // enter sequence


        for (int i = 0; i < regex.length(); i++) {
            char c = regex.charAt(i);
            if (c == '(') {
                startNode = "q" + i;
                selectedNode = startNode;
                digraph.addNode(selectedNode);
                if (endNode != null) {
                    link(digraph, endNode, startNode, "");
                } else {
                    //Mark as start
                }


                continue;
            }

            if (c == ')') {
                endNode = "q" + i;
                digraph.addNode(endNode);
                link(digraph, selectedNode, endNode, "");
                connectEnds(digraph, connect2end, endNode);
                connect2end = new ArrayList<>();
                selectedNode = endNode;
                endNodes.put(i, endNode);
                continue;
            }

            if (c == '*') {
                if (startNode != null && endNode != null) {
                    link(digraph, startNode, endNode, "");
                    link(digraph, endNode, startNode, "");
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
                        link(digraph, selectedNode, n, chars.toString());

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
                }catch (StringIndexOutOfBoundsException e)
                {
                    digraph.markEnd(endNodes.get(i));
                    break;
                }
            }
        }


        digraph.generate("graph.dot");
    }

    public static void connectEnds(Digraph g, ArrayList<String> nodes, String end) {
        if (nodes.isEmpty()) {
            return;
        }

        for (String n : nodes) {
            link(g, n, end, "");
        }


    }

    public static void link(Digraph g, String s, String e, String text) {
        if (text.isBlank()) {
            text = "ε";
        }

        g.link(s, e).setLabel(text);
    }


}
