package com.company;

import com.oracle.truffle.js.runtime.objects.Null;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;

public class Main {
    public static void main(String[] args) throws Exception{
        String regex;

        regex = "(a)+(a|b)*(a|b)";

        RE2NDFA re2NDFA = new RE2NDFA(regex);
        re2NDFA.convert();

        NDFA2DFA ndfa2DFA = new NDFA2DFA(re2NDFA.getBridges());
        ndfa2DFA.convert();




    }

}
