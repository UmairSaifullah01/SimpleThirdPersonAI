using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UMGS
{


	public class TargetFollower
	{

		private Transform transform, target;
		Vector3           damping;

		public TargetFollower(Transform follower, Transform givenTarget)
		{
			target             = givenTarget;
			transform          = follower;
			damping            = target.position;
			damping.y          = transform.position.y;
			transform.position = damping;
		}

		public void DoUpdate()
		{
			var position = target.position - damping;
			position.y         =  0f;
			transform.position += position;
			damping            =  target.position;
			var angles = transform.eulerAngles;
			angles.y              = target.eulerAngles.y;
			transform.eulerAngles = angles;
		}

	}


}