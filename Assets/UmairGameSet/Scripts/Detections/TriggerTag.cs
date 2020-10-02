using System;
using UnityEngine;
using UnityEngine.Events;


namespace UMGS
{


	public class TriggerTag : UJTrigger
	{

		public string     TargetTag;
		public UnityEvent OnTriggerEnter;

		void OnEnable()
		{
			enter += Enter;
		}

		void OnDisable()
		{
			enter -= Enter;
		}

		void Enter(Collider col)
		{
			if (col.attachedRigidbody.CompareTag(TargetTag)) OnTriggerEnter?.Invoke();
		}

	}


}