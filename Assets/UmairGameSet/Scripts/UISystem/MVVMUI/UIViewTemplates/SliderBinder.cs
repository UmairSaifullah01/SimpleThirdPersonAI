using System.Collections;
using System.Collections.Generic;
using UMGS;
using UnityEngine;
using UnityEngine.UI;


namespace UMUINew
{


	[RequireComponent(typeof(Slider))]
	public class SliderBinder : ViewBase
	{

		Slider _slider;
		float sliderValue
		{
			set
			{
				if (!_slider) _slider = GetComponent<Slider>();
				_slider.value = value;
			}
		}

		public override void Init(IViewModel viewModel)
		{
			base.Init(viewModel);
			this.viewModel.floatBinder += ViewModelOnFloatBinder;
		}

		void ViewModelOnFloatBinder(string sliderName, float value)
		{
			if (this.Id == sliderName)
			{
				sliderValue = value;
			}
		}

	}


}