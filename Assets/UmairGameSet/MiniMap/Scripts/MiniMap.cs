using System.Collections.Generic;
using System.Linq;
using UMGS;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{

	public static            MiniMap        instance;
	public                   Transform      player;
	public                   Camera         cam;
	public                   RectTransform  container;
	public                   RectTransform  ItemUIPrefab;
	[SerializeField] private float          BorderOffScreen;
	private                  float          radius;
	[HideInInspector] public List<ItemData> items = new List<ItemData>();
	TargetFollower                          follower;


	void Awake()
	{
		instance = this;
		follower = new TargetFollower(cam.transform, player);
		radius   = (container.sizeDelta.x / 2) - BorderOffScreen;
	}

	public static void Subscribe(IMiniMapItem item)
	{
		instance?.AddItem(item);
	}

	public static void Unsubscribe(IMiniMapItem item)
	{
		instance?.Remove(item);
	}

	private void Remove(IMiniMapItem item)
	{
		var index = items.FindIndex(x => x.item == item);
		if (items[index].relativeUIItem &&items[index].relativeUIItem.gameObject)
			Destroy(items[index].relativeUIItem.gameObject);
		items.RemoveAt(index);
	}

	void AddItem(IMiniMapItem item)
	{
		if (items.Exists(x => x.item == item))
			return;
		items.Add(new ItemData() {item = item, relativeUIItem = CreateItemUI(item)});
	}

	private RectTransform CreateItemUI(IMiniMapItem item)
	{
		var rect  = Instantiate(ItemUIPrefab, container);
		var image = rect.GetComponent<Image>();
		image.sprite = item.Icon;
		image.color  = item.IconColor;
		return rect;
	}

	void LateUpdate()
	{
		foreach (var item in items)
		{
			var pos = WorldToCanvasPosition(item.item.Position);
			item.relativeUIItem.anchoredPosition = item.item.isBounded ? InCircleRadius(pos) : pos;
			//item.relativeUIItem.eulerAngles      = RotateOnZ(transform.eulerAngles, item.item.RotationZ);
			//item.relativeUIItem.anchoredPosition = pos;
		}

		follower.DoUpdate();
	}

	private Vector3 RotateOnZ(Vector3 eulerAngles, float rotationZ)
	{
		eulerAngles.z = rotationZ;
		return eulerAngles;
	}

	Vector2 WorldToCanvasPosition(Vector3 position)
	{
		Vector2 vp  = cam.WorldToViewportPoint(position);
		Vector2 pos = new Vector2((vp.x * container.sizeDelta.x) - (container.sizeDelta.x * 0.5f), (vp.y * container.sizeDelta.y) - (container.sizeDelta.y * 0.5f));
		return pos;
	}

	private Vector2 RectBoundPosition(Vector2 pos)
	{
		pos.x = Mathf.Clamp(pos.x, -((container.sizeDelta.x * 0.5f) - BorderOffScreen), ((container.sizeDelta.x * 0.5f) - BorderOffScreen));
		pos.y = Mathf.Clamp(pos.y, -((container.sizeDelta.y * 0.5f) - BorderOffScreen), ((container.sizeDelta.y * 0.5f) - BorderOffScreen));
		return pos;
	}

	private Vector2 InCircleRadius(Vector2 pos)
	{
		Vector2 origin    = container.anchoredPosition;
		var     direction = pos - origin;
		if (direction.sqrMagnitude < (direction.normalized * radius).sqrMagnitude) return pos;
		return direction.normalized * radius;
	}

	public struct ItemData
	{

		public IMiniMapItem  item;
		public RectTransform relativeUIItem;

	}

}