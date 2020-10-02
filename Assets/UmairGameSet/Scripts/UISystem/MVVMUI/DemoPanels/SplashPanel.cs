using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UMGS;
using UnityEngine;


namespace UMUINew
{


	public class SplashPanel : UIPanel
	{

		public override void Init(IStateMachine stateMachine)
		{
			base.Init(stateMachine);
			exitTime = 3;
			SetTransitions(new Transition(nameof(MenuPanel)));
			
		}

		public override void Enter()
		{
			base.Enter();
			SetTransitionCondition(nameof(MenuPanel), true);
		}

	}


}