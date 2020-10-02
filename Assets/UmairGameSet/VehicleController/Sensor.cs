using UnityEngine;


namespace UMGS.Vehicle
{


	[System.Serializable]
	public class Sensor
	{

		[SerializeField]                    Transform location;
		[SerializeField]                    LayerMask layerMask;
		[SerializeField] [Range(0.1f, 1f)]  float     influance = 0.1f;
		[SerializeField] [Range(0.1f, 10f)] float     range     = 0.1f;

		/// <summary>
		/// check collision and give influance value with distance gap 
		/// </summary>
		/// <param name="transform"></param>
		/// <returns></returns>
		public float Evaluate()
		{
			if (Physics.Raycast(location.position, location.forward, out RaycastHit hit, range, layerMask))
			{
				Debug.DrawLine(location.position, hit.point, Color.red);
				return influance / (Vector3.Distance(location.position, hit.point) / range);
			}

			Debug.DrawLine(location.position, location.position + location.forward * range, Color.green);
			return 0.0f;
		}

	}


}