using System;
using System.Collections;
using System.Collections.Generic;
using UMGS;
using UnityEngine;


// ReSharper disable once IdentifierTypo
// ReSharper disable once CheckNamespace
namespace UMUINew
{


	public class UIHandler : MonoBehaviour, IStateMachine
	{

		[SerializeField] Transform                  container;
		readonly         Dictionary<string, IState> cachedStates = new Dictionary<string, IState>();
		IState                                      currentState, previousState;
		readonly Stack<IState>                      anyStates = new Stack<IState>();
		public   bool                               isTransiting { get; private set; }

		void Awake()
		{
			var states = GetComponentsInChildren<IState>(true);
			foreach (var state in states)
			{
				cachedStates.Add(state.GetType().Name, state);
				state.Init(this);
			}

			LoadState(nameof(SplashPanel), Entry);
		}

		void Update()
		{
			StatesExecution();
		}


		public IState GetState(string id)
		{
			return cachedStates[id];
		}

		public void LoadState(string id, Action<IState> onStateLoad)
		{
			if (cachedStates.ContainsKey(id))
			{
				onStateLoad?.Invoke(cachedStates[id]);
			}
			else
			{
				AddressableManager.Get(id, container.transform, obj =>
				{
					IState state = obj.GetComponent<IState>();
					cachedStates.Add(state.GetType().Name, state);
					state.Init(this);
					onStateLoad?.Invoke(state);
				});
			}
		}

		public void Entry(IState state)
		{
			currentState = state;
			currentState?.Enter();
		}

		public void Transition(ITransition transition)
		{
			if (!isTransiting)
			{
				isTransiting = true;
				StartCoroutine(TransitionTo(transition));
			}
		}


		private IEnumerator TransitionTo(ITransition transition)
		{
			yield return new WaitForSecondsRealtime(currentState.exitTime);
			currentState?.Exit();
			previousState = currentState;
			currentState  = GetState(transition.toState);
			currentState?.Enter();
			isTransiting = false;
		}

		public void AnyTransition(IState state)
		{
			anyStates.Push(state);
			state.Enter();
		}

		public void ExitAnyStates()
		{
			if (anyStates.Count > 0)
				anyStates.Pop().Exit();
		}

		public void StatesExecution()
		{
			currentState?.Execute();
			foreach (var anyState in anyStates)
			{
				anyState?.Execute();
			}
		}

		public void Exit(IState state)
		{
			throw new System.NotImplementedException();
		}

	}


}