using UnityEngine;


namespace UMGS
{


	public class EventHandler
	{

		//Game General Events
		public static EventListener LevelComplete = new EventListener();
		public static EventListener LevelFail     = new EventListener();
		//Game states General Events
		public static EventListener OnGamePaused  = new EventListener();
		public static EventListener OnGameResume  = new EventListener();
		public static EventListener OnGameRestart = new EventListener();
		public static EventListener OnGameToHome  = new EventListener();
		//Camera Events
		public static EventListener<int> OnCameraChange = new EventListener<int>();


		//InApp Events
		public static EventListener               OnRemoveAdsPurchaseSuccess     = new EventListener();
		public static EventListener               OnAllGameUnlockPurchaseSuccess = new EventListener();
		public static EventListener<string>       OnBuyINAPP                     = new EventListener<string>();
		public static EventCallBack<string, bool> HasPruchaseINAPP               = new EventCallBack<string, bool>();
		public static EventListener<string>       OnInAPPSuccess                 = new EventListener<string>();

		//Analytics 
		public static EventListener<string> AnalyticsEvent = new EventListener<string>();
		//Ads
		public static EventListener<int>       ShowInterstitial        = new EventListener<int>();
		public static EventListener<int>       ShowRewaredVideo        = new EventListener<int>();
		public static EventListener<int>       LoadRewaredVideo        = new EventListener<int>();
		public static EventCallBack<int, bool> IsRewaredAvailable      = new EventCallBack<int, bool>();
		public static EventListener            RewaredVideoRewardEvent = new EventListener();
//Sounds
		public static EventListener         ButtonSound = new EventListener();
		public static EventListener<string> PlaySound   = new EventListener<string>();

	}


}