//using System;
//using UnityEngine;
//using UnityEngine.Advertisements;


//namespace UMAds
//{


//	public class UnityPlugin : IAdsPlugin, IUnityAdsListener
//	{

//		Action reward;

//		public UnityPlugin(UnitySettings unitySettings)
//		{
//			Advertisement.Initialize(unitySettings.gameId, unitySettings.testMode);
//			Advertisement.AddListener(this);
//		}


//		public void Init(bool agree)
//		{
//			throw new NotImplementedException();
//		}


//		public void ShowBanner(int index)
//		{
//			Advertisement.Banner.SetPosition(UnityEngine.Advertisements.BannerPosition.TOP_CENTER);
//			Advertisement.Banner.Show();
//		}

//		public void HideBanner(int index)
//		{
//			Advertisement.Banner.Hide();
//		}

//		public void ShowInterstitial(int index)
//		{
//			Advertisement.Show();
//		}

//		public bool IsRewardedAvailable(int index)
//		{
//			return Advertisement.IsReady();
//		}

//		public void ShowRewardedVideo(int index, Action reward)
//		{
//			this.reward = reward;
//			Advertisement.Show();
//		}


//		public void LoadRewarded(int index)
//		{
//		}

//		public void ShowIConAd(int index, GameObject gameObject)
//		{
//			throw new NotImplementedException();
//		}

//		public void HideIConAd(int index, GameObject gameObject)
//		{
//			throw new NotImplementedException();
//		}

//		public void ShowNative(int index, GameObject gameObject)
//		{
//			throw new NotImplementedException();
//		}

//		public void HideNative(int index)
//		{
//			throw new NotImplementedException();
//		}

//		public void HideAll(AdsType type)
//		{

//		}

//		public void OnUnityAdsReady(string placementId)
//		{
			
//		}

//		public void OnUnityAdsDidError(string message)
//		{
		
//		}

//		public void OnUnityAdsDidStart(string placementId)
//		{
		
//		}

//		public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
//		{
//			if (showResult == ShowResult.Finished)
//			{
//				reward?.Invoke();
//				reward = null;
//				// Reward the user for watching the ad to completion.
//			}
//			else if (showResult == ShowResult.Skipped)
//			{
//				// Do not reward the user for skipping the ad.
//			}
//			else if (showResult == ShowResult.Failed)
//			{
//				//"The ad did not finish due to an error."
//			}
//		}

//		private void GiveReward()
//		{
//			reward?.Invoke();
//			reward = null;
//		}

//	}

//	[System.Serializable]
//	public class UnitySettings
//	{

//		public string                                    gameId         = "1486550";
//		public bool                                      testMode       = true;
//		public UnityEngine.Advertisements.BannerPosition bannerPosition = UnityEngine.Advertisements.BannerPosition.TOP_CENTER;

//	}


//}