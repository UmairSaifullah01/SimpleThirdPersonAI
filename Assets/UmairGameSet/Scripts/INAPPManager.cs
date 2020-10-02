// #if UNITY_PURCHASING
// using UnityEngine;
// using UnityEngine.Purchasing;
//
//
// namespace UMGS
// {
// 	public class INAPPManager : IStoreListener
// 	{
//
// 		private static INAPPManager       instance;
// 		static         bool               unityPurchasingInitialized;
// 		protected      IStoreController   controller;
// 		protected      IExtensionProvider extensions;
// 		protected      ProductCatalog     catalog;
// 		static         bool               initializationComplete;
//
// 		[RuntimeInitializeOnLoadMethod]
// 		static void Initialize()
// 		{
// 			CreateCodelessIAPStoreListenerInstance();
// 		}
//
// 		static void CreateCodelessIAPStoreListenerInstance()
// 		{
// 			instance = new INAPPManager();
// 			if (!unityPurchasingInitialized)
// 			{
// 				Debug.Log("Initializing UnityPurchasing via INAPPManager IAP");
// 				InitializePurchasing();
// 				EventHandler.OnBuyINAPP.AddListener(instance.InitiatePurchase);
// 				EventHandler.HasPruchaseINAPP.AddListener(instance.HasPurchased);
// 			}
// 		}
//
// 		public static INAPPManager Instance
// 		{
// 			get
// 			{
// 				if (instance == null)
// 				{
// 					CreateCodelessIAPStoreListenerInstance();
// 				}
//
// 				return instance;
// 			}
// 		}
//
// 		private INAPPManager()
// 		{
// 			catalog = ProductCatalog.LoadDefaultCatalog();
// 		}
//
// 		private bool HasPurchased(string id)
// 		{
// 			return controller.products.WithID(id).hasReceipt;
// 		}
//
// 		public bool HasProductInCatalog(string productID)
// 		{
// 			foreach (var product in catalog.allProducts)
// 			{
// 				if (product.id == productID)
// 				{
// 					return true;
// 				}
// 			}
//
// 			return false;
// 		}
//
// 		public Product GetProduct(string productID)
// 		{
// 			if (controller != null && controller.products != null && !string.IsNullOrEmpty(productID))
// 			{
// 				return controller.products.WithID(productID);
// 			}
//
// 			Debug.LogError("CodelessIAPStoreListener attempted to get unknown product " + productID);
// 			return null;
// 		}
//
// 		public void InitiatePurchase(string productID)
// 		{
// 			if (controller != null) controller.InitiatePurchase(productID);
// 		}
//
// 		private static void InitializePurchasing()
// 		{
// 			StandardPurchasingModule module = StandardPurchasingModule.Instance();
// 			module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
// 			ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
// 			IAPConfigurationHelper.PopulateConfigurationBuilder(ref builder, instance.catalog);
// 			UnityPurchasing.Initialize(instance, builder);
// 			unityPurchasingInitialized = true;
// 		}
//
// 		public IStoreController StoreController => controller;
//
// 		public IExtensionProvider ExtensionProvider => extensions;
//
// 		public void OnInitializeFailed(InitializationFailureReason error)
// 		{
// 			Debug.LogError(string.Format("Purchasing failed to initialize. Reason: {0}", error.ToString()));
// 		}
//
// 		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
// 		{
// 			PurchaseProcessingResult result;
// 			EventHandler.OnInAPPSuccess.Send(e.purchasedProduct.definition.id);
// 			return (true) ? PurchaseProcessingResult.Complete : PurchaseProcessingResult.Pending;
// 		}
//
// 		public void OnPurchaseFailed(Product product, PurchaseFailureReason p)
// 		{
// 			Debug.LogError("Failed purchase not correctly handled for product \"" + product.definition.id + "\". Add an active IAPButton to handle this failure, or add an IAPListener to receive any unhandled purchase failures.");
// 		}
//
// 		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
// 		{
// 			initializationComplete = true;
// 			this.controller = controller;
// 			this.extensions = extensions;
// 		}
//
// 	}
//
// }
// #endif