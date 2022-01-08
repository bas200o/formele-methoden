using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formele_methoden
{
    class NDFAConverter
    {
        public NDFAConverter()
        {

        }

        public DFA<string> createDFA(NDFA<string> ndfa)
        {
            DFA<string> tempDFA = new DFA<string>(ndfa.alphabet.Count);

            SortedSet<string> newStates = new SortedSet<string>();

            foreach (var state in ndfa.states)
            {
                if (state == ndfa.startStates.ElementAt(0))
                {
                    newStates.Add(state);
                }
               
                foreach (var symbol in ndfa.alphabet)
                {
                    
                    //var specificTrans = ndfa.getTransitions(state, symbol);
                    var toStates = ndfa.getNextStatesEpsilon(state, symbol, false);
                    string tempString = "";
                    foreach (string s in toStates)
                    {
                        tempString += s; 
                    }
                    if (!tempString.Equals(""))
                    {
                        newStates.Add(tempString);
                    } else
                    {
                        newStates.Add("Fuik");
                    }
                }
            }

            tempDFA.states = newStates;

            foreach (var state in tempDFA.states)
            {
                foreach (var symbol in tempDFA.alphabet)
                {
                    string tempToState = "";
                    string[] fromStates = state.Split('q');
                    List<string> toStatesList = new List<string>();

                        for (int i = 1; i < fromStates.Length; i++)
                        {
                            toStatesList.AddRange(ndfa.getNextStatesEpsilon("q" + fromStates[i], symbol, false));
                        }

                    

                    toStatesList = toStatesList.Distinct().ToList();

                    foreach (var item in toStatesList)
                    {
                        tempToState += item;
                    }
                    if (tempToState == "")
                    {
                        tempToState = "Fuik";
                    }
                    if (newStates.Contains(tempToState))
                    {
                        tempDFA.transitions.Add(new Transition<string>(state, symbol, tempToState));
                    }
                   
                }
            }

            tempDFA.states = newStates;
            tempDFA.startStates.Add(ndfa.startStates.First());

            //add Finalstates to new DFA

            foreach (var state in newStates)
            {
                var tempString = state.Split('q');

                var tempFinalState = ndfa.finalStates.ElementAt(0).ToString().Split('q');

                foreach (var item in tempString)
                {
                    if (item.Equals(tempFinalState.Last()))
                    {
                        tempDFA.finalStates.Add(state);
                    }
                }
            }

            return tempDFA;
        }
    }
}