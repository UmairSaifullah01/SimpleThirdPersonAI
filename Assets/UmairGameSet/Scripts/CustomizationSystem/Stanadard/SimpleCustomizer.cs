using System.Linq;
using UnityEngine;

[System.Serializable]
public class SimpleCustomizer : Customizer
{
	[SerializeField] private string          CustomizerID;
	private                  CustomizeItem[] customizeItems;

	public override CustomizationInfluence CurrentInfluence()
	{
		return customizeItems[currentIndex].influence;
	}

	public override CustomizeItem CurrentItem()
	{
		return customizeItems[currentIndex];
	}

	public override void LoadData()
	{
		customizeItems = CustomizeDataContainer.Instance.simpleData.First(x => x.CustomizerID == CustomizerID).Elements;
		maxIndex       = customizeItems.Length;
	}

	public override void Select()
	{
		Selected = currentIndex;
	}
}