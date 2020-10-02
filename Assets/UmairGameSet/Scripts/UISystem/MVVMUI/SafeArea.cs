using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UMUINew
{


	public class SafeArea : MonoBehaviour
	{

		Canvas            canvas;
		RectTransform     panelSafeArea;
		Rect              currentSafeArea;
		ScreenOrientation currentOrientation;

		void Start()
		{
			canvas             = GetComponentInParent<Canvas>();
			panelSafeArea      = GetComponent<RectTransform>();
			currentOrientation = Screen.orientation;
			currentSafeArea    = Screen.safeArea;
			ApplySafeArea();
		}

		void ApplySafeArea()
		{
			Vector2 anchorMin = currentSafeArea.position;
			Vector2 anchorMax = currentSafeArea.position + currentSafeArea.size;
			anchorMin.x             /= canvas.pixelRect.width;
			anchorMin.y             /= canvas.pixelRect.height;
			anchorMax.x             /= canvas.pixelRect.width;
			anchorMax.y             /= canvas.pixelRect.height;
			panelSafeArea.anchorMin =  anchorMin;
			panelSafeArea.anchorMax =  anchorMax;
		}

		void Update()
		{
			if (currentOrientation != Screen.orientation && currentSafeArea != Screen.safeArea) ApplySafeArea();
		}

	}


}