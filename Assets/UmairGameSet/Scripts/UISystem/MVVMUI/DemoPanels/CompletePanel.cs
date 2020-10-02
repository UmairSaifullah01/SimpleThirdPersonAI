using UMGS;


namespace UMUINew
{


	public class CompletePanel : UIPanel
	{

		public override void Init(IStateMachine stateMachine)
		{
			base.Init(stateMachine);
			EventBinder("HomeButton",    HomeButton);
			EventBinder("NextButton",    NextButton);
			EventBinder("RestartButton", RestartButton);
		}

		void RestartButton()
		{
			EventHandler.OnGameRestart.Send();
		}

		void NextButton()
		{
		}

		void HomeButton()
		{
			EventHandler.OnGameToHome.Send();
		}

	}


}