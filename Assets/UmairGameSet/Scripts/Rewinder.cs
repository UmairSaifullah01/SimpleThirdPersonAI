using System.Collections.Generic;
using UnityEngine;


public class Rewinder : MonoBehaviour
{

	bool isRewinding = false;

	public float recordTime = 5f;

	List<TransformData> data;

	Rigidbody rb;

	// Use this for initialization
	void Start()
	{
		data = new List<TransformData>();
		rb   = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Jump"))
			StartRewind();
		if (Input.GetButtonUp("Jump"))
			StopRewind();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Respawn"))
		{
			StartRewind();
		}
	}

	void FixedUpdate()
	{
		if (isRewinding)
			Rewind();
		else
			Record();
	}

	void Rewind()
	{
		if (data.Count > 0)
		{
			TransformData pointInTime = data[0];
			transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
			data.RemoveAt(0);
		}
		else
		{
			StopRewind();
		}
	}

	void Record()
	{
		if (data.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			data.RemoveAt(data.Count - 1);
		}

		data.Insert(0, new TransformData(transform.position, transform.rotation));
	}

	public void StartRewind()
	{
		isRewinding    = true;
		rb.isKinematic = true;
	}

	public void StopRewind()
	{
		isRewinding    = false;
		rb.isKinematic = false;
	}

	public class TransformData
	{

		public Vector3    position;
		public Quaternion rotation;

		public TransformData(Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}

	}

}