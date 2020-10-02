using System;


namespace UMGS
{


	public interface IState
	{

		float         exitTime     { get; }
		IStateMachine stateMachine { get; }

		void Init(IStateMachine stateMachine);

		void SetTransitions(params ITransition[] transitions);

		ITransition[] GetTransitions();

		void SetTransitionCondition(string stateName, bool value);

		void Execute();

		void Enter();

		void Exit();

	}

	public interface IBlendState : IState
	{

		IState subStates { get; set; }

	}

	public interface IStateMachine
	{

		bool isTransiting { get; }

		void LoadState(string id, Action<IState> onStateLoad);

		IState GetState(string id);

		void Entry(IState state);

		void Transition(ITransition transition);

		void AnyTransition(IState state);

		void ExitAnyStates();

		void StatesExecution();

		void Exit(IState state);

	}

	public interface ITransition
	{

		string toState   { get; }
		bool   condition { get; set; }

	}

	public static class Utils
	{

		public static string GetStateName(this IState state)
		{
			return state.GetType().Name;
		}

	}


}