using System;
using UnityEngine;
using Object = System.Object;

namespace BehaviourTree
{
	public abstract class BehaviourTreeRunner : MonoBehaviour
	{
		[SerializeField] protected Tree tree;

        public void Init()
        {
			tree = tree.Clone();
			LoadData();
			tree.root.Init();
		}
        private void Start()
		{
			if (tree == null) return;
			Init();
		}

		protected abstract void LoadData();

		private void Update()
		{
			if (tree == null) return;
			tree.Update();
		}
	}
}

