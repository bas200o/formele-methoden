using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formele_methoden
{
    class Transition <T> : IComparable<Transition<T>> where T : IComparable
    {
        public static char EPSILON = 'ε';

        public T fromState { get; set; }
        public char symbol { get; set; }
        public T toState   { get; set; }
        #region Constructor
        //Standard Transition Constructor
        public Transition(T from, char symbol, T to)
        {
            this.fromState = from;
            this.symbol = symbol;
            this.toState = to;
        }
        //Loop Transition Constructor
        public Transition(T fromOrTo, char symbol)
        {
            this.fromState = fromOrTo;
            this.symbol = symbol;
            this.toState = fromOrTo;
        }
        //Epsilon Transition Constructor
        public Transition(T from, T to)
        {
            this.fromState = from;
            this.symbol = EPSILON;
            this.toState = to;
        }
        #endregion
        // Compare Transitions
        public int CompareTo(Transition<T> t)
        {
            int fromCompare = fromState.CompareTo(t.fromState);
            int symbolCompare = symbol.CompareTo(t.symbol);
            int toCompare = toState.CompareTo(t.toState);

            return (fromCompare != 0 ? fromCompare : (symbolCompare != 0 ? symbolCompare : toCompare));
        }

        public String toString()
        {
            return "(" + this.fromState + ", " + this.symbol + ")" + "-->" + this.toState;
        }
    }
}
