using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formele_methoden
{
    public class CustomTransition
    {
        private String originState;
        private String destinationState;
        private String symbol;

        public CustomTransition(string origin, string destination, string symbol)
        {
            this.originState = origin;
            this.destinationState = destination;
            this.symbol = symbol;
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}: {2}", originState, destinationState, symbol);
        }

        public string getTransition()
        {
            return String.Format("{0} -> {1}", originState, destinationState);
        }

        public string getOrigin()
        {
            return this.originState;
        }

        public string getDestination()
        {
            return this.destinationState;
        }

        public string getSymbol()
        {
            return this.symbol;
        }

        public string getLabel()
        {
            return String.Format("[label={0}];", symbol);
        }
    }
}
