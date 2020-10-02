using UnityEditor;
using UnityEngine;


namespace UMGS.WayPointSystem
{


	[CustomEditor(typeof(WayPointManager))]
	public class WayPointManagerEditor : Editor
	{

		WayPointManager  _wayPointManager;
		public Transform wayPointRoot;

		public override void OnInspectorGUI()
		{
			_wayPointManager = (WayPointManager) target;
			SerializedObject obj = new SerializedObject(_wayPointManager);
			if (wayPointRoot == null)
			{
				EditorGUILayout.HelpBox("Root transform must be selected", MessageType.Warning);
				wayPointRoot = Selection.activeGameObject.transform;
			}
			else
			{
				EditorGUILayout.BeginVertical("box");
				var v = obj.FindProperty("wayPointRoot");
				_wayPointManager.width = EditorGUILayout.Slider("Default Width", _wayPointManager.width, 0.1f, 15);
				DrawButtons();
				EditorGUILayout.EndVertical();
			}

			obj.ApplyModifiedProperties();
		}

		void DrawButtons()
		{
			if (GUILayout.Button("Create WayPoint"))
			{
				CreateWayPoint();
			}

			if (GUILayout.Button("Create Branch"))
			{
				CreateBranch();
			}

			if (GUILayout.Button("Create WayPoint Before"))
				CreateWayPointBefore();
			if (GUILayout.Button("Create WayPoint After"))
				CreateWayPointAfter();
			if (GUILayout.Button("Remove WayPoint"))
				RemoveWayPoint();
			if (GUILayout.Button("Place To Ground"))
				PlacetoGround();
			if (GUILayout.Button("Rotate To Path"))
				RotateToPath();
		}

		void CreateBranch()
		{
			GameObject wayPointObject = new GameObject($"WayPoint {wayPointRoot.childCount}", typeof(WayPoint));
			wayPointObject.transform.SetParent(wayPointRoot, false);
			var wayPoint   = wayPointObject.GetComponent<WayPoint>();
			var branchFrom = Selection.activeGameObject.GetComponent<WayPoint>();
			branchFrom.branches.Add(wayPoint);
			wayPoint.transform.position = branchFrom.transform.position;
			wayPoint.transform.forward  = branchFrom.transform.forward;
			Selection.activeGameObject  = wayPoint.gameObject;
		}

		void RemoveWayPoint()
		{
			var selectedWayPoint = Selection.activeGameObject.GetComponent<WayPoint>();
			if (selectedWayPoint.nextWayPoint)
			{
				selectedWayPoint.nextWayPoint.previousWayPoint = selectedWayPoint.previousWayPoint;
			}

			if (selectedWayPoint.previousWayPoint != null)
			{
				selectedWayPoint.previousWayPoint.nextWayPoint = selectedWayPoint.nextWayPoint;
				Selection.activeGameObject                     = selectedWayPoint.previousWayPoint.gameObject;
			}

			DestroyImmediate(selectedWayPoint.gameObject);
		}

		public void PlacetoGround()
		{
			foreach (WayPoint trans in wayPointRoot.GetComponentsInChildren<WayPoint>())
			{
				//define ray to cast downwards waypoint position
				Ray ray1 = new Ray(trans.GetPosition(1) + new Vector3(0, 2f, 0), -Vector3.up);
				Ray ray2 = new Ray(trans.GetPosition(0) + new Vector3(0, 2f, 0), -Vector3.up);
				Undo.RecordObject(trans, "Place To Ground");
				RaycastHit hit;
				Vector3    v1 = trans.transform.position, v2 = trans.transform.position;
				//cast ray against ground, if it hit:
				if (Physics.Raycast(ray1, out hit, 100))
				{
					//position waypoint to hit point
					v1 = hit.point;
				}

				if (Physics.Raycast(ray2, out hit, 100))
				{
					v2 = hit.point;
				}

				trans.transform.position = v1 + (v2                                     - v1) / 2;
				trans.transform.rotation = Quaternion.Euler(trans.transform.eulerAngles - Vector3.forward * Vector2.Angle(v2 - v1, trans.transform.position));
			}
		}

