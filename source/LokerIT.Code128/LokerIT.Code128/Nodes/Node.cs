using System.Collections.Generic;

namespace LokerIT.Code128.Nodes
{
    public abstract class Node : INode
    {
        private readonly List<INode> _successors = new List<INode>();
        public IEnumerable<INode> Successors => _successors;
        public abstract INode Predecessor { get; }
        public abstract int Start { get; }
        public abstract int Length { get; }
        public abstract int EmitCount { get; }
        public abstract bool IsTerminal { get; }
        public abstract bool IsHigh { get; }
        public abstract CodeSetType FinalCodeSet { get; }
        public abstract void Emit(IMapping mapping, byte[] input, ICollection<Symbol> buffer);

        public void AddSuccessor(INode successor)
        {
            _successors.Add(successor);
        }
    }
}