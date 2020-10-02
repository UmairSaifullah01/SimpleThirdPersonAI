using System.Collections;
using UMGS;
using UnityEngine;

[System.Serializable]
public class ColorCustomizer : Customizer
{
	public  Material[]           BodyMaterials;
	private ColorCustomizeItem[] colors;
	private Coroutine            coroutine;

	public override CustomizationInfluence CurrentInfluence()
	{
		return colors[currentIndex].influence;
	}

	public override CustomizeItem CurrentItem()
	{
		return colors[currentIndex];
	}

	public override void LoadData()
	{
		colors   = CustomizeDataContainer.Instance.ColorData.Colors;
		maxIndex = colors.Length;
		//   Show ();
	}

	public override void Select()
	{
		Selected = currentIndex;
	}

	protected override void Show()
	{
		if (coroutine != null) CoroutineHandler.StopStaticCoroutine(coroutine);
		var color = colors[currentIndex].color;
		coroutine = CoroutineHandler.StartStaticCoroutine(ChangeColor(color));
	}

	private IEnumerator ChangeColor(Color color)
	{
		var isTrue = true;
		while (isTrue)
		{
			foreach (var m in BodyMaterials)
			{
				m.color = Color.Lerp(m.color, color, 2 * Time.deltaTime);
				if (m.color == color) isTrue = false;
				yield return new WaitForFixedUpdate();
			}
		}
	}
}