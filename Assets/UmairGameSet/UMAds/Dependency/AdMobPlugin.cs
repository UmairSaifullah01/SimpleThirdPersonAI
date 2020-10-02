//using System;
//using GoogleMobileAds.Api;
//using UnityEngine;


//namespace UMAds
//{


//	public class AdMobPlugin : IAdsPlugin
//	{

//		BannerView[] bannerViews;

//		InterstitialAd[] interstitialAds;

//		RewardedAd[] rewardedAds;
//		Action       reward;

//		public AdMobPlugin(AdmobSettings settings)
//		{
//			this.bannerViews     = settings.GetBannerViews();
//			this.interstitialAds = settings.GetInterstitialAds();
//			this.rewardedAds     = settings.GetRewardedAds();
//			MobileAds.Initialize(InitCompleteAction);
//		}

//		void InitCompleteAction(InitializationStatus status)
//		{
//			foreach (InterstitialAd interstitialAd in interstitialAds)
//			{
//				interstitialAd.LoadAd(Request());
//			}
//		}

//		public void Init(bool agree)
//		{
//			throw new NotImplementedException();
//		}

//		private AdRequest Request()
//		{
//			return new AdRequest.Builder().Build();
//		}

//		public void ShowBanner(int index)
//		{
//			bannerViews[index].LoadAd(Request());
//		}

//		public void HideBanner(int index)
//		{
//			bannerViews[index].Hide();
//		}

//		public void ShowInterstitial(int index)
//		{
//			if (interstitialAds[index].IsLoaded())
//			{
//				interstitialAds[index].Show();
//				interstitialAds[index].LoadAd(Request());
//			}
//		}

//		public bool IsRewardedAvailable(int index)
//		{
//			return rewardedAds[index].IsLoaded();
//		}

//		public void ShowRewardedVideo(int index, Action reward)
//		{
//			this.reward                           =  reward;
//			rewardedAds[index].OnUserEarnedReward += OnOnUserEarnedReward;
//			rewardedAds[index].Show();
//		}

//		void OnOnUserEarnedReward(object sender, Reward e)
//		{
//			reward?.Invoke();
//			reward = null;
//		}

//		public void LoadRewarded(int index)
//		{
//			rewardedAds[index].OnUserEarnedReward -= OnOnUserEarnedReward;
//			rewardedAds[index].LoadAd(Request());
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
//			throw new NotImplementedException();
//		}

//	}

//	[System.Serializable]
//	public class AdmobSettings
//	{

//		public AdmobBanner[] bannerViews     = new[] {new AdmobBanner() {id = "ca-app-pub-3940256099942544/6300978111", size = BannerSize.SmartBanner, position = AdPosition.Top}};
//		public string[]      interstitialAds = {"ca-app-pub-3940256099942544/1033173712"};
//		public string[]      rewardedAds     = {"ca-app-pub-3940256099942544/5224354917"};

//		// public BannerView[]     bannerViews     = new[] {new BannerView("ca-app-pub-3940256099942544/6300978111", AdSize.SmartBanner, AdPosition.Top)};
//		// public InterstitialAd[] interstitialAds = new[] {new InterstitialAd("ca-app-pub-3940256099942544/1033173712")};
//		// public RewardedAd[]     rewardedAds     = new[] {new RewardedAd("ca-app-pub-3940256099942544/5224354917")};
//		public BannerView[] GetBannerViews()
//		{
//			BannerView[] actualBannerViews = new BannerView[bannerViews.Length];
//			for (int i = 0; i < bannerViews.Length; i++)
//			{
//				actualBannerViews[i] = bannerViews[i].GetBanner();
//			}

//			return actualBannerViews;
//		}

//		public InterstitialAd[] GetInterstitialAds()
//		{
//			InterstitialAd[] actualInterstitialAds = new InterstitialAd[interstitialAds.Length];
//			for (int i = 0; i < interstitialAds.Length; i++)
//			{
//				actualInterstitialAds[i] = new InterstitialAd(interstitialAds[i]);
//			}

//			return actualInterstitialAds;
//		}

//		public RewardedAd[] GetRewardedAds()
//		{
//			RewardedAd[] actualRewardedAds = new RewardedAd[interstitialAds.Length];
//			for (int i = 0; i < interstitialAds.Length; i++)
//			{
//				actualRewardedAds[i] = new RewardedAd(rewardedAds[i]);
//			}

//			return actualRewardedAds;
//		}

//	}

//	[System.Serializable]
//	public struct AdmobBanner
//	{

//		public string     id;
//		public BannerSize size;
//		public AdPosition position;

//		public BannerView GetBanner()
//		{
//			return new BannerView(id, GetSize(), position);
//		}

//		AdSize GetSize()
//		{
//			switch (size)
//			{
//				case BannerSize.Banner:
//					return AdSize.Banner;

//				case BannerSize.MediumRectangle:
//					return AdSize.MediumRectangle;

//				case BannerSize.IABBanner:
//					return AdSize.IABBanner;

//				case BannerSize.Leaderboard:
//					return AdSize.Leaderboard;

//				case BannerSize.SmartBanner:
//					return AdSize.SmartBanner;

//				default:
//					return AdSize.Banner;
//			}
//		}

//	}

//	public enum BannerSize
//	{

//		Banner,
//		MediumRectangle,
//		IABBanner,
//		Leaderboard,
//		SmartBanner

//	}


//}