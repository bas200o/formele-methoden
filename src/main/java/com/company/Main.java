package com.company;

import com.company.models.CustomNode;
import com.company.models.CustomTransition;
import guru.nidi.graphviz.attribute.*;
import guru.nidi.graphviz.engine.Format;
import guru.nidi.graphviz.engine.Graphviz;
import guru.nidi.graphviz.model.Graph;
import guru.nidi.graphviz.model.Node;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import static guru.nidi.graphviz.attribute.Rank.RankDir.LEFT_TO_RIGHT;
import static guru.nidi.graphviz.model.Factory.*;

public class Main {

    public static void main(String[] args) throws IOException {
        generateFirstExample();
    }

    public static void generateFirstExample() throws IOException {
        System.out.println("-------------------------------");
        System.out.println("In dit voorbeeld zullen we een Graphviz diagram genereren, die aan de volgende eisen moet voldoen;");
        System.out.println("1. Een even aantal b's");
        System.out.println("2. Eindigt op 'aab'");
        System.out.println("-------------------------------");

        // Initialize all the connections
        List<CustomTransition> connectionsQ0 = List.of(new CustomTransition("q0", "a"), new CustomTransition("q1", "b"));
        List<CustomTransition> connectionsQ1 = List.of(new CustomTransition("q2", "a"), new CustomTransition("q0", "b"));
        List<CustomTransition> connectionsQ2 = List.of(new CustomTransition("q3", "a"), new CustomTransition("q0", "b"));
        List<CustomTransition> connectionsQ3 = List.of(new CustomTransition("q3", "a"), new CustomTransition("q4", "b"));
        List<CustomTransition> connectionsQ4 = List.of(new CustomTransition("q0", "a, b"));

        // Initialize the nodes
        CustomNode customNode1 = new CustomNode("q0", connectionsQ0, true, false);
        CustomNode customNode2 = new CustomNode("q1", connectionsQ1, false, false);
        CustomNode customNode3 = new CustomNode("q2", connectionsQ2, false, false);
        CustomNode customNode4 = new CustomNode("q3", connectionsQ3, false, false);
        CustomNode customNode5 = new CustomNode("q4", connectionsQ4, false, true);

        // Create an ArrayList with all the nodes
        ArrayList<CustomNode> givenCustomNodes = new ArrayList<>();
        givenCustomNodes.add(customNode1);
        givenCustomNodes.add(customNode2);
        givenCustomNodes.add(customNode3);
        givenCustomNodes.add(customNode4);
        givenCustomNodes.add(customNode5);

        // Create the graph
        createGraph(givenCustomNodes);
    }

    /**
     * A function which will create a graph
     * @param givenCustomNodes the CustomNodes which should be used in the graph
     * @throws IOException when the file could not be created
     */
    public static void createGraph(ArrayList<CustomNode> givenCustomNodes) throws IOException {
        ArrayList<Node> nodes = new ArrayList<>();

        for (CustomNode given : givenCustomNodes) {
            if(given.isStart() && given.isEnd()) {
                // Create a node with a start indication and a double circle
                String name = String.format("<b>%s</b><br/>start", given.getName());
                Node newNode = node(given.getName()).with(Label.html(name), Color.rgb("1020d0").font()).with(Shape.DOUBLE_CIRCLE);
                nodes.add(newNode);
            } else if (given.isEnd()) {
                // Create a node with a double circle
                Node newNode = node(given.getName()).with(Shape.DOUBLE_CIRCLE);
                nodes.add(newNode);
            } else if (given.isStart()) {
                // Create a node with a start indication
                String name = String.format("<b>%s</b><br/>start", given.getName());
                Node newNode = node(given.getName()).with(Label.html(name), Color.rgb("1020d0").font());
                nodes.add(newNode);
            }

            // Loop through all the given connections, and add these links
            for (CustomTransition connection: given.getConnections()) {
                Node tempNode = node(given.getName()).link(
                        to(node(connection.getName())).with(Label.of(connection.getLabel()))
                );
                nodes.add(tempNode);
            }
        }

        // Generate the graph
        Graph g = graph("example2").directed().graphAttr().with(Rank.dir(LEFT_TO_RIGHT)).with(nodes);
        Graphviz.fromGraph(g).width(900).render(Format.PNG).toFile(new File("example/ex2.png"));
    }
}
