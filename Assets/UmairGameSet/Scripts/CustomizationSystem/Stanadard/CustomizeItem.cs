using System;
using UMDataManagement;
using UMGS;
using UnityEngine;


[System.Serializable]
public class CustomizeItem : IPurchasable
{

	public                   string                 ID;
	[SerializeField] private string                 virtualCurrencyID;
	[SerializeField] private float                    price;
	[SerializeField] private bool                   isPurchased;
	public                   CustomizationInfluence influence;

	public float Price
	{
		get => price;
		set => price = value;
	}


	public Currency CurrencyName { get; }
	public bool IsPurchased
	{
		get => isPurchased;
		set => isPurchased = value;
	}

	public void Purchased()
	{
		DataPersistSystem.Instance.Add(ID, new Data<int>(1));
	}

	public string VirtualCurrencyID
	{
		get => virtualCurrencyID;
		set => virtualCurrencyID = value;
	}
	

	private void GetData()
	{
		var data = DataPersistSystem.Instance.Get<Data<int>>(ID);
		if (data != null)
		{
			isPurchased = data.value == 1;
		}
	}

}

[System.Serializable]
public class WheelCustomizeItem : CustomizeItem
{

	public Texture wheelTexture;

}

[System.Serializable]
public class ColorCustomizeItem : CustomizeItem
{

	public Color color;

}

[System.Serializable]
public class TextureCustomizeItem : CustomizeItem
{

	public Texture2D texture;

}

[System.Serializable]
public class StickerCustomizeItem : CustomizeItem
{

	public GameObject StickerObject;

}