		void CreateWayPointAfter()
		{
			GameObject wayPointObject = new GameObject($"WayPoint {wayPointRoot.childCount}", typeof(WayPoint));
			wayPointObject.transform.SetParent(wayPointRoot, false);
			var wayPoint = wayPointObject.GetComponent<WayPoint>();
			wayPoint.width = _wayPointManager.width;
			var selectedWayPoint = Selection.activeGameObject.GetComponent<WayPoint>();
			wayPointObject.transform.position = selectedWayPoint.transform.position;
			wayPointObject.transform.forward  = selectedWayPoint.transform.forward;
			wayPoint.previousWayPoint         = selectedWayPoint;
			if (selectedWayPoint.nextWayPoint)
			{
				selectedWayPoint.nextWayPoint.previousWayPoint = wayPoint;
				wayPoint.nextWayPoint                          = selectedWayPoint.nextWayPoint;
			}

			selectedWayPoint.nextWayPoint = wayPoint;
			wayPoint.transform.SetSiblingIndex(selectedWayPoint.transform.GetSiblingIndex());
			Selection.activeGameObject = wayPointObject;
		}

		void CreateWayPointBefore()
		{
			GameObject wayPointObject = new GameObject($"WayPoint {wayPointRoot.childCount}", typeof(WayPoint));
			wayPointObject.transform.SetParent(wayPointRoot, false);
			var wayPoint = wayPointObject.GetComponent<WayPoint>();
			wayPoint.width = _wayPointManager.width;
			var selectedWayPoint = Selection.activeGameObject.GetComponent<WayPoint>();
			wayPointObject.transform.position = selectedWayPoint.transform.position;
			wayPointObject.transform.forward  = selectedWayPoint.transform.forward;
			if (selectedWayPoint.previousWayPoint != null)
			{
				wayPoint.previousWayPoint                      = selectedWayPoint.previousWayPoint;
				selectedWayPoint.previousWayPoint.nextWayPoint = wayPoint;
			}

			wayPoint.nextWayPoint             = selectedWayPoint;
			selectedWayPoint.previousWayPoint = wayPoint;
			wayPoint.transform.SetSiblingIndex(selectedWayPoint.transform.GetSiblingIndex());
			Selection.activeGameObject = wayPointObject;
		}

		public void CreateWayPoint()
		{
			GameObject wayPointObject = new GameObject($"WayPoint {wayPointRoot.childCount}", typeof(WayPoint));
			wayPointObject.transform.SetParent(wayPointRoot, false);
			var wayPoint = wayPointObject.GetComponent<WayPoint>();
			wayPoint.width             = _wayPointManager.width;
			Selection.activeGameObject = wayPointObject;
			if (wayPointRoot.childCount > 1)
			{
				wayPoint.previousWayPoint              = wayPointRoot.GetChild(wayPointRoot.childCount - 2).GetComponent<WayPoint>();
				wayPoint.previousWayPoint.nextWayPoint = wayPoint;
				wayPoint.transform.position            = wayPoint.previousWayPoint.transform.position;
				wayPoint.transform.forward             = wayPoint.previousWayPoint.transform.forward;
			}
		}

		public void OnSceneGUI()
		{
			if (Event.current.type != EventType.KeyDown) return;
			if (Event.current.keyCode == KeyCode.P)
			{
				Ray        worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
				RaycastHit hitInfo;
				if (Physics.Raycast(worldRay, out hitInfo))
				{
					Event.current.Use();
					GameObject wayPointObject = new GameObject($"WayPoint {wayPointRoot.childCount}", typeof(WayPoint));
					wayPointObject.transform.SetParent(wayPointRoot, false);
					var wayPoint = wayPointObject.GetComponent<WayPoint>();
					wayPoint.width             = _wayPointManager.width;
					Selection.activeGameObject = wayPointObject;
					if (wayPointRoot.childCount > 0)
					{
						wayPoint.previousWayPoint              = wayPointRoot.GetChild(wayPointRoot.childCount - 2).GetComponent<WayPoint>();
						wayPoint.previousWayPoint.nextWayPoint = wayPoint;
						wayPoint.transform.position            = hitInfo.point;
						wayPoint.transform.forward             = wayPoint.previousWayPoint.transform.forward;
					}
				}
			}
		}

		void RotateToPath()
		{
			foreach (Transform trans in wayPointRoot)
			{
				Undo.RecordObject(trans, "Rotate Waypoints");
				var next = trans.GetComponent<WayPoint>().nextWayPoint;
				if (next) trans.LookAt(next.transform);
			}
		}

		void PreviousStepCreate()
		{
		}

	}


}