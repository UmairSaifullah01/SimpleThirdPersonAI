using System.Collections.Generic;
using UMGS;
using UMGS.WayPointSystem;
using UnityEngine;

public interface IPositionStats
{

	int   Id              { get; set; }
	int   PositionNo      { get; set; }
	float CoveredDistance { get; set; }

}

public class RaceOrganizer : MonoBehaviour
{

	[SerializeField] Transform[] Cars;
	List<IPositionStats>         Items = new List<IPositionStats>();

	void Start()
	{
		for (int i = 0; i < Cars.Length; i++)
		{
			var p = Cars[i].GetComponent<IPositionStats>();
			if (p != null)
			{
				p.Id = i;
				Items.Add(p);
			}
		}
	}

	void CalculatePosition()
	{
		Items.Sort((a, b) => b.CoveredDistance.CompareTo(a.CoveredDistance));
		for (int i = 0; i < Items.Count; i++)
		{
			Items[i].PositionNo = 1 + i;
		}
	}


	void Update()
	{
		CalculatePosition();
	}

}