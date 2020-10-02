using UnityEngine;

[System.Serializable]
public class WheelCustomizer : Customizer
{
	public  Material[]           wheelMaterials;
	private WheelCustomizeItem[] wheelCustomizeItems;

	public override CustomizationInfluence CurrentInfluence()
	{
		return wheelCustomizeItems[currentIndex].influence;
	}

	public override CustomizeItem CurrentItem()
	{
		return wheelCustomizeItems[currentIndex];
	}

	public override void LoadData()
	{
		wheelCustomizeItems = CustomizeDataContainer.Instance.Wheeldata.Elements;
		maxIndex            = wheelCustomizeItems.Length;


		// foreach (var w in WheelOjects)
		// {
		//     var renderer = w.GetComponent<MeshRenderer>();
		//     if (renderer)
		//     {
		//         Object.Destroy(renderer);
		//     }
		//
		//     var meshFilter = w.GetComponent<MeshFilter>();
		//     if (meshFilter)
		//     {
		//         Object.Destroy(meshFilter);
		//     }
		// }

		// Show();
	}

	public override void Select()
	{
		Selected = currentIndex;
	}

	protected override void Show()
	{
		if (currentIndex < 0) currentIndex = 0;
		WheelCustomizeItem customizeItem   = wheelCustomizeItems[currentIndex];
		var                wheelTexture    = customizeItem.wheelTexture;
		foreach (var material in wheelMaterials)
		{
			material.mainTexture = wheelTexture;
		}

		// var wheelOject = customizeItem.WheelObject;
		// foreach (var w in WheelOjects)
		// {
		//     if (w.transform.childCount > 0)
		//     {
		//         foreach (Transform item in w.transform)
		//         {
		//             Object.Destroy(item.gameObject);
		//         }
		//     }
		//
		//     Object.Instantiate(wheelOject, w.transform.position, w.transform.rotation, w.transform);
		// }
	}
}