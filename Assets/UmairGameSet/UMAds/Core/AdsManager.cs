using System;
using UnityEngine;


namespace UMAds
{


	public class AdsManager
	{

		private static IAdsPlugin plugin;
		public static  string     removeAds  = "RemoveAds";
		public static  bool       isEventLog = true;
		private static bool showAds
		{
			get
			{
				bool isTrue = false;
				#if !UNITY_EDITOR
				isTrue = PlayerPrefs.GetInt(removeAds) == 0;
				#endif
				return isTrue;
			}
		}

		[RuntimeInitializeOnLoadMethod]
		static void Initialization()
		{
			//plugin = new UnityAdmobPlugin();
		}
/// <summary>
/// Agreement for user YES or No
/// </summary>
/// <param name="value"></param>
		public static void UserConsent(bool value)
		{
			if (showAds)
				plugin.Init(value);
			Print($"*********** Consent {value} *******");
		}
/// <summary>
/// Show banner Related to Index
/// </summary>
/// <param name="index">type of banner</param>
		public static void ShowBanner(int index)
		{
			if (showAds)
				plugin.ShowBanner(index);
			Print($"Banner Called ID:{index}");
		}
/// <summary>
/// Hide Banner Related To Index
/// </summary>
/// <param name="index">type of banner</param>
		public static void HideBanner(int index)
		{
			if (showAds)
				plugin.HideBanner(index);
			Print($"Banner Hide ID:{index}");
		}
/// <summary>
/// Show Interstitial With Given Id Index
/// </summary>
/// <param name="index">type of interstitial</param>
		public static void ShowInterstitial(int index)
		{
			if (showAds)
				plugin.ShowInterstitial(index);
			Print($"Interstitial Called ID:{index}");
		}
/// <summary>
/// 
/// </summary>
/// <param name="index"></param>
/// <returns></returns>
		public static bool IsRewardedAvailable(int index)
		{
			return !showAds || plugin.IsRewardedAvailable(index);
		}

		public static void ShowRewardedVideo(int index, Action reward)
		{
			if (showAds)
				plugin.ShowRewardedVideo(index, reward);
			#if UNITY_EDITOR
			else reward.Invoke();
			#endif
			Print($"RewardedVideo Called ID:{index}");
		}

		public static void LoadRewarded(int index)
		{
			if (showAds)
				plugin.LoadRewarded(index);
			Print($"Loading RewardedVideo Started ID:{index}");
		}

		public static void ShowIConAd(int index, GameObject gameObject)
		{
			if (showAds)
				plugin.ShowIConAd(index, gameObject);
			Print($"IConAd Called ID:{index}");
		}

		public static void HideIConAd(int index, GameObject gameObject)
		{
			if (showAds)
				plugin.HideIConAd(index, gameObject);
			Print($"IConAd Hided ID:{index}");
		}

		public static void ShowNative(int index, GameObject gameObject)
		{
			if (showAds)
				plugin.ShowNative(index, gameObject);
			Print($"Native Called ID:{index}");
		}

		public static void HideNative(int index)
		{
			if (showAds)
				plugin.HideNative(index);
			Print($"Native Hided ID:{index}");
		}

		public static void HideAll(AdsType type)
		{
			if (showAds)
				plugin.HideAll(type);
			Print($"{type} All Ads Hided");
		}

		static void Print(string text)
		{
			if (isEventLog)
				Debug.Log(text);
		}

	}

	public enum AdsType
	{

		Banner,
		Interstitial,
		Icon,
		Native

	}


}