using System;
using System.Collections;
using System.Collections.Generic;
using UMGS;
using UnityEngine;
using UnityEngine.UI;


namespace UMUINew
{


	[RequireComponent(typeof(Button))]
	public class ButtonView : ViewBase
	{

		[SerializeField] string methodName;

		public override void Init(IViewModel viewModel)
		{
			base.Init(viewModel);
			GetComponent<Button>().onClick.AddListener(OnClick);
		}


		void OnClick()
		{
			Debug.Log(viewModel.GetType().Name);
			var method = viewModel.GetType().GetMethod(methodName);
			method?.Invoke(viewModel, new object[0]);
		}

	}


}