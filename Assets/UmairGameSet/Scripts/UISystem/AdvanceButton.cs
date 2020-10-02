using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AdvanceButton : Selectable, IPointerClickHandler
{

	public UnityEvent OnClick, OnPressed, OnReleased;

	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);
		OnPressed?.Invoke();
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		OnReleased?.Invoke();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClick?.Invoke();
	}

}