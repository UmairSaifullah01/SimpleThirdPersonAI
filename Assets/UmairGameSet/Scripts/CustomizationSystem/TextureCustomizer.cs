using UnityEngine;

[System.Serializable]
public class TextureCustomizer : Customizer
{
	public  Material[]             BodyMaterials;
	private TextureCustomizeItem[] textures;
	public override CustomizationInfluence CurrentInfluence()
	{
		return textures[currentIndex].influence;
	}

	public override CustomizeItem CurrentItem()
	{
		return textures[currentIndex];
	}

	public override void LoadData()
	{
		textures = CustomizeDataContainer.Instance.TexturesData.textures;
		maxIndex = textures.Length;
	}

	public override void Select()
	{
		Selected = currentIndex;
	}

	protected override void Show()
	{
		foreach (var m in BodyMaterials)
		{
			m.mainTexture = textures[currentIndex].texture;
		}
	}

}