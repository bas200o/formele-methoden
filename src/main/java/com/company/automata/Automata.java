package com.company.automata;
/**
 * The class Automata represents both DFA and NDFA: some NDFA's are also DFA
 * Using the method isDFA we can check this
 * 
 * We use '$' to denote the empty symbol epsilon
 * 
 * @author Paul de Mast
 * @version 1.0

 */

import java.util.Arrays;
import java.util.Set;
import java.util.SortedSet;
import java.util.TreeSet;

public class Automata<T extends Comparable>
{

    // Or use a Map structure
    private Set<Transition <T>> transitions;

    private SortedSet<T> states;
    private SortedSet<T> startStates;
    private SortedSet<T> finalStates;
    private SortedSet<Character> symbols;

    public Automata()
    {
           this(new TreeSet<Character>());
    }
    
    public Automata(Character [] s)
    {   
        this(new TreeSet<Character>(Arrays.asList(s)) );
    }

    public Automata(SortedSet<Character> symbols)
    {
        transitions = new TreeSet<>();
        states = new TreeSet<>();
        startStates = new TreeSet<>();
        finalStates = new TreeSet<>();
        this.setAlphabet(symbols);
    }
    
    public void setAlphabet(Character [] s)
    {
        this.setAlphabet(new TreeSet<Character>(Arrays.asList(s)));
    }
    
    public void setAlphabet(SortedSet<Character> symbols)
    {
       this.symbols = symbols;
    }
    
    public SortedSet<Character> getAlphabet()
    {
       return symbols;
    }
    
    public void addTransition(Transition<T> t)
    {
        transitions.add(t);
        states.add(t.getFromState());
        states.add(t.getToState());        
    }
    
    public void defineAsStartState(T t)
    {
        // if already in states no problem because a Set will remove duplicates.
        states.add(t);
        startStates.add(t);        
    }

    public void defineAsFinalState(T t)
    {
        // if already in states no problem because a Set will remove duplicates.
        states.add(t);
        finalStates.add(t);        
    }

    public void printTransitions()
    {

        for (Transition<T> t : transitions)
        {
            System.out.println (t);
        }
    }
}
