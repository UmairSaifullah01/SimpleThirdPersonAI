using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UMGS;
using UnityEngine.Networking;

[System.Serializable]
[RequireComponent(typeof(Animator))]
public class IKDriver : MonoBehaviour
{
    //reference to the animator component to call IK functions
    protected Animator animator;

    //the look target transform position.x value
    float lookTargetPosX;

    Vector3 lookPosition;

    //the starting look target transform.x value
    [HideInInspector] public float defaultLookXPos;

    //the maximum distance the look target can move right
    [HideInInspector] public float maxLookRight;

    //the maximum distance the look target can move right
    [HideInInspector] public float maxLookLeft;

    //the speed the look object will move wheen steering
    [HideInInspector] public float minLookSpeed;

    //the snap back speed the look object will use when not steering
    [HideInInspector] public float maxLookSpeed;

    //the speed at which the look target object will move
    private float lookObjMoveSpeed;

    //used to determine when the right hand should target a steering wheel target or shift target
    private bool shifting;

    //enable/disable IK control of the avatar
    public bool ikActive = false;

    //maximum rotation of steering wheel transform on x axis
    public float steeringWheelRotation;

    public Transform steeringWheel;

    //the local transform position of the avatar, set in the Start method
    public Vector3 avatarPosition;

    //set this bool to true to trigger a shift
    public bool shift;

    //IK driver targets
    [HideInInspector] public Transform targetRightHandIK;
    [HideInInspector] public Transform rightHandTarget;
    [HideInInspector] public Transform targetLeftHandIK;
    [HideInInspector] public Transform targetRightFootIK;
    [HideInInspector] public Transform targetLeftFootIK;
    [HideInInspector] public Transform lookObj;
    [HideInInspector] public Transform rightHandObj;
    [HideInInspector] public Transform leftHandObj;
    [HideInInspector] public Transform rightFootObj;

    [HideInInspector] public Transform leftFootObj;

    //steering wheel targets
    [HideInInspector] public Transform steeringW;
    [HideInInspector] public Transform steeringNW;
    [HideInInspector] public Transform steeringN;
    [HideInInspector] public Transform steeringNE;
    [HideInInspector] public Transform steeringE;
    [HideInInspector] public Transform steeringS;
    [HideInInspector] public Transform steeringSE;

    [HideInInspector] public Transform steeringSW;

    //otherIK target objects
    [HideInInspector] public Transform shiftObj;
    [HideInInspector] public Transform leftFootIdle;
    [HideInInspector] public Transform leftFootClutch;
    [HideInInspector] public Transform rightFootBrake;
    [HideInInspector] public Transform rightFootIdle;
    [HideInInspector] public Transform rightFootGas;
    
    private float yVelocity;
    public bool mobile;
    public Vector3 defaultSteering;
    public float horizontalInput;
    public float verticalInput;
    private string gameMode;

    void Start()
    {
        transform.localPosition = avatarPosition;
        animator = GetComponent<Animator>();
        lookTargetPosX = defaultLookXPos;
    }

    void Update()
    {
        if (mobile)
        {
            horizontalInput = -Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }

        if (shift)
        {
            shift = false;
            TargetShifter();
        }

        if (steeringWheel != null)
        {
            Vector3 temp2;
            temp2 = new Vector3(defaultSteering.x, defaultSteering.y, -(horizontalInput * steeringWheelRotation));
            float zAngle = Mathf.SmoothDampAngle(steeringWheel.localEulerAngles.z, temp2.z, ref yVelocity, 0.07f);
            steeringWheel.localEulerAngles =
                new Vector3(defaultSteering.x, defaultSteering.y,
                    zAngle); //temp2;//new Vector3 (30.8f, 0, -(Input.GetAxis ("Horizontal") * steeringWheelRotation));
        }
    }



    public void TargetWheel()
    {
        shifting = false;
        targetRightHandIK = rightHandTarget;
    }

    public void TargetShifter()
    {
        shifting = true;
        targetRightHandIK = shiftObj;
        leftFootObj = leftFootClutch;
        this.AfterWait(TargetWheel, 0.35f);
        this.AfterWait(LeftFootIdle, 0.5f);
    }

