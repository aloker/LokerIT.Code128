using System.Collections.Generic;
using LokerIT.Code128.Nodes;

namespace LokerIT.Code128.Optimization
{
    public class Tracker
    {
        private readonly Stack<int> _trail = new Stack<int>();
        private int _cheapestCost = int.MaxValue;
        private int _running;

        public INode CheapestNode { get; private set; }

        public bool Push(INode node)
        {
            var cost = node.EmitCount;
            if (_running + cost >= _cheapestCost)
            {
                // no need to consider this node
                return false;
            }

            _trail.Push(cost);
            _running += cost;

            if (node.IsTerminal && _running < _cheapestCost)
            {
                _cheapestCost = _running;
                CheapestNode = node;
            }

            return true;
        }

        public void Pop()
        {
            _running -= _trail.Pop();
        }
    }
}