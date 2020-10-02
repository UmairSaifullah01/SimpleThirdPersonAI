using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine.AI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace UMGS
{


	public static class UMTools
	{

		#region GameObject

		public static void SetActiveOther(this GameObject _obj, GameObject obj)
		{
			_obj.SetActive(false);
			obj.SetActive(true);
		}

		public static void SetActiveFalse(this GameObject _obj)
		{
			_obj.SetActive(false);
		}

		public static void SetActiveTrue(this GameObject _obj)
		{
			_obj.SetActive(true);
		}

		public static void SetActiveChildren(this GameObject gameObject, bool value)
		{
			foreach (Transform child in gameObject.transform)
				child.gameObject.SetActive(value);
		}

		public static void SetLayerRecursively(this GameObject obj, int layer)
		{
			obj.layer = layer;
			foreach (Transform child in obj.transform)
			{
				child.gameObject.SetLayerRecursively(layer);
			}
		}

		#endregion


		#region Transfrom

		public static bool FindTagInParent(this Transform t, string tagString, out Transform other)
		{
			if (t.CompareTag(tagString))
			{
				other = t.transform;
				return true;
			}

			if (t.parent == null) return FindTagInParent(t.parent, tagString, out other);
			other = null;
			return false;
		}

		public static bool FindTagInParent(this Transform t, string tagString)
		{
			return t.CompareTag(tagString) || (t.parent != null && FindTagInParent(t.parent, tagString));
		}

		public static void SetPosition(this Transform from, float value, Axis axis = Axis.x)
		{
			Vector3 position = from.position;
			if (axis == Axis.x)
				position.x = value;
			else if (axis == Axis.y)
				position.y = value;
			else
				position.z = value;
			from.position = position;
		}

		public static void SetPosition(this Transform trans, Vector3 position, Axis ignoreAxis = Axis.x)
		{
			if (ignoreAxis == Axis.x)
				position.x = trans.position.x;
			else if (ignoreAxis == Axis.y)
				position.y = trans.position.y;
			else
				position.z = trans.position.z;
			trans.position = position;
		}

		public static void SetPosition(this Transform from, Transform target)
		{
			from.position = target.position;
			from.rotation = target.rotation;
		}

		public static void ResetTransformation(this Transform trans)
		{
			trans.position   = Vector3.zero;
			trans.rotation   = Quaternion.identity;
			trans.localScale = new Vector3(1, 1, 1);
		}

		public static void ResetLocalTransformation(this Transform trans)
		{
			trans.localPosition = Vector3.zero;
			trans.localRotation = Quaternion.identity;
			trans.localScale    = new Vector3(1, 1, 1);
		}

		public static void ResetTransformPosition(this Transform trans)
		{
			trans.position = Vector3.zero;
		}

		public static void ResetTransformScale(this Transform trans)
		{
			trans.localScale = new Vector3(1, 1, 1);
		}

		public static void ResetLocalScaleAndPosition(this Transform trans)
		{
			trans.localPosition = Vector3.zero;
			trans.localScale    = new Vector3(1, 1, 1);
		}

		public static void DestroyAllChildren(this Transform trans)
		{
			foreach (Transform child in trans)
			{
				Object.Destroy(child.gameObject);
			}
		}

		public static bool NotNull<T>(this T trans)
		{
			return trans != null;
		}

		/// <summary>
		/// Minimum the distance form array.
		/// </summary>
		public static int NearestTransformIndex(this Transform trans, Transform[] givenPoints)
		{
			return NearestTransformIndex(trans, givenPoints, out Transform temp);
		}

		public static int NearestTransformIndex(this Transform trans, Transform[] givenPoints, out Transform point)
		{
			float distance = Vector3.Distance(givenPoints[0].position, trans.position);
			int   index    = 0;
			for (int i = 0; i < givenPoints.Length; i++)
			{
				var tempDistance = Vector3.Distance(givenPoints[i].position, trans.position);
				if (tempDistance < distance)
				{
					index    = i;
					distance = tempDistance;
				}
			}

			point = givenPoints[index];
			return index;
		}

		/// <summary>
		///Sort list by distance
		/// get center position trnasform and sort list
		/// </summary>
		public static List<Transform> SortByDistance(Transform trans, List<Transform> givenPoints)
		{
			givenPoints.Sort((a, b) => Vector3.Distance(trans.position, a.position).CompareTo(Vector3.Distance(trans.position, b.position)));
			return givenPoints;
		}

		public static float Distance(this Transform trans, Transform target)
		{
			return Distance(trans.position, target.position);
		}

		public static float Distance(this Transform trans, Vector3 targetPosition)
		{
			return Distance(trans.position, targetPosition);
		}

		public static bool FieldOfView(this Transform trans, Transform target, float angle)
		{
			Vector3 dirToTarget = (target.position - trans.position).normalized;
			return Vector3.Angle(trans.forward, dirToTarget) < angle / 2;
		}

		public static bool Sensor(this Transform from, float radius, LayerMask layerMask, string tag)
		{
			Vector3 origin = from.position;
			return Sensor(origin, radius, layerMask, tag);
		}

		public static LocationTransform GetTransformValues(this Transform trans, bool isLocal = false)
		{
			return new LocationTransform {Position = isLocal ? trans.localPosition : trans.position, Rotation = isLocal ? trans.localRotation : trans.rotation};
		}

		public static void SetTransformValues(this Transform trans, LocationTransform values, bool isLocal = false)
		{
			if (isLocal)
			{
				trans.localPosition = values.Position;
				trans.localRotation = values.Rotation;
			}
			else
			{
				trans.position = values.Position;
				trans.rotation = values.Rotation;
			}
		}

		public static void SetTransformValues(this Transform trans, Transform values, bool isLocal = false)
		{
			if (isLocal)
			{
				trans.localPosition    = values.localPosition;
				trans.localEulerAngles = values.localEulerAngles;
			}
			else
			{
				trans.position    = values.position;
				trans.eulerAngles = values.eulerAngles;
			}
		}

		#endregion


		#region Vecters

		public static Vector3 SetVector3Axis(this Vector3 pos, float value, Axis axis = Axis.x)
		{
			if (axis == Axis.x)
				pos.x = value;
			else if (axis == Axis.y)
				pos.y = value;
			else
				pos.z = value;
			return pos;
		}

		public static Vector3 RandomPointInCircle(Vector3 orign, float radius, Axis aroundAxis = Axis.x)
		{
			switch (aroundAxis)
			{
				case Axis.x:
					return new Vector3(orign.x, orign.y + Random.Range(0.0f, radius), orign.z + Random.Range(0.0f, radius));

				case Axis.y:
					return new Vector3(orign.x + Random.Range(0.0f, radius), orign.y, orign.z + Random.Range(0.0f, radius));

				case Axis.z:
					return new Vector3(orign.x + Random.Range(0.0f, radius), orign.y + Random.Range(0.0f, radius), orign.z);

				default:
					return new Vector3(orign.x + Random.Range(0.0f, radius), orign.y + Random.Range(0.0f, radius), orign.z);
			}
		}

		public static float Distance(this Vector3 pos, Vector3 targetPosition)
		{
			return Vector3.Distance(pos, targetPosition);
		}

		public static Vector3 Difference(this Vector3 vector, Vector3 otherVector)
		{
			return otherVector - vector;
		}

		/// <summary>
		/// Line Point form Equation of Line.
		/// Slope Formula for equation of Line y=mx+b , m=y-y1 / x-x1, b=y-mx m=tan0
		/// </summary>
		public static Vector3 LinePoint(Vector2 first, Vector2 second, float y, float z)
		{
			Vector3 vect      = Vector3.zero;
			float   changeInX = (second.x - first.x);
			float   changeInY = (second.y - first.y);
			if (changeInX == 0)
				changeInX = 1;
			float m = changeInY / changeInX;
			if (m == 0)
				m = 1;
			float b = second.y - (second.x) * m;
			vect.y = y;
			vect.z = z;
			vect.x = (z - b) / m;
			return vect;
		}

		public static Vector3 DirFromAngle(float angleInDegrees)
		{
			return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
		}

		public static Vector3 DirFromAngle(float angleInDegrees, Axis axis)
		{
			if (axis == Axis.x)
				return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, 0);
			else if (axis == Axis.y)
				return new Vector3(0, Mathf.Sin(angleInDegrees * Mathf.Deg2Rad) / Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
			else
				return new Vector3(0, 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
		}

		public static bool IsExistInBoundary(Vector3 center, float width, float height, Vector3 target)
		{
			Vector3 corner = new Vector3() {x = center.x - (width / 2), z = center.x - (height / 2)};
			if (target.x < corner.x + width && target.x > corner.x && target.z < corner.z + width && target.z > corner.z)
				return true;
			return false;
		}

		#endregion


		#region RigidBody

		public static Collider[] AttachedCollider(this Rigidbody rigidbody)
		{
			var col         = rigidbody.GetComponent<Collider>();
			var subCollider = rigidbody.GetComponentsInChildren<Collider>();
			if (col)
			{
				subCollider.Append(col);
			}

			return subCollider;
		}

		#endregion


		#region Physics

		static Collider[] cols;

		public static bool Sensor(Vector3 origin, float radius, LayerMask layerMask, string tag)
		{
			var size = Physics.OverlapSphereNonAlloc(origin, radius, cols, layerMask);
			if (size > 0)
			{
				foreach (var col in cols)
				{
					if (col.transform.parent.CompareTag(tag))
						return true;
					else
						return false;
				}
			}

			return false;
		}

		public static Vector3 BoxSize(this BoxCollider boxCollider)
		{
			var length = boxCollider.transform.lossyScale.x * boxCollider.size.x;
			var width  = boxCollider.transform.lossyScale.z * boxCollider.size.z;
			var height = boxCollider.transform.lossyScale.y * boxCollider.size.y;
			return new Vector3(length, height, width);
		}

		#endregion


		#region Navmesh

		public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
		{
			// Get Random Point inside Sphere which position is center, radius is maxDistance
			Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;
			return GetPointOnNavMesh(randomPos, maxDistance);
		}

		public static Vector3 GetPointOnNavMesh(Vector3 from, float distance = 100)
		{
			return NavMesh.SamplePosition(from, out NavMeshHit myNavHit, distance, NavMesh.AllAreas) ? myNavHit.position : GetPointOnNavMesh(from, distance + 100);
		}

		#endregion


		#region Camera

		public static Bounds OrthographicBounds(this Camera camera)
		{
			float  screenAspect = (float) Screen.width    / (float) Screen.height;
			float  cameraHeight = camera.orthographicSize * 2;
			Bounds bounds       = new Bounds(camera.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
			return bounds;
		}

		#endregion


		#region Component

		/// <summary>
		/// Attaches a component to the given component's game object.
		/// </summary>
		/// <param name="component">Component.</param>
		/// <returns>Newly attached component.</returns>
		public static T AddComponent<T>(this Component component) where T : Component
		{
			return component.gameObject.AddComponent<T>();
		}

		/// <summary>
		/// Gets a component attached to the given component's game object.
		/// If one isn't found, a new one is attached and returned.
		/// </summary>
		/// <param name="component">Component.</param>
		/// <returns>Previously or newly attached component.</returns>
		public static T GetOrAddComponent<T>(this Component component) where T : Component
		{
			return component.GetComponent<T>() ?? component.AddComponent<T>();
		}

		/// <summary>
		/// Checks whether a component's game object has a component of type T attached.
		/// </summary>
		/// <param name="component">Component.</param>
		/// <returns>True when component is attached.</returns>
		public static bool HasComponent<T>(this Component component) where T : Component
		{
			return component.GetComponent<T>() != null;
		}

		#endregion


		#region Array

		public static T[] Append<T>(this T[] arrayInitial, T[] arrayToAppend)
		{
			if (arrayToAppend == null)
			{
				throw new ArgumentNullException("The appended object cannot be null");
			}

			if ((arrayInitial is string) || (arrayToAppend is string))
			{
				throw new ArgumentException("The argument must be an enumerable");
			}

			T[] ret = new T[arrayInitial.Length + arrayToAppend.Length];
			arrayInitial.CopyTo(ret, 0);
			arrayToAppend.CopyTo(ret, arrayInitial.Length);
			return ret;
		}

		public static T[] ToArray<T>(this List<T> list)
		{
			T[] array = new T[list.Count];
			list.CopyTo(array);
			return array;
		}

		public static List<T> ToList<T>(this T[] array)
		{
			var list = new List<T>();
			list.AddRange(array);
			return list;
		}

		#endregion


		#region Dictionary

		public static T[] GetKeys<T, T2>(this Dictionary<T, T2> dict)
		{
			var array = new T[dict.Count];
			dict.Keys.CopyTo(array, 0);
			return array;
		}

		#endregion


		#region Enum

		public static bool Contains(this Enum keys, Enum flag)
		{
			if (keys.GetType() != flag.GetType())
				throw new ArgumentException("Type Mismatch");
			return (Convert.ToUInt64(keys) & Convert.ToUInt64(flag)) != 0;
		}

		#endregion


		#region Lerp

		public static float FastLerp(float from, float to, float t)
		{
			return from + (to - from) * t;
		}


		public static float LinearLerp(float x0, float y0, float x1, float y1, float x)
		{
			return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
		}


		public static float LinearLerp(Vector2 from, Vector2 to, float t)
		{
			return LinearLerp(from.x, from.y, to.x, to.y, t);
		}


		public static float CubicLerp(float x0, float y0, float x1, float y1, float x)
		{
			// Hermite-based cubic polinomial function (spline) with horizontal tangents (0)
			//
			// h1(t) =  2*t3 - 3*t2 + 1;	-> start point
			// h2(t) = -2*t3 + 3*t2;		-> end point
			float t  = (x - x0) / (x1 - x0);
			float t2 = t        * t;
			float t3 = t        * t2;
			return y0 * (2 * t3 - 3 * t2 + 1) + y1 * (-2 * t3 + 3 * t2);
		}


		public static float CubicLerp(Vector2 from, Vector2 to, float t)
		{
			return CubicLerp(from.x, from.y, to.x, to.y, t);
		}


		// Smooth interpolation with simplified tangent adjustment


		public static float TangentLerp(float x0, float y0, float x1, float y1, float a, float b, float x)
		{
			float h   = y1 - y0;
			float tg0 = 3.0f * h * a;
			float tg1 = 3.0f * h * b;

			// Hermite-based cubic polinomial function (spline)
			//
			// h1(t) =  2*t3 - 3*t2 + 1;	-> start point
			// h2(t) = -2*t3 + 3*t2;		-> end point
			// h3(t) =    t3 - 2*t2 + t;	-> start tangent
			// h4(t) =    t3 - t2;			-> end tangent
			float t  = (x - x0) / (x1 - x0);
			float t2 = t        * t;
			float t3 = t        * t2;
			return y0 * (2 * t3 - 3 * t2 + 1) + y1 * (-2 * t3 + 3 * t2) + tg0 * (t3 - 2 * t2 + t) + tg1 * (t3 - t2);
		}


		public static float TangentLerp(Vector2 from, Vector2 to, float a, float b, float t)
		{
			return TangentLerp(from.x, from.y, to.x, to.y, a, b, t);
		}


		// Hermite interpolation with full control on tangents


		public static float HermiteLerp(float x0, float y0, float x1, float y1, float outTangent, float inTangent, float x)
		{
			// Hermite-based cubic polinomial function (spline)
			//
			// h1(t) =  2*t3 - 3*t2 + 1;	-> start point
			// h2(t) = -2*t3 + 3*t2;		-> end point
			// h3(t) =    t3 - 2*t2 + t;	-> start tangent
			// h4(t) =    t3 - t2;			-> end tangent
			float t  = (x - x0) / (x1 - x0);
			float t2 = t        * t;
			float t3 = t        * t2;
			return y0 * (2 * t3 - 3 * t2 + 1) + y1 * (-2 * t3 + 3 * t2) + outTangent * (t3 - 2 * t2 + t) + inTangent * (t3 - t2);
		}


		// Generic biased lerp with optional context optimization:
		//
		// 	BiasedLerp(x, bias)				generic unoptimized
		//	BiasedLerp(x, bias, context)	optimized for bias which changes unfrequently


		public class BiasLerpContext
		{

			public float lastBias     = -1.0f;
			public float lastExponent = 0.0f;

		}


		static float BiasWithContext(float x, float bias, BiasLerpContext context)
		{
			if (x <= 0.0f)
				return 0.0f;
			if (x >= 1.0f)
				return 1.0f;
			if (bias != context.lastBias)
			{
				if (bias <= 0.0f)
					return x >= 1.0f ? 1.0f : 0.0f;
				else if (bias >= 1.0f)
					return x > 0.0f ? 1.0f : 0.0f;
				else if (bias == 0.5f)
					return x;
				context.lastExponent = Mathf.Log(bias) * -1.4427f;
				context.lastBias     = bias;
			}

			return Mathf.Pow(x, context.lastExponent);
		}


		static float BiasRaw(float x, float bias)
		{
			if (x <= 0.0f)
				return 0.0f;
			if (x >= 1.0f)
				return 1.0f;
			if (bias <= 0.0f)
				return x >= 1.0f ? 1.0f : 0.0f;
			else if (bias >= 1.0f)
				return x > 0.0f ? 1.0f : 0.0f;
			else if (bias == 0.5f)
				return x;
			float exponent = Mathf.Log(bias) * -1.4427f;
			return Mathf.Pow(x, exponent);
		}


		public static float BiasedLerp(float x, float bias)
		{
			float result = bias <= 0.5f ? BiasRaw(Mathf.Abs(x), bias) : 1.0f - BiasRaw(1.0f - Mathf.Abs(x), 1.0f - bias);
			return x < 0.0f ? -result : result;
		}


		public static float BiasedLerp(float x, float bias, BiasLerpContext context)
		{
			float result = bias <= 0.5f ? BiasWithContext(Mathf.Abs(x), bias, context) : 1.0f - BiasWithContext(1.0f - Mathf.Abs(x), 1.0f - bias, context);
			return x < 0.0f ? -result : result;
		}

		#endregion


		#region Others

		public static int RandomWithExclusiveValue(int min, int max, int value)
		{
			if (min == max || Mathf.Abs(min - max) < 2 || min == value) return 0;
			var r = 0;
			do
			{
				r = Random.Range(min, max);
			} while (r == value);

			return r;
		}

		public static int ToInt(this float obj)
		{
			return System.Convert.ToInt32(obj);
		}

		public static float RateOf(float amount)
		{
			return (1 / amount);
		}

		public static bool Probability(int chancesPercentage)
		{
			if (Random.Range(0, Mathf.RoundToInt(100 / chancesPercentage)) == 0)
			{
				return true;
			}

			return false;
		}

		public static float ClampAngle(float angle, float min, float max)
		{
			do
			{
				if (angle < -360)
					angle += 360;
				if (angle > 360)
					angle -= 360;
			} while (angle < -360 || angle > 360);

			return Mathf.Clamp(angle, min, max);
		}

// Returns an angle valued clamped as [-180 .. +180]
		public static float ClampAngle(float angle)
		{
			angle %= 360.0f;
			if (angle > 180.0f)
				angle -= 360.0f;
			return angle;
		}
		// Returns an angle valued clamped as [0 .. +360] suitable for Mathf.LerpAngle

		public static float ClampAngle360(float angle)
		{
			angle = angle % 360.0f;
			if (angle < 0.0f)
				angle += 360.0f;
			return angle;
		}

		public static Color RandomColor()
		{
			return Random.ColorHSV();
			//return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		}

		public static void NetworkRequiredSpeed(this MonoBehaviour behaviour, float requiredSpeed, Action onSpeedAchieved)
		{
			behaviour.StartCoroutine(CheckNetSpeed(requiredSpeed, onSpeedAchieved));
		}

		private static IEnumerator CheckNetSpeed(float requiredSpeed, Action onSpeedAchieved)
		{
			Ping p = new Ping("172.217.9.35"); //google ip address
			yield return new WaitUntil(() => p.isDone);
			Debug.Log(p.time);
			if (p.time > requiredSpeed)
				onSpeedAchieved?.Invoke();
			else Debug.Log("SlowSpeed");
		}


		public static T CloneObject<T>(T source)
		{
			#if NETFX_CORE
        if (!typeof(T).GetTypeInfo().IsSerializable)
			#else
			if (!typeof(T).IsSerializable)
				#endif
				throw new System.ArgumentException("The type must be serializable.", "source");

			// Don't serialize a null object, simply return the default for that object
			if (Object.ReferenceEquals(source, null))
				return default(T);
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			Stream        stream     = new MemoryStream();
			using (stream)
			{
				serializer.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T) serializer.Deserialize(stream);
			}
		}

		#endregion

	}

	[System.Serializable]
	public class LocationTransform
	{

		public Vector3    Position;
		public Quaternion Rotation;
		public Vector3    Scale = Vector3.one;

	}

	public enum Axis
	{

		x,
		y,
		z

	}


}