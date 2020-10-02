using UnityEngine;

public class MiniMapItem : MonoBehaviour, IMiniMapItem
{

	[SerializeField] private Sprite _icon;
	[SerializeField] private bool   isbounded = false;
	public Sprite Icon
	{
		get => _icon;
	}
	public Color IconColor => Color.green;
	public Vector3 Position
	{
		get => transform.position;
	}

	public bool isBounded => isbounded;

	public float RotationZ => -transform.eulerAngles.y;

	public void Subscribe()
	{
		MiniMap.Subscribe(this);
	}

	void OnDestroy()
	{
		MiniMap.Unsubscribe(this);
	}

	void Start()
	{
		Subscribe();
	}

}