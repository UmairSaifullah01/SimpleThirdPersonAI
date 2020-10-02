using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


namespace UMGS
{


	[CanEditMultipleObjects]
	[CustomEditor(typeof(UBehaviour), true)]
	public class UEditorBase : Editor
	{

		#region Variables

		public string[]              ignoreEvents;
		public string[]              notEventProperties;
		public string[]              ignore_vMono = new string[] {"openCloseWindow", "openCloseEvents", "selectedToolbar"};
		public UClassHeaderAttribute headerAttribute;
		public GUISkin               skin;
		public Texture2D             m_Logo;
		public List<vToolBar>        toolbars;

		#endregion


		public class vToolBar
		{

			public string       title;
			public List<string> variables;

			public vToolBar()
			{
				title     = string.Empty;
				variables = new List<string>();
			}

		}

		protected virtual void OnEnable()
		{
			var targetObject       = serializedObject.targetObject;
			var hasAttributeHeader = targetObject.GetType().IsDefined(typeof(UClassHeaderAttribute), true);
			if (hasAttributeHeader)
			{
				var attributes = Attribute.GetCustomAttributes(targetObject.GetType(), typeof(UClassHeaderAttribute), true);
				if (attributes.Length > 0)
					headerAttribute = (UClassHeaderAttribute) attributes[0];
			}

			skin   = Resources.Load("skin") as GUISkin;
			m_Logo = Resources.Load("icon_v2") as Texture2D;
			var prop = serializedObject.GetIterator();
			if (((UBehaviour) target) != null)
			{
				const BindingFlags flags  = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
				List<string>       events = new List<string>();
				toolbars = new List<vToolBar>();
				var toolbar = new vToolBar();
				toolbar.title = "Default";
				toolbars.Add(toolbar);
				var index = 0;
				while (prop.NextVisible(true))
				{
					var fieldInfo = targetObject.GetType().GetField(prop.name, flags);
					if (fieldInfo != null)
					{
						var toolBarAttributes = fieldInfo.GetCustomAttributes(typeof(UToolbarAttribute), true);
						if (toolBarAttributes.Length > 0)
						{
							var attribute = toolBarAttributes[0] as UToolbarAttribute;
							var _toolbar  = toolbars.Find(tool => tool != null && tool.title == attribute.title);
							if (_toolbar == null)
							{
								toolbar       = new vToolBar();
								toolbar.title = attribute.title;
								toolbars.Add(toolbar);
								index = toolbars.Count - 1;
							}
							else index = toolbars.IndexOf(_toolbar);
						}

						if (index < toolbars.Count)
							toolbars[index].variables.Add(prop.name);
						if ((UEditorHelper.IsUnityEventyType(fieldInfo.FieldType) && !events.Contains(fieldInfo.Name)))
						{
							events.Add(fieldInfo.Name);
						}
					}
				}

				var nullToolBar = toolbars.FindAll(tool => tool != null && (tool.variables == null || tool.variables.Count == 0));
				for (int i = 0; i < nullToolBar.Count; i++)
				{
					if (toolbars.Contains(nullToolBar[i]))
						toolbars.Remove(nullToolBar[i]);
				}

				ignoreEvents = UMTools.ToArray(events);
				if (headerAttribute != null)
					m_Logo = Resources.Load(headerAttribute.iconName) as Texture2D;
				//else headerAttribute = new vClassHeaderAttribute(target.GetType().Name);
			}
		}

		protected bool openCloseWindow
		{
			get { return serializedObject.FindProperty("openCloseWindow").boolValue; }
			set
			{
				var _openClose = serializedObject.FindProperty("openCloseWindow");
				if (_openClose != null && value != _openClose.boolValue)
				{
					_openClose.boolValue = value;
					serializedObject.ApplyModifiedProperties();
				}
			}
		}

		protected bool openCloseEvents
		{
			get
			{
				var _openCloseEvents = serializedObject.FindProperty("openCloseEvents");
				return _openCloseEvents != null ? _openCloseEvents.boolValue : false;
			}
			set
			{
				var _openCloseEvents = serializedObject.FindProperty("openCloseEvents");
				if (_openCloseEvents != null && value != _openCloseEvents.boolValue)
				{
					_openCloseEvents.boolValue = value;
					serializedObject.ApplyModifiedProperties();
				}
			}
		}

