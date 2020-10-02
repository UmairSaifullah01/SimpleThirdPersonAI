using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UMGS.WayPointSystem
{


	public class WayPoint : MonoBehaviour
	{

		public                    WayPoint       previousWayPoint;
		public                    WayPoint       nextWayPoint;
		[Range(0.1f, 15f)] public float          width = 1;
		public                    float          distanceFromPrevious, distanceToNext;
		public                    List<WayPoint> branches    = new List<WayPoint>();
		[Range(0f, 1f)] public    float          branchRatio = 0.5f;

		public Vector3 GetPositionRandom()
		{
			return GetPosition(Random.Range(0f, 1f));
		}

		public Vector3 GetPosition()
		{
			return transform.position;
		}

		public WayPoint GetNextWayPoint()
		{
			if (branches.Count > 0 && Random.Range(0f, 1f) <= branchRatio)
			{
				return branches[Random.Range(0, branches.Count)];
			}

			return nextWayPoint;
		}

		public Vector3 GetPosition(float deltaOffSet)
		{
			Vector3 minBound = transform.position + transform.right * width / 2;
			Vector3 maxBound = transform.position - transform.right * width / 2;
			return Vector3.Lerp(minBound, maxBound, deltaOffSet);
		}

	}


}