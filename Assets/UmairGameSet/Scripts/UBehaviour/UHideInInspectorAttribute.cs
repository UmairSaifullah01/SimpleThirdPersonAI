using UnityEngine;
using System.Collections;
using System;
[AttributeUsage(AttributeTargets.All, Inherited = true,AllowMultiple =true)]
public class UHideInInspectorAttribute : PropertyAttribute
{   
	public string refbooleanProperty;
    public bool invertValue;
    public UHideInInspectorAttribute(string refbooleanProperty,bool invertValue = false)
    {        
        this.refbooleanProperty = refbooleanProperty;
        this.invertValue = invertValue;     
    }
}
