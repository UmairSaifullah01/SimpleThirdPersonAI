using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : ILocomotion
{
    Animator animator;
    Rigidbody rigidbody;
    CapsuleCollider collider;
    Vector3 targetDirection;
    Transform transform;
    bool stopMove = false;
    private float speed = 0;
    Vector2 movemet = Vector2.zero;
    public IInput input
    {
        get; set;
    }

    public void DoUpdate ()
    {
        movemet.x = input.GetAxis ("Horizontal");
        movemet.y = input.GetAxis ("Vertical");
        targetDirection = movemet.x * transform.right + movemet.y * transform.forward;
        targetDirection.y = 0;
        speed = Mathf.Abs (movemet.x) + Mathf.Abs (movemet.y);
        speed = Mathf.Clamp01 (speed);
    }

    protected virtual void FreeVelocity (float velocity)
    {
        var _targetVelocity = transform.forward * velocity * speed;
        _targetVelocity.y = rigidbody.velocity.y;
        rigidbody.velocity = _targetVelocity;
    }
    public void InitLocomotion (Transform transform)
    {
        animator = transform.GetComponent<Animator> ();
        rigidbody = transform.GetComponent<Rigidbody> ();
        collider = transform.GetComponent<CapsuleCollider> ();
        this.transform = transform;
        input = new UserInput ();
        targetDirection = Vector3.zero;

    }
    public virtual void ControlSpeed (float velocity)
    {
        if (Time.deltaTime == 0)
            return;


        if (movemet.magnitude > 0.1f)
        {

            var deltaPosition = new Vector3 (animator.deltaPosition.x, transform.position.y, animator.deltaPosition.z);
            Vector3 v = ( deltaPosition * ( velocity > 0 ? velocity : 1f ) ) / Time.deltaTime;
            v.y = rigidbody.velocity.y;
            rigidbody.velocity = Vector3.Lerp (rigidbody.velocity, v, 20f * Time.deltaTime);
        }
        else
        {


            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            transform.rotation = animator.rootRotation;
            transform.position = animator.rootPosition;

        }
    }
    public void OnAnimatorMove ()
    {
        var dir = transform.InverseTransformDirection (targetDirection);
        dir.Normalize ();
        dir.z *= speed;
        dir.x = Mathf.Clamp ( dir.x, -0.25f, 0.25f);
        animator.SetFloat ("InputVertical", !stopMove ? dir.z : 0f, 0.25f, Time.deltaTime);
        animator.SetFloat ("InputHorizontal", !stopMove ? dir.x : 0f, 0.25f, Time.deltaTime);
        animator.SetFloat ("InputMagnitude", speed, .2f, Time.deltaTime);
        ControlSpeed (speed);

    }


}
