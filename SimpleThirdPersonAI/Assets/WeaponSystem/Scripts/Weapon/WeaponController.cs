using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    public GameObject[] WeaponLists;
    public int CurrentWeapon = 0;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetButton ("Fire1"))
        {
            LaunchWeapon (CurrentWeapon);
        }
    }
	
	public void LaunchWeapon(int index){
		CurrentWeapon = index;
		if(CurrentWeapon < WeaponLists.Length && WeaponLists[index] != null){
			WeaponLists[index].gameObject.GetComponent<WeaponLauncher>().Shoot();
		}
	}
	public void LaunchWeapon(){
		if(CurrentWeapon < WeaponLists.Length && WeaponLists[CurrentWeapon] != null){
			WeaponLists[CurrentWeapon].gameObject.GetComponent<WeaponLauncher>().Shoot();
		}
	}
}
