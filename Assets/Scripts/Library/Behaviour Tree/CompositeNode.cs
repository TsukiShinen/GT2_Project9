using System.Collections.Generic;

namespace BehaviourTree
{
	public abstract class CompositeNode : Node
	{
		
		public CompositeNode() : base() {}
		public CompositeNode(List<Node> children) : base(children) { }
	}
}