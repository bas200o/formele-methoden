package com.company.models;
import com.company.enums.TransitionType;

/**
 * A record class which will hold the Transition data
 *
 * @param name the name of the node it is connected to
 * @param label the label which should be added to the connection
 */
public record CustomTransition(String name, String label, TransitionType transitionType) {

    public String getName() {
        return name;
    }

    public String getLabel() {
        return label;
    }

    public TransitionType getTransitionType() {
        return transitionType;
    }
}