using System;
using UnityEngine;
using Object = System.Object;

namespace BehaviourTree
{
	public abstract class BehaviourTreeRunner : MonoBehaviour
	{
		[SerializeField] protected Tree tree;

		private void Start()
		{
			tree = tree.Clone();
			LoadData();
			tree.root.Init();
		}

		protected abstract void LoadData();

		private void Update()
		{
			tree.Update();
		}
	}
}

