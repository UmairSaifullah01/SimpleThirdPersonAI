//using System;
//using UMAds;
//using UnityEngine;

//public class UnityAdmobPlugin : IAdsPlugin
//{

//	IAdsPlugin admob;
//	IAdsPlugin unity;

//	public UnityAdmobPlugin()
//	{
//		UnityAdmobSettings.Init();
//		admob = new AdMobPlugin(UnityAdmobSettings.GetAdmobSettings());
//		unity = new UnityPlugin(UnityAdmobSettings.GetUnitySettings());
//	}

//	public void Init(bool agree)
//	{
//		admob.Init(agree);
//		unity.Init(agree);
//	}

//	public void ShowBanner(int index)
//	{
//		if (index == -1)
//		{
//			unity.ShowBanner(0);
//		}
//		else
//		{
//			admob.ShowBanner(index);
//		}
//	}

//	public void HideBanner(int index)
//	{
//		if (index == -1)
//		{
//			unity.HideBanner(0);
//		}
//		else
//		{
//			admob.HideBanner(index);
//		}
//	}

//	public void ShowInterstitial(int index)
//	{
//		if (index == -1)
//		{
//			unity.ShowInterstitial(0);
//		}
//		else
//		{
//			admob.ShowInterstitial(index);
//		}
//	}

//	public bool IsRewardedAvailable(int index)
//	{
//		if (index == -1)
//		{
//			return unity.IsRewardedAvailable(0);
//		}

//		return admob.IsRewardedAvailable(index);
//	}

//	public void ShowRewardedVideo(int index, Action reward)
//	{
//		if (index == -1)
//		{
//			unity.ShowRewardedVideo(0, reward);
//		}
//		else
//		{
//			admob.ShowRewardedVideo(index, reward);
//		}
//	}

//	public void LoadRewarded(int index)
//	{
//		if (index == -1)
//		{
//			unity.LoadRewarded(0);
//		}
//		else
//		{
//			admob.LoadRewarded(index);
//		}
//	}

//	public void ShowIConAd(int index, GameObject gameObject)
//	{
//	}

//	public void HideIConAd(int index, GameObject gameObject)
//	{
//		throw new NotImplementedException();
//	}

//	public void ShowNative(int index, GameObject gameObject)
//	{
//		throw new NotImplementedException();
//	}

//	public void HideNative(int index)
//	{
//		throw new NotImplementedException();
//	}

//	public void HideAll(AdsType type)
//	{
//		throw new NotImplementedException();
//	}

//}