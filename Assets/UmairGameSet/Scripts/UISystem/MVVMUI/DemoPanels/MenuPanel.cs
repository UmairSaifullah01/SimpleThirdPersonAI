using System.Collections;
using System.Collections.Generic;
using UMGS;
using UnityEngine;


namespace UMUINew
{


	public class MenuPanel : UIPanel
	{

		public override void Init(IStateMachine stateMachine)
		{
			base.Init(stateMachine);
			EventBinder("PlayButton",      Play);
			EventBinder("MoreGamesButton", MoreGames);
			EventBinder("SettingsButton",  Settings);
			SetTransitions(new Transition(nameof(ExitPanel)));
		}


		public override void Execute()
		{
			base.Execute();
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				SetTransitionCondition(nameof(ExitPanel), true);
			}
		}

		void Play()
		{
			Debug.Log("Play Button Pressed");
		}

		void MoreGames()
		{
			Debug.Log("Moregames Button Pressed");
		}

		void Settings()
		{
			Debug.Log("Settings Button Pressed");
		}

	}


}