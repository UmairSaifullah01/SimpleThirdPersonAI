using UnityEngine;

[System.Serializable]
public class VehicleLights
{

	[SerializeField] private GameObject frontLights, redLights, whiteLights, yellowLightLeft, yellowLightRight;
	[SerializeField]         GameObject indicatorSound;
	[HideInInspector] public bool       leftIndicator = false, rightIndicator = false;

	public void DoUpdate(float throttleInput, float brakeInput, float handbrakeInput, float steerInput)
	{
		if (redLights   != null) redLights.SetActive(brakeInput              > 0 || handbrakeInput    > 0);
		if (whiteLights != null) whiteLights.SetActive(Mathf.Abs(brakeInput) < 0.01f && throttleInput < 0);
		if (yellowLightLeft != null)
		{
			yellowLightLeft.SetActive((leftIndicator || steerInput < 0) && (float) Mathf.Sin(Time.time * 6) > 0);
			if (yellowLightRight != null)
			{
				yellowLightRight.SetActive((rightIndicator || steerInput > 0) && (float) Mathf.Sin(Time.time * 6) > 0);
				if (indicatorSound != null) indicatorSound.SetActive(yellowLightLeft.activeSelf || yellowLightRight.activeSelf);
			}
		}
	}

	public void FrontLight(bool active)
	{
		frontLights.SetActive(active);
	}

}