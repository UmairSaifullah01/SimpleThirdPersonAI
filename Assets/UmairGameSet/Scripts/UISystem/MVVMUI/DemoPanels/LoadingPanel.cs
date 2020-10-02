using System;
using System.Collections;
using System.Collections.Generic;
using UMGS;
using UnityEngine;
using UnityEngine.UI;


namespace UMUINew
{


	public class LoadingPanel : UIPanel
	{

		public override void Execute()
		{
			base.Execute();
			FloatBinder("LoadingSlider", 1);
		}

	}


}