using System;
using UnityEditor;
using UnityEngine;

public class LightBakeEditor : EditorWindow
{
	Renderer[] AllRenderers;float lightbakescale = 1;
	[MenuItem("Window/LightBake")] 
	static void Init()
	{
		var window=GetWindow<LightBakeEditor>();
		window.titleContent = new GUIContent("Light Bake");
		
	}

	void OnGUI()
	{
		AllRenderers = FindObjectsOfType<Renderer>();
		lightbakescale = EditorGUILayout.FloatField("Light bake scale Detail", lightbakescale);
		if (GUILayout.Button("SetScale"))
		{
			foreach (Renderer renderer in AllRenderers)
			{
				SerializedObject so = new SerializedObject (renderer);
				so.FindProperty("m_ScaleInLightmap").floatValue = lightbakescale;
				so.ApplyModifiedProperties();
			}
		}
	}

}
