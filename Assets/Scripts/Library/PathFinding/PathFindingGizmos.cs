using System;
using UnityEngine;

namespace PathFinding
{
	public enum DebugMode
	{
		Grid,
		Cost,
		Path
	}
	
	public class PathFindingGizmos : MonoBehaviour
	{
		[SerializeField] private PathFindingController pathFindingController;
		[SerializeField] private DebugMode debugMode;
		
		
#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			switch (debugMode)
			{
				case DebugMode.Grid:
					pathFindingController.OnDrawGizmosGrid(Color.yellow);
					break;
				case DebugMode.Cost:
					pathFindingController.OnDrawGizmosGrid(Color.green);
					pathFindingController.OnDrawGizmosCost();
					break;
				case  DebugMode.Path:
					pathFindingController.OnDrawGizmosGrid(Color.green);
					pathFindingController.OnDrawGizmosPath();
					break;
				default:
					break;
			}
		}
#endif
	}
}