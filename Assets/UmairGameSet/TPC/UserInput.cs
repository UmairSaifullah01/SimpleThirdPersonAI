using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : IInput
{
    public float GetAxis (string axisName) => Input.GetAxis (axisName);


    public bool GetButtonDown (string buttonName) => Input.GetButtonDown (buttonName);

}
