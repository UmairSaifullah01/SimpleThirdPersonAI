using System;
using UnityEngine;


namespace UMGS.Vehicle
{


	[UClassHeader("Vehicle Controller")]
	public class VehicleController : UBehaviour
	{

		// SerializeFields
		[UToolbar("Engine")] [SerializeField]   AnimationCurve motorTorque    = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));
		[SerializeField]                        AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);
		[Range(2,    16)] [SerializeField]      float          diffGearing    = 4.0f;
		[Range(0.5f, 15f)] [SerializeField]     float          downforce      = 1.0f;
		[SerializeField]                        float          antiRoll;
		[SerializeField]                        float          maxSpeed       = 80.0f;
		[SerializeField]                        float          maxAngularDrag = 5f;
		[SerializeField]                        float          brakeForce     = 1500.0f;
		[Range(0f,     50.0f)] [SerializeField] float          steerAngle     = 30.0f;
		[Range(0.001f, 1.0f)] [SerializeField]  float          steerSpeed     = 0.2f;
		[SerializeField]                        bool           isDriftActive;
		[SerializeField] [Range(0.0f, 2f)]      float          driftIntensity;

		[SerializeField]                                            Transform centerOfMass;
		[UToolbar("Wheels")] [SerializeField]                       Wheel[]   wheels;
		[Header("Easy Suspension")] [Range(0, 20)] [SerializeField] float     naturalFrequency = 10;

		[Range(0, 3)] [SerializeField] float dampingRatio = 0.8f;

		[Range(-1, 1)] [SerializeField] float forceShift = 0.03f;

		public                                  bool             setSuspensionDistance = true;
		[UToolbar("Audio")] [SerializeField]    VehicleAudio     sounds;
		[UToolbar("Lights")] [SerializeField]   VehicleLights    Lights;
		[UToolbar("Effects")] [SerializeField]  ParticleSystem[] gasParticles;
		[UToolbar("Controls")] [SerializeField] ControlInput     ControlInput;
		// Public Area 
		public float speed          => transform.InverseTransformDirection(attachedRigidbody.velocity).z * 3.6f;
		public float directionSpeed => Vector3.Dot(attachedRigidbody.velocity, transform.forward);
		public bool IsGrounded
		{
			get
			{
				isGrounded = true;
				foreach (Wheel wheel in wheels)
				{
					if (Physics.Raycast(wheel.WheelCollider.transform.position, -wheel.WheelCollider.transform.up, 2))
					{
						isGrounded = true;
						return isGrounded;
					}
				}

				isGrounded = false;
				return isGrounded;
			}
		}
		[HideInInspector] public Rigidbody attachedRigidbody;
		// public bool IsGroundedAllWheels
		// {
		// 	get
		// 	{
		// 		foreach (Wheel wheel in wheels)
		// 		{
		// 			if (wheel.Drive && (!wheel.WheelCollider.gameObject.activeSelf || !wheel.WheelCollider.isGrounded))
		// 				isGrounded = false;
		// 		}
		//
		// 		return true;
		// 	}
		// }
		public float lastCollisionTime { get; private set; }

		//}
		//private 

		bool    isGrounded      = false;
		int     lastGroundCheck = 0;
		bool    isColliding     = false;
		float[] travelValues    = new float[4];
		float   desireSpeed;


		void Start()
		{
			attachedRigidbody            = GetComponent<Rigidbody>();
			attachedRigidbody.useGravity = false;
			if (attachedRigidbody != null && centerOfMass != null)
			{
				attachedRigidbody.centerOfMass = centerOfMass.localPosition;
			}

//			attachedRigidbody.maxDepenetrationVelocity = 8.0f;
			sounds.Initialize(ControlInput);
		}

		void Update()
		{
			foreach (ParticleSystem gasParticle in gasParticles)
			{
				gasParticle.Play();
				ParticleSystem.EmissionModule em = gasParticle.emission;
				em.rateOverTime = ControlInput.handbrake > 0.5f ? 0 : Mathf.Lerp(em.rateOverTime.constant, Mathf.Clamp(150.0f * ControlInput.throttle, 30.0f, 100.0f), 0.1f);
			}

			if (isColliding) lastCollisionTime += Time.deltaTime;
			sounds.DoUpdate(speed);
		}

		void FixedUpdate()
		{
			ControlInput.DoUpdate(directionSpeed);
			Lights.DoUpdate(ControlInput.throttle, ControlInput.brake, ControlInput.handbrake > 0.5f ? 1 : 0, ControlInput.turn);
			float steerValue = ComputeSteerAngle();
			foreach (Wheel wheel in wheels)
			{
				if (wheel.Steer) wheel.WheelCollider.steerAngle = steerValue;
				if (ControlInput.handbrake > 0.5f)
				{
					// Don't zero out this value or the wheel completely lock up
					wheel.WheelCollider.motorTorque = 0.0001f;
					wheel.WheelCollider.brakeTorque = brakeForce;
				}
				else if (wheel.Drive && ControlInput.brake < 0.5f)
				{
					wheel.WheelCollider.motorTorque = EngineCalculations(); //Remember to count drive wheels
				}

				wheel.WheelCollider.brakeTorque = brakeForce * ControlInput.brake;
				VisualWheelsUpdate(wheel);
				//Suspension Calculations 
				EasySuspension(wheel.WheelCollider);
			}

			if (ControlInput.brake > 0.5f)
			{
				desireSpeed = Mathf.MoveTowards(desireSpeed, speed > maxSpeed / 2 ? maxSpeed / 2 : 0, Time.deltaTime * speed);
			}
			else
			{
				desireSpeed = maxSpeed;
			}

			//max Speed Counter
			attachedRigidbody.velocity = attachedRigidbody.velocity.normalized * Mathf.Clamp(attachedRigidbody.velocity.magnitude, 0, desireSpeed);
			//MaxAngular speed
			attachedRigidbody.angularDrag = Mathf.Lerp(0.01f, maxAngularDrag, attachedRigidbody.velocity.magnitude / maxSpeed);
			ApplyAntiRoll();
			//Grounded Checking....
			if (!IsGrounded)
			{
				//Direction changer force and rotation
				attachedRigidbody.AddForce(Vector3.Cross(UMTools.SetVector3Axis(attachedRigidbody.velocity, 0, Axis.y), Vector3.up) * (-ControlInput.turn * 500));
				transform.Rotate(0, 0, Time.deltaTime * 0.1f                                                                        * -ControlInput.turn);
				//Simple Gravity
				attachedRigidbody.AddForce(Physics.gravity * attachedRigidbody.mass);
				attachedRigidbody.angularVelocity = Vector3.zero;
				//Downforce
				DownForceImplementation();
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z), 10 * Time.deltaTime);
			}
			else
			{
				//Simple Gravity
				if (speed < 5f && transform.eulerAngles.z > 90) attachedRigidbody.AddForce(Physics.gravity * attachedRigidbody.mass);
				else //Stick Gravity
					attachedRigidbody.AddForce(-transform.up * (Physics.gravity.magnitude * attachedRigidbody.mass));
			}

			if (ControlInput.Drift > 0.5f && isDriftActive)
			{
				Vector3 driftForce = -transform.right;
				driftForce.y = 0.0f;
				driftForce.Normalize();
				if (Math.Abs(ControlInput.turn) > 0.01f)
					driftForce *= attachedRigidbody.mass * speed / 7f * ControlInput.throttle * ControlInput.turn / steerAngle;
				Vector3 driftTorque = transform.up * (0.1f * ControlInput.turn) / steerAngle;
				attachedRigidbody.AddForce(driftForce                           * driftIntensity, ForceMode.Force);
				attachedRigidbody.AddTorque(driftTorque                         * driftIntensity, ForceMode.VelocityChange);
				print("Drift");
			}
		}

		float EngineCalculations()
		{
			return ControlInput.throttle * motorTorque.Evaluate(speed) * diffGearing / 4;
		}

		void DownForceImplementation()
		{
			attachedRigidbody.AddForce(-transform.up * (speed * downforce * 10));
		}


		void ApplyAntiRoll()
		{
			WheelHit hit;
			for (int i = 0; i < wheels.Length; i++)
			{
				isGrounded      = wheels[i].WheelCollider.GetGroundHit(out hit);
				travelValues[i] = isGrounded ? (-wheels[i].WheelCollider.transform.InverseTransformPoint(hit.point).y - wheels[i].WheelCollider.radius) / wheels[i].WheelCollider.suspensionDistance : 1;
			}

			float antiRollForceFrontHorizontal = (travelValues[0] - travelValues[1]) * antiRoll;
			if (wheels[0].WheelCollider.isGrounded)
				attachedRigidbody.AddForceAtPosition(wheels[0].WheelCollider.transform.up * -antiRollForceFrontHorizontal, wheels[0].WheelCollider.transform.position);
			if (wheels[1].WheelCollider.isGrounded) attachedRigidbody.AddForceAtPosition(wheels[1].WheelCollider.transform.up * -antiRollForceFrontHorizontal, wheels[1].WheelCollider.transform.position);
			antiRollForceFrontHorizontal = (travelValues[2] - travelValues[3]) * antiRoll;
			if (wheels[2].WheelCollider.isGrounded) attachedRigidbody.AddForceAtPosition(wheels[2].WheelCollider.transform.up * -antiRollForceFrontHorizontal, wheels[2].WheelCollider.transform.position);
			if (wheels[3].WheelCollider.isGrounded) attachedRigidbody.AddForceAtPosition(wheels[3].WheelCollider.transform.up * -antiRollForceFrontHorizontal, wheels[3].WheelCollider.transform.position);
			float antiRollForceFrontVertical = (travelValues[0] - travelValues[2]) * antiRoll;
			if (wheels[0].WheelCollider.isGrounded) attachedRigidbody.AddForceAtPosition(wheels[0].WheelCollider.transform.up * -antiRollForceFrontVertical, wheels[0].WheelCollider.transform.position);
			if (wheels[2].WheelCollider.isGrounded) attachedRigidbody.AddForceAtPosition(wheels[2].WheelCollider.transform.up * -antiRollForceFrontVertical, wheels[2].WheelCollider.transform.position);
			antiRollForceFrontVertical = (travelValues[1] - travelValues[3]) * antiRoll;
			if (wheels[1].WheelCollider.isGrounded) attachedRigidbody.AddForceAtPosition(wheels[1].WheelCollider.transform.up * -antiRollForceFrontVertical, wheels[1].WheelCollider.transform.position);
			if (wheels[3].WheelCollider.isGrounded) attachedRigidbody.AddForceAtPosition(wheels[3].WheelCollider.transform.up * -antiRollForceFrontVertical, wheels[3].WheelCollider.transform.position);
		}

		void VisualWheelsUpdate(Wheel wheel)
		{
			wheel.WheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
			wheel.VisualWheel.rotation      =  rot;
			wheel.VisualWheel.localRotation *= Quaternion.Euler(wheel.LocalRotOffset);
			wheel.VisualWheel.position      =  pos;
		}

		void EasySuspension(WheelCollider wc)
		{
			JointSpring spring = wc.suspensionSpring;
			spring.spring       = Mathf.Pow(Mathf.Sqrt(wc.sprungMass) * naturalFrequency, 2);
			spring.damper       = 2 * dampingRatio * Mathf.Sqrt(spring.spring * wc.sprungMass);
			wc.suspensionSpring = spring;
			Vector3 wheelRelativeBody = transform.InverseTransformPoint(wc.transform.position);
			float   distance          = attachedRigidbody.centerOfMass.y - wheelRelativeBody.y + wc.radius;
			wc.forceAppPointDistance = distance - forceShift;

			// the following line makes sure the spring force at maximum droop is exactly zero
			if (spring.targetPosition > 0 && setSuspensionDistance)
				wc.suspensionDistance = wc.sprungMass * Physics.gravity.magnitude / (spring.targetPosition * spring.spring);
		}

		float ComputeSteerAngle()
		{
			float inputSteerAngle = steerAngle * turnInputCurve.Evaluate(ControlInput.turn);
			float speedFactor     = Mathf.Lerp(0.01f, 1.0f, 1 - (attachedRigidbody.velocity.magnitude / maxSpeed / 2));
			inputSteerAngle *= speedFactor;
			float assistedSteerAngle = steerAngle * 0.6f * (speedFactor) * Mathf.InverseLerp(1.0f, 3.0f, Mathf.Abs(steerSpeed));
			return Mathf.Clamp(Mathf.Lerp(0, inputSteerAngle + assistedSteerAngle, Time.deltaTime * 100 * steerSpeed), -steerAngle, +steerAngle);
		}

		public void OnSkid(Vector3 skidPoint, float intensity)
		{
			sounds.SkidAudio(skidPoint, intensity);
		}

		void OnCollisionEnter(Collision other)
		{
			isColliding       = true;
			lastCollisionTime = 0;
			sounds.ImpactAudio(other.contacts[0].point);
			attachedRigidbody.angularVelocity = Vector3.zero;
		}

		void OnCollisionExit(Collision other)
		{
			isColliding = false;
		}

	}

	[System.Serializable]
	public struct Wheel
	{

		public WheelCollider WheelCollider;
		public Transform     VisualWheel;
		public Vector3       LocalRotOffset;
		public bool          Drive, Steer;

	}


}