		protected int selectedToolBar
		{
			get
			{
				var _selectedToolBar = serializedObject.FindProperty("selectedToolbar");
				return _selectedToolBar != null ? _selectedToolBar.intValue : 0;
			}
			set
			{
				var _selectedToolBar = serializedObject.FindProperty("selectedToolbar");
				if (_selectedToolBar != null && value != _selectedToolBar.intValue)
				{
					_selectedToolBar.intValue = value;
					serializedObject.ApplyModifiedProperties();
				}
			}
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			if (toolbars != null && toolbars.Count > 1)
			{
				GUILayout.BeginVertical(headerAttribute != null ? headerAttribute.header : target.GetType().Name, skin.window);
				GUILayout.Label(m_Logo, skin.label, GUILayout.MaxHeight(30));
				GUILayout.Space(10);
				if (headerAttribute.openClose)
				{
					openCloseWindow = GUILayout.Toggle(openCloseWindow, openCloseWindow ? "Close Properties" : "Open Properties", EditorStyles.toolbarButton);
				}

				if (!headerAttribute.openClose || openCloseWindow)
				{
					var titles = getToobarTitles();
					EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
					GUILayout.Space(10);
					var customToolbar = skin.GetStyle("customToolbar");
					selectedToolBar = GUILayout.SelectionGrid(selectedToolBar, titles, 3, customToolbar, GUILayout.MinWidth(250));
					if (!(selectedToolBar < toolbars.Count)) selectedToolBar = 0;
					GUILayout.Space(10);
					GUILayout.Box(toolbars[selectedToolBar].title, skin.box, GUILayout.ExpandWidth(true));
					var ignore           = getIgnoreProperties(toolbars[selectedToolBar]);
					var ignoreProperties = ignore.Append(ignore_vMono);
					DrawPropertiesExcluding(serializedObject, ignoreProperties);
				}

				GUILayout.EndVertical();
			}
			else
			{
				if (headerAttribute == null)
				{
					if (((UBehaviour) target) != null)
						DrawPropertiesExcluding(serializedObject, ignore_vMono);
					else
						base.OnInspectorGUI();
				}
				else
				{
					GUILayout.BeginVertical(headerAttribute.header, skin.window);
					GUILayout.Label(m_Logo, skin.label, GUILayout.MaxHeight(40));
					GUILayout.Space(10);
					if (headerAttribute.openClose)
					{
						openCloseWindow = GUILayout.Toggle(openCloseWindow, openCloseWindow ? "Close Properties" : "Open Properties", EditorStyles.toolbarButton);
					}

					if (!headerAttribute.openClose || openCloseWindow)
					{
						if (headerAttribute.useHelpBox)
							EditorGUILayout.HelpBox(headerAttribute.helpBoxText, MessageType.Info);
						if (ignoreEvents != null && ignoreEvents.Length > 0)
						{
							var ignoreProperties = ignoreEvents.Append(ignore_vMono);
							DrawPropertiesExcluding(serializedObject, ignoreProperties);
							openCloseEvents = GUILayout.Toggle(openCloseEvents, (openCloseEvents ? "Close " : "Open ") + "Events ", skin.button);
							if (openCloseEvents)
							{
								foreach (string propName in ignoreEvents)
								{
									var prop = serializedObject.FindProperty(propName);
									if (prop != null)
										EditorGUILayout.PropertyField(prop);
								}
							}
						}
						else
						{
							var ignoreProperties = ignoreEvents.Append(ignore_vMono);
							DrawPropertiesExcluding(serializedObject, ignoreProperties);
						}
					}

					EditorGUILayout.EndVertical();
				}
			}

			if (GUI.changed)
			{
				serializedObject.ApplyModifiedProperties();
				EditorUtility.SetDirty(serializedObject.targetObject);
			}
		}

		public string[] getToobarTitles()
		{
			List<string> props = new List<string>();
			for (int i = 0; i < toolbars.Count; i++)
			{
				if (toolbars[i] != null)
					props.Add(toolbars[i].title);
			}

			return UMTools.ToArray(props);
		}

		public string[] getIgnoreProperties(vToolBar toolbar)
		{
			List<string> props = new List<string>();
			for (int i = 0; i < toolbars.Count; i++)
			{
				if (toolbars[i] != null && toolbar != null && toolbar.variables != null)
				{
					for (int a = 0; a < toolbars[i].variables.Count; a++)
					{
						if (!props.Contains(toolbars[i].variables[a]) && !toolbar.variables.Contains(toolbars[i].variables[a]))
						{
							props.Add(toolbars[i].variables[a]);
						}
					}
				}
			}

			props.Add("m_Script");
			return UMTools.ToArray(props);
		}

	}


}