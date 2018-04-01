namespace LokerIT.Code128.Nodes
{
    public abstract class StartNode : Node
    {
        public override INode Predecessor => null;
        public override int Start => 0;
    }
}