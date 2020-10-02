using UMDataManagement;
using UnityEngine;

public class CustomizationController : MonoBehaviour
{

	public string                 ID;
	public CustomizationInfluence defualtVaules;
	public WheelCustomizer        wheelCustomizer;
	public TextureCustomizer      textureCustomizer;
	//  public ColorCustomizer colorCustomizer;
	public SimpleCustomizer fuelCustomizer;
	public SimpleCustomizer engineCustomizer;
	public Customizer       defaultCustomizer;

	private void Awake()
	{
		if (DataPersistSystem.Instance != null && DataPersistSystem.Instance.Contains(ID))
		{
			GetSavedData();
		}

		wheelCustomizer.LoadData();
		wheelCustomizer.MoveNext();
		textureCustomizer.LoadData();
		textureCustomizer.MoveNext();
		engineCustomizer.LoadData();
		engineCustomizer.MoveNext();
		// colorCustomizer.LoadData();
		// colorCustomizer.MoveNext();
		fuelCustomizer.LoadData();
		fuelCustomizer.MoveNext();
		defaultCustomizer = wheelCustomizer;
	}

	private void GetSavedData()
	{
		if (DataPersistSystem.Instance == null) return;
		var data = DataPersistSystem.Instance.Get<Data<int, int, int, int>>(ID);
		if (data == null) return;
		wheelCustomizer.currentIndex = data.value0 - 1;
		// colorCustomizer.currentIndex = data.value1 - 1;
		textureCustomizer.currentIndex = data.value1 - 1;
		fuelCustomizer.currentIndex    = data.value2 - 1;
		engineCustomizer.currentIndex  = data.value3 - 1;
		wheelCustomizer.Selected       = data.value0;
		textureCustomizer.Selected     = data.value1;
		// colorCustomizer.Selected = data.value1;
		fuelCustomizer.Selected   = data.value2;
		engineCustomizer.Selected = data.value3;
	}

	public void SaveData()
	{
		if (DataPersistSystem.Instance != null)
		{
			wheelCustomizer.Select();
			textureCustomizer.Select();
			fuelCustomizer.Select();
			engineCustomizer.Select();
			var data = new Data<int, int, int, int>(wheelCustomizer.Selected, textureCustomizer.Selected, fuelCustomizer.Selected, engineCustomizer.Selected);
			DataPersistSystem.Instance.Add(ID, data);
		}
	}

	public void MoveNext()
	{
		defaultCustomizer.MoveNext();
	}

	public void MoveBack()
	{
		defaultCustomizer.MoveBack();
	}

	public void CustomizationType(int type)
	{
		switch (type)
		{
			case 0:
				defaultCustomizer = wheelCustomizer;
				break;

			case 1:
				defaultCustomizer = textureCustomizer;
				// defaultCustomizer = colorCustomizer;
				break;

			case 2:
				defaultCustomizer = fuelCustomizer;
				break;

			case 3:
				defaultCustomizer = engineCustomizer;
				break;
		}
	}


	public void ShowEntity(int index)
	{
		defaultCustomizer.currentIndex = index - 1;
		defaultCustomizer.MoveNext();
	}

}