using System.Collections.Generic;

namespace LokerIT.Code128.Nodes
{
    public interface INode
    {
        INode Predecessor { get; }
        IEnumerable<INode> Successors { get; }
        int Start { get; }
        int Length { get; }
        int EmitCount { get; }
        bool IsTerminal { get; }
        bool IsHigh { get; }
        CodeSetType FinalCodeSet { get; }
        void Emit(IMapping mapping, byte[] input, ICollection<Symbol> buffer);
        void AddSuccessor(INode node);
    }
}