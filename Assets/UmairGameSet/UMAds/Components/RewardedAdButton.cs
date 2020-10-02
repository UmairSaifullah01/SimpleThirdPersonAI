using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace UMAds
{


	[RequireComponent(typeof(Button))]
	public class RewardedAdButton : MonoBehaviour
	{

		[SerializeField] UnityEvent OnReward;
		[SerializeField] int        index;

		void Start()
		{
			GetComponent<Button>().onClick.AddListener(Call);
			AdsManager.LoadRewarded(index);
		}

		void Call()
		{
			AdsManager.ShowRewardedVideo(index, OnReward.Invoke);
		}

	}


}