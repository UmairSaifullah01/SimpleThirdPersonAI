using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	public Texture2D logo;
	void Start () {
	
	}

	void Update () {
		Screen.lockCursor = false;
	}
	
	void OnGUI(){
		GUI.skin.button.fontSize = 20;
		GUI.DrawTexture(new Rect(Screen.width/2 - logo.width/2,10,logo.width,logo.height),logo);
		if(GUI.Button(new Rect(Screen.width/2 - 400,300,300,50),"Demo 1"))
			Application.LoadLevel("Demo1");
		if(GUI.Button(new Rect(Screen.width/2 - 400,360,300,50),"Demo 2"))	
			Application.LoadLevel("Demo2");
		if(GUI.Button(new Rect(Screen.width/2 - 400,420,300,50),"Demo 3"))	
			Application.LoadLevel("Demo3");
		if(GUI.Button(new Rect(Screen.width/2 - 400,480,300,50),"Get this project"))
			Application.OpenURL("https://www.assetstore.unity3d.com/#/content/7676");
		GUI.skin.label.fontSize = 14;
		GUI.Label(new Rect(20,Screen.height-60,300,50),"Weapon System 3.0 By Rachan Neamprasert www.hardworkerstudio.com");
		
		
	}
}
