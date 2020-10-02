using UMGS.WayPointSystem;
using UnityEngine;
using TMPro;

namespace UMGS.Vehicle
{


	[RequireComponent(typeof(VehicleController))]
	public class AIControlInput : ControlInput, IPositionStats
	{

		[SerializeField]                         WayPointManager path;
		[SerializeField] [Range(0, 1)]   private float           m_CautiousSpeedFactor           = 0.05f; // percentage of max speed to use when being maximally cautious
		[SerializeField] [Range(0, 180)] private float           m_CautiousMaxAngle              = 50f;
		[SerializeField]                 private float           m_SteerSensitivity              = 0.05f;
		[SerializeField]                 private float           m_AccelSensitivity              = 0.04f;
		[SerializeField]                 private float           m_CautiousAngularVelocityFactor = 30f;

		[SerializeField]                 float    MaxSpeed;
		[SerializeField] [Range(0f, 1f)] float    OffsetX;
		[SerializeField]                 Sensor[] Sensors;
		public                           WayPoint currentWayPoint;
		//Position Stats
		public int Id         { get; set; }
		public int PositionNo { get; set; }
		public float CoveredDistance
		{
			get => CoveredDistanceCal();
			set { ; }
		}
		float           waypointdistance = 0;
		VehicleController   Controller;
		Rigidbody       _rigidbody => Controller.attachedRigidbody;
		private Vector3 offsetTargetPos;
		[SerializeField] TextMeshPro text;
		void Start()
		{
			Controller         = GetComponent<VehicleController>();
			currentWayPoint = path.head;
			offsetTargetPos = currentWayPoint.GetPosition(OffsetX);
			_rigidbody.OnTriggerEnter((col) =>
			{
				if (col.CompareTag("Respawn"))
				{
					ResetPosition();
				}
			});
		}

		public override void DoUpdate(float speed)
		{
			if (currentWayPoint.nextWayPoint == null)
			{
				Stop();
				return;
			}

			brake = 0;
			CheckWaypointDistance();
			Vector3 fwd = transform.forward;
			//Accelaration Calculations
			float desiredSpeed           = MaxSpeed;
			float approachingCornerAngle = Vector3.Angle(currentWayPoint.transform.forward, fwd);
			float spinningAngle          = _rigidbody.angularVelocity.magnitude * m_CautiousAngularVelocityFactor;
			float cautiousnessRequired   = Mathf.InverseLerp(0, m_CautiousMaxAngle, Mathf.Max(spinningAngle, approachingCornerAngle));
			desiredSpeed = Mathf.Lerp(MaxSpeed, MaxSpeed * m_CautiousSpeedFactor, cautiousnessRequired);
			float accel = Mathf.Clamp((desiredSpeed - speed) * m_AccelSensitivity, -1, 1);
			//Steer Calculation
			Vector3 localTarget = transform.InverseTransformPoint(offsetTargetPos);
			float   targetAngle = Mathf.Atan2(localTarget.x, localTarget.z)            * Mathf.Rad2Deg;
			float   steer       = Mathf.Clamp(targetAngle * m_SteerSensitivity, -1, 1) * Mathf.Sign(speed);
			accel = SensorsCalculations(accel, ref steer);
			//Assigning result values
			throttle = Mathf.Clamp(accel, -1, 1);
			turn     = Mathf.Clamp(steer, -1, 1);
			CollisionStayReset();
			text.text = PositionNo.ToString();
		}

		float SensorsCalculations(float accel, ref float steer)
		{
			//front two sensors for braking
			brake += Sensors[7].Evaluate();
			brake += Sensors[6].Evaluate();
			if (brake < 0.1f)
			{
				//back tow sensors for accel
				accel += Sensors[4].Evaluate();
				accel += Sensors[5].Evaluate();
			}

			//RightSensors for steer
			steer += -Sensors[0].Evaluate();
			steer += Sensors[1].Evaluate();
			//Left Sensors
			steer += Sensors[2].Evaluate();
			steer += -Sensors[3].Evaluate();
			return accel;
		}

		float MiniMumDistance;

		void CheckWaypointDistance()
		{
			var distance                                    = Vector3.Distance(transform.position, offsetTargetPos);
			if (MiniMumDistance > distance) MiniMumDistance = distance;
			if (MiniMumDistance < distance && distance < 10f || distance < 4f)
			{
				UpdateNode();
			}
		}

		void UpdateNode()
		{
			if (currentWayPoint.nextWayPoint)
				currentWayPoint = currentWayPoint.nextWayPoint;
			offsetTargetPos  =  currentWayPoint.GetPosition(OffsetX);
			waypointdistance += Vector3.Distance(currentWayPoint.GetPosition(), currentWayPoint.previousWayPoint.GetPosition());
			MiniMumDistance  =  Vector3.Distance(transform.position,            offsetTargetPos);
		}

		//Sensors
		void CollisionStayReset()
		{
			if (Controller.lastCollisionTime > 3)
			{
				//Reset
				transform.rotation = Quaternion.LookRotation((offsetTargetPos - transform.position).normalized, Vector3.up);
			}
		}

		void ResetPosition()
		{
			transform.SetPositionAndRotation(offsetTargetPos + Vector3.up * 3, Quaternion.LookRotation((currentWayPoint.nextWayPoint.GetPosition(OffsetX) - transform.position).normalized, Vector3.up));
		}


		float CoveredDistanceCal()
		{
			float distance = waypointdistance;
			if (currentWayPoint.previousWayPoint)
			{
				float c = Vector3.Distance(transform.position, currentWayPoint.GetPosition());
				// float p  = Vector3.Distance(transform.position,            currentWayPoint.previousWayPoint.GetPosition());
				float cp = Vector3.Distance(currentWayPoint.GetPosition(), currentWayPoint.previousWayPoint.GetPosition());
				// float n  = Vector3.Distance(transform.position,            currentWayPoint.nextWayPoint.GetPosition());
				//float cn = Vector3.Distance(currentWayPoint.GetPosition(), currentWayPoint.nextWayPoint.GetPosition());
				distance += cp - c;
			}
			return distance;
		}

	}


}