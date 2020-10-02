using System.Collections;
using System.Collections.Generic;
using UMGS;
using UnityEngine;


namespace UMUINew
{


	public class ExitPanel : UIPanel
	{

		public override void Init(IStateMachine stateMachine)
		{
			base.Init(stateMachine);
			SetTransitions(new Transition(nameof(MenuPanel)));
			EventBinder("YesButton", Yes);
			EventBinder("NoButton",  No);
		}
		

		void Yes()
		{
			Application.Quit();
		}

		void No()
		{
			SetTransitionCondition(nameof(MenuPanel), true);
		}

	}


}