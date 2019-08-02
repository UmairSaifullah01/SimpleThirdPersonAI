using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    protected WeaponAnimation animations;
    public abstract void Shoot ();

    // Start is called before the first frame update
    private void Start ()
    {
        Initialize ();
    }
    public abstract void Initialize ();

    public abstract void DoUpdate ();

    // Update is called once per frame
    private void Update ()
    {
        DoUpdate ();
    }
    public abstract void SetAnimations (ref AnimatorOverrideController animController);
}
