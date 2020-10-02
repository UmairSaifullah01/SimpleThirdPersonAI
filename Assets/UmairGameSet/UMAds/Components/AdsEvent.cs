using System;
using UMAds;
using UnityEngine;


namespace UMAds
{
	public class AdsEvent : MonoBehaviour
    {
    
    	[SerializeField] AdsType type;
    	[SerializeField] int     index;
    
    	void OnEnable()
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
    
    			default:
    				throw new ArgumentOutOfRangeException();
    		}
    	}
    
    	void OnDisable()
    	{
    		switch (type)
    		{
    			case AdsType.Banner:
    				AdsManager.HideBanner(index);
    				break;
    
    			case AdsType.Icon:
    				AdsManager.HideIConAd(index, gameObject);
    				break;
    
    			case AdsType.Native:
    				AdsManager.HideNative(index);
    				break;
    
    			default:
    				throw new ArgumentOutOfRangeException();
    		}
    	}

    }


}
