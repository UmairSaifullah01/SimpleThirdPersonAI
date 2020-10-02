using UnityEngine;


namespace UMGS.Vehicle
{


	public class UserControlInput : ControlInput
	{

		[SerializeField] bool   IsAutoStart    = true;
		[SerializeField] string ThrottleInput  = "Vertical";
		[SerializeField] string TurnInput      = "Horizontal";
		[SerializeField] string HandBrakeInput = "Jump";
		[SerializeField] string DriftInput     = "Jump";

		float GetInput(string input)
		{
			return Input.GetAxis(input);
		}

		public override void DoUpdate(float speed)
		{
			run = IsAutoStart;
			if (!run)
			{
				Stop();
				return;
			}

			float throttleInput = 0.0f;
			float brakeInput    = 0.0f;
			turn      = Mathf.Clamp(GetInput(TurnInput), -1.0f, 1.0f);
			handbrake = Mathf.Clamp01(GetInput(HandBrakeInput));
			float forwardInput = Mathf.Clamp01(GetInput(ThrottleInput));
			float reverseInput = Mathf.Clamp01(-GetInput(ThrottleInput));
			float minSpeed     = 0.1f;
			float minInput     = 0.1f;
			if (speed > minSpeed)
			{
				throttleInput = forwardInput;
				brakeInput    = reverseInput;
			}
			else
			{
				if (reverseInput > minInput)
				{
					throttleInput = -reverseInput;
					brakeInput    = 0.0f;
				}
				else if (forwardInput > minInput)
				{
					if (speed < -minSpeed)
					{
						throttleInput = 0.0f;
						brakeInput    = forwardInput;
					}
					else
					{
						throttleInput = forwardInput;
						brakeInput    = 0;
					}
				}
			}

			throttle = throttleInput;
			brake    = brakeInput;
			Drift    = Mathf.Clamp01(GetInput(DriftInput));
		}

	}


}