    public void LeftFootIdle()
    {
        leftFootObj = leftFootIdle;
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            if (ikActive)
            {
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, targetLeftHandIK.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, targetLeftHandIK.rotation);

                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, targetRightHandIK.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, targetRightHandIK.rotation);

                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, targetLeftFootIK.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, targetLeftFootIK.rotation);

                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, targetRightFootIK.position);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, targetRightFootIK.rotation);

                    lookPosition = lookObj.localPosition;


                    if (horizontalInput > 0)
                    {
                        if (horizontalInput >= 0.75f)
                        {
                            rightHandObj = steeringNW;
                            leftHandObj = steeringSE;
                        }
                        else if (horizontalInput >= 0.5f)
                        {
                            rightHandObj = steeringN;
                            leftHandObj = steeringS;
                        }
                        else
                        {
                            rightHandObj = steeringNE;
                            leftHandObj = steeringSW;
                        }

                        lookTargetPosX = defaultLookXPos + maxLookRight;
                        lookObjMoveSpeed = minLookSpeed;
                    }
                    else if (horizontalInput < 0)
                    {
                        if (horizontalInput <= -0.75f)
                        {
                            rightHandObj = steeringSW;
                            leftHandObj = steeringNE;
                        }
                        else if (horizontalInput <= -0.5f)
                        {
                            rightHandObj = steeringS;
                            leftHandObj = steeringN;
                        }
                        else
                        {
                            rightHandObj = steeringSE;
                            leftHandObj = steeringNW;
                        }

                        lookTargetPosX = defaultLookXPos + maxLookLeft;
                        lookObjMoveSpeed = minLookSpeed;
                    }
                    else
                    {
                        rightHandObj = steeringE;
                        leftHandObj = steeringW;
                        lookTargetPosX = defaultLookXPos;
                        if (Mathf.Approximately(lookPosition.x, lookTargetPosX))
                        {
                            lookObjMoveSpeed = minLookSpeed;
                        }
                        else
                        {
                            lookObjMoveSpeed = Mathf.Lerp(lookObjMoveSpeed, maxLookSpeed, 1 * Time.deltaTime);
                        }
                    }

                    if (verticalInput > 0)
                    {
                        rightFootObj = rightFootGas;
                    }
                    else if (verticalInput < 0)
                    {
                        // if (gearString == "R")
                        // {
                        //     rightFootObj = rightFootGas;
                        // }
                        // else
                        // {
                            rightFootObj = rightFootBrake;
                        // }
                    }
                    else
                    {
                        rightFootObj = rightFootIdle;
                    }

                    targetRightFootIK.localPosition = Vector3.Lerp(targetRightFootIK.localPosition,
                        rightFootObj.localPosition, 8 * Time.deltaTime);
                    targetRightFootIK.localRotation = Quaternion.Lerp(targetRightFootIK.localRotation,
                        rightFootObj.localRotation, 8 * Time.deltaTime);

                    targetLeftFootIK.localPosition = Vector3.Lerp(targetLeftFootIK.localPosition,
                        leftFootObj.localPosition, 8 * Time.deltaTime);
                    targetLeftFootIK.localRotation = Quaternion.Lerp(targetLeftFootIK.localRotation,
                        leftFootObj.localRotation, 8 * Time.deltaTime);

                    targetLeftHandIK.localPosition = Vector3.Lerp(targetLeftHandIK.localPosition,
                        leftHandObj.localPosition, 8 * Time.deltaTime);
                    targetLeftHandIK.localRotation = Quaternion.Lerp(targetLeftHandIK.localRotation,
                        leftHandObj.localRotation, 8 * Time.deltaTime);

                    if (shifting == false)
                    {
                        targetRightHandIK.localPosition = Vector3.Lerp(targetRightHandIK.localPosition,
                            rightHandObj.localPosition, 8 * Time.deltaTime);
                        targetRightHandIK.localRotation = Quaternion.Lerp(targetRightHandIK.localRotation,
                            rightHandObj.localRotation, 8 * Time.deltaTime);
                    }

                    lookPosition.x = Mathf.Lerp(lookPosition.x, lookTargetPosX, lookObjMoveSpeed * Time.deltaTime);
                    lookObj.localPosition = lookPosition;
                }
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
            }
        }
    }
}