using System.Collections;
using System.Collections.Generic;
using UMGS;
using UnityEngine;


namespace UMGS
{


	public class Transition : ITransition
	{

		public string toState   { get; }
		public bool   condition { get; set; }

		public Transition(string to)
		{
			this.toState = to;
		}

	}


}