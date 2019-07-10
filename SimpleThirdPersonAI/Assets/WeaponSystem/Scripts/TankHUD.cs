using UnityEngine;
using System.Collections;

public class TankHUD : MonoBehaviour {

	int currentWeapon;
	WeaponController weaponManager;
	void Start () {
		weaponManager = this.GetComponent<WeaponController>();
	}
	
	void Update () {
		if(!weaponManager)
			return;
		
		if(Input.GetKey(KeyCode.Escape)){
			Application.LoadLevel(0);	
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
			weaponManager.CurrentWeapon-=1;
			if(weaponManager.CurrentWeapon<0)
				weaponManager.CurrentWeapon = weaponManager.WeaponLists.Length-1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            weaponManager.CurrentWeapon+=1;
			if(weaponManager.CurrentWeapon>=weaponManager.WeaponLists.Length){
				weaponManager.CurrentWeapon = 0;
			}
        }
		currentWeapon = weaponManager.CurrentWeapon;
	}
	
	void OnGUI(){
		if(!weaponManager || currentWeapon>weaponManager.WeaponLists.Length)
			return;
		
		GUI.skin.label.fontSize = 15;
		GUI.Label(new Rect(20,20,300,30),"Weapon Index "+currentWeapon);
		GUI.Label(new Rect(20,80,300,30),"Esc back to mainmenu");
		GUI.Label(new Rect(20,Screen.height-50,300,30),"Scroll Mouse to Change weapons");
		GUI.Label(new Rect(20,Screen.height-70,300,30),"W A S D to Move");
		
		GUI.skin.label.fontSize = 25;
		GUI.Label(new Rect(20,40,300,50),""+weaponManager.WeaponLists[currentWeapon].name);
		
	
	}
}
