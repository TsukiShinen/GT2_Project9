using System.Collections.Generic;

namespace BehaviourTree
{
	public abstract class DecorateNode : Node
	{
		
		public DecorateNode() : base() {}
		public DecorateNode(List<Node> children) : base(children) { }
	}
}