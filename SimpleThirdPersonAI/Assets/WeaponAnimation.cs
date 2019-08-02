using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : ScriptableObject
{
    [Header ("General")]
    public AnimationClip grounded;
    public AnimationClip crouching, airBorne;
    [Header ("Weapon")]
    public AnimationClip shoot;
    public AnimationClip reload, idle;




#if UNITY_EDITOR
    [UnityEditor.MenuItem ("Assets/Create/ScriptableObject/WeaponAnimation")]
    public static void CreateAsset ()
    {
        ScriptableObjectUtility.CreateAsset<WeaponAnimation> ();
    }
#endif
}
