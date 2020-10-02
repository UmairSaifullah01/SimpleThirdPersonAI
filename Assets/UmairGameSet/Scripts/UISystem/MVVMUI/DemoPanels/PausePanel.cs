using UMGS;
using UMUINew;


public class PausePanel : UIPanel
{

	public override void Init(IStateMachine stateMachine)
	{
		base.Init(stateMachine);
		EventBinder("PauseButton",Resume);
	}

	void Resume()
	{
	}

	void Restart()
	{
	}

	void Home()
	{
	}

}