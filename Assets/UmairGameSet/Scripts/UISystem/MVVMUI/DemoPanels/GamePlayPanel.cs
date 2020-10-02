using UMGS;


namespace UMUINew
{


	public class GamePlayPanel : UIPanel
	{

		public override void Init(IStateMachine stateMachine)
		{
			base.Init(stateMachine);
			EventBinder("PauseButton", PauseButton);
			SetTransitions(new Transition(nameof(PausePanel)), new Transition(nameof(CompletePanel)), new Transition(nameof(FailPanel)));

			EventHandler.LevelFail.AddListener(() => SetTransitionCondition(nameof(FailPanel), true));
		}
		

		void PauseButton()
		{
			SetTransitionCondition(nameof(PausePanel), true);
		}

	}


}