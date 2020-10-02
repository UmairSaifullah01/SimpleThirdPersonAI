using System;
using System.Collections;
using System.Collections.Generic;
using UMGS;
using UnityEngine;
using UnityEngine.UI;

namespace UMUINew
{


	[RequireComponent(typeof(Button))]
	public class ButtonBinder : ViewBase
	{

		Action clickEvent;

		public override void Init(IViewModel viewModel)
		{
			base.Init(viewModel);
			GetComponent<Button>().onClick.AddListener(OnClick);
			viewModel.eventBinder += ViewModelOnEventBinder;
		}

		void ViewModelOnEventBinder(string id, Action action)
		{
			if (this.Id == id)
				clickEvent = action;
		}

		void OnClick()
		{
			clickEvent?.Invoke();
		}

	}


}