package com.company;

import guru.nidi.graphviz.engine.Format;
import guru.nidi.graphviz.engine.Graphviz;
import guru.nidi.graphviz.model.MutableGraph;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;

import static guru.nidi.graphviz.model.Factory.*;

class CustomNode {
    private String name;
    private ArrayList<String> connections;

    CustomNode(String name, ArrayList<String> connections) {
        this.name = name;
        this.connections = connections;
    }

    public String getName() {
      return this.name;
    }

    public ArrayList<String> getConnections() {
        return this.connections;
    }
}

public class Main {

    public static void main(String[] args) throws IOException {
        generateExampleOne();
    }

    public static void generateExampleOne() throws IOException {
        System.out.println("------------------------------------------------------\n" +
                "Generating a graph with the following requirements:\n" +
                "1. The string should have an equal amount of b's\n" +
                "2. The string should end with aab.\n" +
                "------------------------------------------------------\n");

        createGraph(initNodes());
    }

    public static ArrayList<CustomNode> initNodes() {
        ArrayList<String> temp = new ArrayList<>();
        temp.add("second");
        temp.add("third");

        CustomNode customNode1 = new CustomNode("first", temp);
        CustomNode customNode2 = new CustomNode("second", new ArrayList<>());
        CustomNode customNode3 = new CustomNode("third", new ArrayList<>());
        CustomNode customNode4 = new CustomNode("fourth", new ArrayList<>());

        ArrayList<CustomNode> givenCustomNodes = new ArrayList<>();
        givenCustomNodes.add(customNode1);
        givenCustomNodes.add(customNode2);
        givenCustomNodes.add(customNode3);
        givenCustomNodes.add(customNode4);

        return givenCustomNodes;
    }

    public static void createGraph(ArrayList<CustomNode> givenCustomNodes) throws IOException {
        MutableGraph g = mutGraph("example1").setDirected(true).use((gr, ctx) -> {
            for(int i = 0; i < givenCustomNodes.size(); i++) {
                mutNode(givenCustomNodes.get(i).getName());
            }

            for(int i = 0; i < givenCustomNodes.size(); i++) {
                for (int j = 0; j < givenCustomNodes.get(i).getConnections().size(); j++) {
                    mutNode(givenCustomNodes.get(i).getName()).addLink(givenCustomNodes.get(i).getConnections().get(j));
                }
            }
        });
        Graphviz.fromGraph(g).width(200).render(Format.PNG).toFile(new File("example/ex1i.png"));
    }
}
