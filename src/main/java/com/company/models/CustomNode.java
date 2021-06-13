package com.company.models;

import java.util.ArrayList;
import java.util.List;

/**
 * A record class which will hold the node data
 *
 * @param name The name of the node
 * @param connections a List of CustomTransitions used for the links
 * @param start a bool containing true/false, depending on whether the node is a starting point
 * @param end a bool containing true/false, depending on whether the node is an end point
 */
public record CustomNode(String name, ArrayList<CustomTransition> connections, boolean start,
                         boolean end) {

    public String getName() {
        return this.name;
    }

    public ArrayList<CustomTransition> getConnections() {
        return this.connections;
    }

    public boolean isStart() {
        return start;
    }

    public boolean isEnd() {
        return end;
    }
}