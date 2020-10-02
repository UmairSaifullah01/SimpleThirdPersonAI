using TMPro;
using UMGS;
using UnityEngine;


namespace UMUINew
{


	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TextView : ViewBase
	{

		TextMeshProUGUI _text;
		string text
		{
			set
			{
				if (!_text)
				{
					_text = GetComponent<TextMeshProUGUI>();
				}

				_text.text = value;
			}
		}

		public override void Init(IViewModel viewModel)
		{
			base.Init(viewModel);
			viewModel.stringBinder += ViewModelOnStringBinder;
		}

		void ViewModelOnStringBinder(string textName, string value)
		{
			if (this.Id == textName)
			{
				text = value;
			}
		}

	}


}