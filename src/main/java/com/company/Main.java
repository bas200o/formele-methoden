package com.company;

import com.company.automata.Automata;
import com.company.automata.TestAutomata;
import com.company.regex.TestRegExp;

public class Main {

    public static void main(String[] args) {
//        TestRegExp regExp = new TestRegExp();
//        regExp.testLanguage();
        Automata first = TestAutomata.getExampleSlide8Lesson2();
        Automata second = TestAutomata.getExampleSlide14Lesson2();

        first.printTransitions();
        second.printTransitions();
    }
}
