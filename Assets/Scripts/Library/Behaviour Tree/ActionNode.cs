using System.Collections.Generic;

namespace BehaviourTree
{
	public abstract class ActionNode : Node
	{
		
		public ActionNode() : base() {}
		public ActionNode(List<Node> children) : base(children) { }
	}
}