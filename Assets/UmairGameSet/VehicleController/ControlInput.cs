using UnityEngine;


namespace UMGS.Vehicle
{


	[System.Serializable]
	public abstract class ControlInput : MonoBehaviour
	{

		public bool  run       { get; protected set; }
		public float throttle  { get; protected set; }
		public float brake     { get; protected set; }
		public float turn      { get; protected set; }
		public float handbrake { get; protected set; }
		public float Drift     { get; protected set; }


		public abstract void DoUpdate(float speed);


		public void Stop()
		{
			throttle  = 0;
			brake     = 0;
			turn      = 0;
			handbrake = 1;
			Drift     = 0;
		}

	}


}