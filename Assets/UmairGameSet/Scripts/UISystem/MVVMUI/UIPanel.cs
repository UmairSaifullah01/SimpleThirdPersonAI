using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UMGS;
using UMUI;
using UnityEngine;


namespace UMUINew
{


	// ReSharper disable once InconsistentNaming
	public abstract class UIPanel : MonoBehaviour, IState, IViewModel
	{

		List<ITransition>                   cachedTransitions = new List<ITransition>();
		public event Action<string, string> stringBinder;
		public event Action<string, float>  floatBinder;
		public event Action<string, Action> eventBinder;
		public IView[]                      views          { get; set; }
		public IStorageService              storageService { get; set; }


		public float         exitTime     { get; protected set; }
		public IStateMachine stateMachine { get; private set; }


		public virtual void Init(IStateMachine stateMachine)
		{
			storageService    = LocalStorageService.GetService();
			this.stateMachine = stateMachine;
			InitViewModel();
			gameObject.SetActive(false);
		}

		public void SetTransitions(params ITransition[] transitions)
		{
			foreach (var transition in transitions)
			{
				stateMachine.LoadState(transition.toState, null);
			}

			this.cachedTransitions = transitions.ToList();
		}

		public ITransition[] GetTransitions() => cachedTransitions.ToArray();

		public void SetTransitionCondition(string stateName, bool value)
		{
			var trans                          = cachedTransitions.FirstOrDefault(x => x.toState == stateName);
			if (trans != null) trans.condition = value;
		}

		public virtual void Execute()
		{
			var executableTransition = cachedTransitions.FirstOrDefault(x => x.condition);
			if (executableTransition != null)
			{
				stateMachine.Transition(executableTransition);
			}
		}

		public virtual void Enter()
		{
			gameObject.SetActive(true);
			transform.SetAsLastSibling();
		}

		public virtual void Exit()
		{
			gameObject.SetActive(false);
			cachedTransitions.ForEach(x => x.condition = false);
		}

		public virtual void InitViewModel()
		{
			if (views == null)
			{
				views = GetComponentsInChildren<IView>(true);
			}

			foreach (IView view in views)
			{
				view.Init(this);
			}
		}


		protected void StringBinder(string Id, string value)
		{
			stringBinder?.Invoke(Id, value);
		}

		protected void FloatBinder(string Id, float value)
		{
			floatBinder?.Invoke(Id, value);
		}

		protected void EventBinder(string Id, Action value)
		{
			eventBinder?.Invoke(Id, value);
		}

	}


}