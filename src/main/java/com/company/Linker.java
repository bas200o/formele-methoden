package com.company;

import java.util.ArrayList;

public class Linker {

    public ArrayList<Bridge> bridges = new ArrayList<>();

    public void link(Digraph g, String s, String e, String text) {
        if (text.isBlank()) {
            text = "ε";
        }

        g.link(s, e).setLabel(text);

        this.bridges.add(new Bridge(s, e, text));
    }

    public void connectEnds(Digraph g, ArrayList<String> nodes, String end) {
        if (nodes.isEmpty()) {
            return;
        }

        for (String n : nodes) {
            link(g, n, end, "");
        }
    }


}
