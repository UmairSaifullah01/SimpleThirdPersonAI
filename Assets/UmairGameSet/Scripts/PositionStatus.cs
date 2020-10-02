using System;
using System.Collections;
using TMPro;
using UMGS.WayPointSystem;
using UnityEngine;

public class PositionStatus : MonoBehaviour, IPositionStats
{

	private WayPoint             currentWayPoint;
	WayPointManager              Manager;
	[SerializeField] int         fromIndex, endIndex;
	WayPoint[]                   wayPoints;
	float                        locationUpdateRate   = 0.1f;
	int                          currentWayPointIndex = 0;
	[SerializeField] TextMeshPro text;
	public           GameObject  Effect;

	void Start()
	{
		Manager         = FindObjectOfType<WayPointManager>();
		currentWayPoint = Manager.head;
		wayPoints       = Manager.CopyPoints(fromIndex, endIndex - fromIndex);
		StartCoroutine(CheckWaypointDistance());
	}

	// Update is called once per frame


	public int Id         { get; set; }
	public int PositionNo { get; set; }
	public float CoveredDistance
	{
		get => CoveredDistanceCal();
		set { ; }
	}
	float waypointdistance = 0;

	IEnumerator CheckWaypointDistance()
	{
		while (true)
		{
			yield return new WaitForSeconds(locationUpdateRate);
			currentWayPointIndex = MinimumDistance();
			currentWayPoint      = wayPoints[currentWayPointIndex];
			waypointdistance     = Manager.GetDistance(currentWayPoint, currentWayPointIndex - fromIndex);
			text.text            = PositionNo.ToString();
		}

		yield return null;
	}

	void Update()
	{
		if (Input.GetButtonDown("Respawn"))
		{
			RespawnPosition();
		}
	}

	public void RespawnPosition()
	{
		transform.position = currentWayPoint.GetPosition() + Vector3.up * 3;
		transform.rotation = Quaternion.LookRotation((currentWayPoint.nextWayPoint.GetPosition() - currentWayPoint.GetPosition()).normalized, Vector3.up);
		Instantiate(Effect, transform);
	}

	int MinimumDistance()
	{
		float minidis = Vector3.Distance(wayPoints[0].GetPosition(), transform.position);
		int   Index   = 0;
		for (int i = 0; i < wayPoints.Length; i++)
		{
			float dist = Vector3.Distance(wayPoints[i].GetPosition(), transform.position);
			if (!(dist < minidis)) continue;
			Index   = i;
			minidis = dist;
		}

		return Index;
	}

	float CoveredDistanceCal()
	{
		float distance = waypointdistance;
		float cn       = currentWayPoint.distanceToNext;
		float c        = Vector3.Distance(transform.position, currentWayPoint.GetPosition());
		float n        = Vector3.Distance(transform.position, currentWayPoint.nextWayPoint.GetPosition());
		distance += n > cn ? c : -c;
		return distance;
	}

}