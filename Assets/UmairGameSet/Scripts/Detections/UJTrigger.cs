using System;
using UnityEngine;


namespace UMGS
{


	public class UJTrigger : MonoBehaviour
	{

		public Action<Collider> enter, stay, exit;

		private void OnTriggerEnter(Collider other)
		{
			if (enter != null)
				enter.Invoke(other);
		}

		private void OnTriggerStay(Collider other)
		{
			if (stay != null)
				stay.Invoke(other);
		}

		private void OnTriggerExit(Collider other)
		{
			if (exit != null)
				exit.Invoke(other);
		}

	}

	public static class UMTrigger
	{

		static UJTrigger GetTrigger(GameObject gameObject)
		{
			UJTrigger trgr = gameObject.GetComponent<UJTrigger>();
			if (trgr == null)
			{
				trgr = gameObject.AddComponent<UJTrigger>();
			}

			trgr.hideFlags = HideFlags.HideInInspector;
			return trgr;
		}

		public static void OnTriggerEnter(this Collider col, Action<Collider> trigger)
		{
			UJTrigger trgr = GetTrigger(col.gameObject);
			trgr.enter = trigger;
		}


		public static void OnTriggerStay(this Collider col, Action<Collider> trigger)
		{
			UJTrigger trgr = GetTrigger(col.gameObject);
			trgr.stay = trigger;
		}

		public static void OnTriggerExit(this Collider col, Action<Collider> trigger)
		{
			UJTrigger trgr = GetTrigger(col.gameObject);
			trgr.exit = trigger;
		}

		public static void OnTriggerEnter(this Rigidbody rigidbody, Action<Collider> trigger)
		{
			UJTrigger trgr = GetTrigger(rigidbody.gameObject);
			trgr.enter = trigger;
		}


		public static void OnTriggerStay(this Rigidbody rigidbody, Action<Collider> trigger)
		{
			UJTrigger trgr = GetTrigger(rigidbody.gameObject);
			trgr.stay = trigger;
		}

		public static void OnTriggerExit(this Rigidbody rigidbody, Action<Collider> trigger)
		{
			UJTrigger trgr = GetTrigger(rigidbody.gameObject);
			trgr.exit = trigger;
		}

	}


}