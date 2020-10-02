using System;
using UnityEngine;


namespace UMAds
{


	public interface IAdsPlugin
	{

		void Init(bool agree);

		void ShowBanner(int index);

		void HideBanner(int index);

		void ShowInterstitial(int index);

		bool IsRewardedAvailable(int index);

		void ShowRewardedVideo(int index, Action reward);

		void LoadRewarded(int index);

		void ShowIConAd(int index, GameObject gameObject);

		void HideIConAd(int index, GameObject gameObject);

		void ShowNative(int index, GameObject gameObject);

		void HideNative(int index);

		void HideAll(AdsType type);

	}


}