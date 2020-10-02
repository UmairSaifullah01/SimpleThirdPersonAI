using System;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

    //Parts
    IInput input
    {
        get; set;
    }
    ILocomotion locomotion
    {
        get; set;
    }
    ISkill[] skills
    {
        get; set;
    }
    private void Start ()
    {
        locomotion = new Locomotion ();
        locomotion.InitLocomotion (transform);
    }
    private void FixedUpdate ()
    {
        locomotion.DoUpdate ();
    }
    protected virtual void OnAnimatorMove ()
    {
        locomotion.OnAnimatorMove (); 
    }

    //Functions

}
//controls inputs functions controls like sensors

public interface IInput
{

    bool GetButtonDown (string buttonName);

    float GetAxis (string axisName);

}

//Controls all movement actions
public interface ILocomotion
{

    IInput input
    {
        get;
    }
    void InitLocomotion (Transform transform);
    void DoUpdate ();
    void OnAnimatorMove ();

}

public interface IFlyingLocomotion : ILocomotion
{

}

public interface ISkill
{

}

public interface IShooter : ISkill
{

}

public interface IFighter : ISkill
{

}

public interface IMeleeFighter : IFighter
{

}

public interface ICoverShooter : IShooter
{

}

public interface IHealthSystem
{

    float maximumHealth
    {
        get;
    }
    float currentHealth
    {
        get;
    }
    bool isAlive
    {
        get; set;
    }
    event Action OnDeath;
    event Action<float> OnTakeDamage;
    event Action<float> OnHealDamage;
    event Action OnRevive;

    void TakeDamage (float damageAmount);

    void HealDamage (float damageAmount);

    void Revive ();

}