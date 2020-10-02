using System;
using UnityEngine;

public class UMTouchControl
{

	private Vector3 _fp;           //First touch position
	private Vector3 _lp;           //Last touch position
	private float   _dragDistance; //minimum distance for a swipe to be registered
	public  Action  OnSwipeLeft, OnSwipeRight, OnSwipeUP, OnSwipeDown, OnTap;

	public static UMTouchControl Init(float dragDistance)
	{
		return new UMTouchControl {_dragDistance = dragDistance};
	}

	public void DoUpdate()
	{
		if (Input.touchCount == 1) // user is touching the screen with a single touch
		{
			var touch = Input.GetTouch(0); // get the touch
			switch (touch.phase)
			{
				//check for the first touch
				case TouchPhase.Began:
					_fp = touch.position;
					_lp = touch.position;
					break;

				// update the last position based on where they moved
				case TouchPhase.Moved:
					_lp = touch.position;
					break;

				//check if the finger is removed from the screen
				case TouchPhase.Ended:
				{
					_lp = touch.position; //last touch position. Ommitted if you use list

					//Check if drag distance is greater than 20% of the screen height
					if (Mathf.Abs(_lp.x - _fp.x) > _dragDistance || Mathf.Abs(_lp.y - _fp.y) > _dragDistance)
					{
						//It's a drag
						//check if the drag is vertical or horizontal
						if (Mathf.Abs(_lp.x - _fp.x) > Mathf.Abs(_lp.y - _fp.y))
						{
							//If the horizontal movement is greater than the vertical movement...
							if ((_lp.x > _fp.x)) //If the movement was to the right)
							{
								//Right swipe
								OnSwipeRight?.Invoke();
							}
							else
							{
								OnSwipeLeft?.Invoke();
								//Left swipe
							}
						}
						else
						{
							//the vertical movement is greater than the horizontal movement
							if (_lp.y > _fp.y) //If the movement was up
							{
								//Up swipe
								OnSwipeUP?.Invoke();
							}
							else
							{
								OnSwipeDown?.Invoke();
								//Down swipe
							}
						}
					}
					else
					{
						OnTap?.Invoke();
						//It's a tap as the drag distance is less than 20% of the screen height
						Debug.Log("Tap");
					}

					break;
				}
			}
		}
	}

}