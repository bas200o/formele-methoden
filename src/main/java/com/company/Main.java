package com.company;

import guru.nidi.graphviz.attribute.Color;
import guru.nidi.graphviz.attribute.Label;
import guru.nidi.graphviz.engine.Format;
import guru.nidi.graphviz.engine.Graphviz;
import guru.nidi.graphviz.model.Graph;
import guru.nidi.graphviz.model.MutableGraph;
import guru.nidi.graphviz.model.Node;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;

import static guru.nidi.graphviz.model.Factory.*;

public class Main {

    public static void main(String[] args) throws IOException {

        String regex = "(a|b)(a)*";
        Node startNode = null;
        Node endNode = null;
        Node selectedNode = null;
        Graph g = graph("ndfa").directed();
        ArrayList<Node> connect2end = new ArrayList<>();


        // enter sequence


        for (int i = 0; i < regex.length(); i++) {
            char c = regex.charAt(i);
            if(c == '('){
                startNode = node("q" + i).with(Label.of("q" + i));
                selectedNode = startNode;
                continue;
            }

            if(c == ')'){
                endNode = node("q" + i).with(Label.of("q" + i));
                selectedNode = endNode;
                connectEnds(g, connect2end, endNode);
                continue;
            }

            if(c == '*'){
                if(startNode != null && endNode != null) {
                    link(g, startNode, endNode, "");
                    link(g, endNode, startNode, "");
                }
            }


            if (c == 'a' || c == 'b'){
                ArrayList<Character> chars = new ArrayList<>();

                chars.add(c);

                while (true)
                {
                    char t = regex.charAt(i + 1);
                    if (t == 'a' || t == 'b')
                    {
                        chars.add(t);
                        i++;
                    }else{
                        Node n = node("q"+i).with(Label.of("q" + i));
                        link(g, selectedNode, n, chars.toString());

                        if (t != '|')
                            selectedNode = n;
                        else
                            connect2end.add(n);


                        break;
                    }
                }
            }
        }

        Node start = node("start");
        Node end = node("end");

        Graph f = graph()
        .with(
                start.link(end)
        );

        Graphviz.fromGraph(f).width(200).render(Format.PNG).toFile(new File("example/ex1m.png"));
    }

    public static void connectEnds(Graph g, ArrayList<Node> nodes, Node end)
    {
        if (nodes.isEmpty())
        {
            return;
        }

        for (Node n : nodes) {
            link(g, n, end,"");
        }

    }

    public static void link(Graph g, Node s, Node e, String text)
    {
        g.with(
                s.link(
                        to(e).with(Label.of(text))
                )
        );
    }


}
