package com.company.models;

/**
 * A record class which will hold the Transition data
 *
 * @param name the name of the node it is connected to
 * @param label the label which should be added to the connection
 */
public record CustomTransition(String name, String label) {

    public String getName() {
        return name;
    }

    public String getLabel() {
        return label;
    }
}