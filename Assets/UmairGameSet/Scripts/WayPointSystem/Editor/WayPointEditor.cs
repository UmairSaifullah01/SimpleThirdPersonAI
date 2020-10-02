using System.Collections;
using System.Collections.Generic;
using UMGS.WayPointSystem;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad()]
public class WayPointEditor
{

	[DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
	public static void OnDrawSceneGizmo(WayPoint wayPoint, GizmoType gizmoType)
	{
		if ((gizmoType & GizmoType.Selected) != 0)
		{
			Gizmos.color = Color.yellow;
		}
		else
		{
			Gizmos.color = Color.yellow * 0.5f;
		}

		Gizmos.DrawSphere(wayPoint.transform.position, .1f);
		Gizmos.color = Color.white;
		Gizmos.DrawLine(wayPoint.GetPosition(0), wayPoint.GetPosition(1));
		if (wayPoint.previousWayPoint != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(wayPoint.GetPosition(0), wayPoint.previousWayPoint.GetPosition(0));
		}

		if (wayPoint.nextWayPoint != null)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(wayPoint.GetPosition(1), wayPoint.nextWayPoint.GetPosition(1));
		}

		if (wayPoint.branches != null)
		{
			foreach (WayPoint branch in wayPoint.branches)
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(wayPoint.GetPosition(),branch.GetPosition());
			}
			
		}
	}

}