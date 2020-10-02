using UnityEngine;
using UnityEngine.UI;


namespace UMAds
{
	
	[RequireComponent(typeof(Button))]
	public class AdsButton : MonoBehaviour
	{

		[SerializeField] AdsType type;
		[SerializeField] int     index;

		void Start()
		{
			GetComponent<Button>().onClick.AddListener(Call);
		}

		void Call()
		{
			switch (type)
			{
				case AdsType.Banner:
					AdsManager.ShowBanner(index);
					break;

				case AdsType.Interstitial:
					AdsManager.ShowInterstitial(index);
					break;

				case AdsType.Icon:
					AdsManager.ShowIConAd(index, gameObject);
					break;

				case AdsType.Native:
					AdsManager.ShowNative(index, gameObject);
					break;
			}
		}

	}


}