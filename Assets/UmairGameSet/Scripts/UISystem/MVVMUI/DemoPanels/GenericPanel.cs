using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UMGS;
using UMUI;
using UnityEngine;
using UnityEngine.Events;


namespace UMUINew
{


	public class GenericPanel : UIPanel
	{

		GenericPanelData   panelData;
		public UnityAction positive;
		public UnityAction negative;
		GameObject         positiveGameObject;

		public override void Init(IStateMachine stateMachine)
		{
			base.Init(stateMachine);
			positiveGameObject = views.FirstOrDefault(x => x.Id == "PositiveButton")?.GetTransform().gameObject;
		}

		public override void Enter()
		{
			base.Enter();
			panelData = (GenericPanelData) storageService.Load<GenericPanelData>();
			StringBinder("DescriptionText", panelData.message);
			StringBinder("TitleText",       panelData.title);
			StringBinder("NegativeText",    panelData.negativeText);
			EventBinder("NegativeButton", Negative);
			negative = panelData.negativeEvent;
			if (panelData.isPositiveButton)
			{
				StringBinder("PositiveText", panelData.positiveText);
				EventBinder("PositiveButton", Positive);
				positive = panelData.positiveEvent;
			}

			positiveGameObject.SetActive(panelData.isPositiveButton);
		}

		void Positive()
		{
			Debug.Log("PositiveButton");
			positive?.Invoke();
		}

		void Negative()
		{
			Debug.Log("NegativeButton");
			negative?.Invoke();
			Exit();
		}

	}


}