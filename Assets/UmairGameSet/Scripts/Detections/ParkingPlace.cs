using UMGS;
using UnityEngine;
using UnityEngine.Events;

public class ParkingPlace : MonoBehaviour
{

	[Header("Parking Settings")] public string     requiredTag = "Player";
	public                              bool       oneSide     = false;
	public                              float      distanceLimit, angleLimit;
	[HideInInspector] public            Transform  target;
	[HideInInspector] public            bool       inArea   = false;
	private                             bool       isParked = false, isBehind = false;
	public                              Collider   senser;
	public                              UnityEvent OnParked;

	protected virtual void Update()
	{
		if (inArea && target && senser.transform.Distance(target.position) < distanceLimit)
		{
			Vector3 targetPosition = UMTools.SetVector3Axis(target.position, senser.transform.position.y, Axis.y);
			Vector3 dirToTarget    = (targetPosition - senser.transform.position).normalized;
			float   angle          = Mathf.Abs(Vector3.Angle(senser.transform.forward, dirToTarget) - ((Vector3.Dot(senser.transform.forward, dirToTarget) < 0) ? 180 : 0));
			if (angle < angleLimit && (!oneSide || Vector3.Dot(senser.transform.forward, target.forward) > 0))
			{
				if (!isParked)
					OnParked?.Invoke();
				isParked = true;
			}
		}
	}


	public virtual void Awake()
	{
		senser = GetComponent<Collider>();
		if (!senser)
		{
			senser = GetComponentInChildren<Collider>();
		}

		senser.OnTriggerEnter((other) =>
		{
			if (other.attachedRigidbody && other.attachedRigidbody.CompareTag(requiredTag))
			{
				inArea = true;
				target = other.attachedRigidbody.transform;
			}
			else if (other.transform.FindTagInParent(requiredTag, out Transform result))
			{
				inArea = true;
				target = result;
			}
		});
		senser.OnTriggerExit((other) =>
		{
			if (other.attachedRigidbody && other.attachedRigidbody.CompareTag(requiredTag))
			{
				inArea   = false;
				target   = null;
				isParked = false;
			}
			else if (other.transform.FindTagInParent(requiredTag))
			{
				inArea   = false;
				target   = null;
				isParked = false;
			}
		});
	}

}