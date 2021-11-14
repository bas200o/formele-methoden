package com.company;

import java.util.ArrayList;
import java.util.HashMap;

public class NDFA2DFA {

    private ArrayList<Bridge> NDFABridges;
    public Linker linker = new Linker();
    public Digraph digraph = new Digraph("dfa");

    public NDFA2DFA(ArrayList<Bridge> NDFABridges) {
        this.NDFABridges = NDFABridges;
    }

    public void convert() {

        String selNode;
        Bridge selBridge;

        for (int i = 0; i < NDFABridges.size(); i++) {

            selBridge = NDFABridges.get(i);
            selNode = selBridge.startnode;
            HashMap<String, String> ABridges = new HashMap<>();


            if (selBridge.key.equals("[a]")) {
                String t = findNodes(selNode, selBridge, "a");

                System.out.println("pass a " + i + " " + t);
            }

            if (selBridge.key.equals("[b]")) {
                String t = findNodes(selNode, selBridge, "b");

                System.out.println("pass b " + i + " " + t);
            }
        }



    }

    public String findNodes(String selNode, Bridge selBridge, String letter) {

        selNode = selBridge.endnode;
        String s = selBridge.endnode + "";

        while (true) {
            ArrayList<Bridge> bridges = new ArrayList<>();
            for (Bridge b : NDFABridges) {
                if (b.startnode.equals(selNode)) {
                    bridges.add(b);
                }
            }

            for (Bridge b : bridges) {
                if (b.key.equals("ε")) {
                    if (!s.contains(b.endnode)) {
                        s = s + b.endnode;
                    }
                    selNode = b.endnode;
                }
            }

            boolean exit = true;
            for (Bridge b : bridges) {
                if (b.key.equals("ε")) {
                    exit = false;
                }
            }
            if (exit) {
                break;
            }
        }

        return s;
    }

}
