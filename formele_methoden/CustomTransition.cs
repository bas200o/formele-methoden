using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formele_methoden
{
    /// <summary>
    /// A custom class which functions as the transitions between nodes
    /// </summary>
    public class CustomTransition
    {
        private String originState;
        private String destinationState;
        private String symbol;

        /// <summary>
        /// The constructor of the CustomTransition class
        /// </summary>
        /// <param name="origin">A string containing the original node of the transition</param>
        /// <param name="destination">A string containing the destination node of the transition</param>
        /// <param name="symbol">A string containing the symbol of the transition</param>
        public CustomTransition(string origin, string destination, string symbol)
        {
            this.originState = origin;
            this.destinationState = destination;
            this.symbol = symbol;
        }

        /// <summary>
        /// A function which can be used to get the transition, which can be used to draw the graph
        /// </summary>
        /// <returns>A string containing the transition within graphviz</returns>
        public string getTransition()
        {
            return String.Format("{0} -> {1}", originState, destinationState);
        }

        /// <summary>
        /// A function which can be used to get the transition's origin node
        /// </summary>
        /// <returns>A string containing the transition's origin node</returns>
        public string getOrigin()
        {
            return this.originState;
        }

        /// <summary>
        /// A function which can be used to get the transition's destination node
        /// </summary>
        /// <returns>A string containing the transition's destination node</returns>
        public string getDestination()
        {
            return this.destinationState;
        }

        /// <summary>
        /// A function which can be used to get the transition's symbol
        /// </summary>
        /// <returns>A string containing the transition's symbol</returns>
        public string getSymbol()
        {
            return this.symbol;
        }

        /// <summary>
        /// A function which can be used to get the label, which can be used to draw the graph
        /// </summary>
        /// <returns>A string containing the transition's label within graphviz</returns>
        public string getLabel()
        {
            return String.Format("[label={0}];", symbol);
        }
    }
}
