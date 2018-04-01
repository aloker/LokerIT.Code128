using System;

namespace LokerIT.Code128.Nodes
{
    public abstract class SuccessorNode : Node
    {
        protected SuccessorNode(INode predecessor)
        {
            Predecessor = predecessor ?? throw new ArgumentNullException(nameof(predecessor));
            Start = predecessor.Start + predecessor.Length;
        }

        public override INode Predecessor { get; }
        public override int Start { get; }
    }
}