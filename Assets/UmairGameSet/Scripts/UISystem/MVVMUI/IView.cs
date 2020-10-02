using System;
using System.Collections;
using System.Collections.Generic;
using UMUI;
using UnityEngine;
using UnityEngine.UI;


namespace UMGS
{


	public interface IView
	{

		string     Id        { get; }
		IViewModel viewModel { get; set; }

		void Init(IViewModel viewModel);

		Transform GetTransform();

	}


	public interface IViewModel
	{

		event Action<string, string> stringBinder;
		event Action<string, float>  floatBinder;
		event Action<string, Action> eventBinder;
		IView[]                      views          { get; set; }
		IStorageService              storageService { get; set; }

		void InitViewModel();

	}

	public interface IStrongViewModel : IViewModel
	{

		event Action<string, Image> imageBinder;

	}


}