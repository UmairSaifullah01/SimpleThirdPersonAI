using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UMSTPA
{
    public class SelfAnimator : MonoBehaviour
    {
        protected Animator m_Animator;
        public float Jump
        {
            get
            {
                return GetAnimator ().GetFloat ("Jump");
            }
            set
            {
                GetAnimator ().SetFloat ("Jump", value);
            }
        }
        public float JumpLeg
        {
            get
            {
                return GetAnimator ().GetFloat ("JumpLeg");
            }
            set
            {
                GetAnimator ().SetFloat ("JumpLeg", value);
            }
        }
        public float PunchType
        {
            get
            {
                return GetAnimator ().GetFloat ("PunchType");
            }
            set
            {
                GetAnimator ().SetFloat ("PunchType", value);
            }
        }
        public float KickType
        {
            get
            {
                return GetAnimator ().GetFloat ("KickType");
            }
            set
            {
                GetAnimator ().SetFloat ("KickType", value);
            }
        }
        public float HurtType
        {
            get
            {
                return GetAnimator ().GetFloat ("HurtType");
            }
            set
            {
                GetAnimator ().SetFloat ("HurtType", value);
            }
        }
        public bool FightMode
        {
            get
            {
                return GetAnimator ().GetBool ("FightMode");
            }
            set
            {
                GetAnimator ().SetBool ("FightMode", value);
            }
        }
        public bool ShootMode
        {
            get
            {
                return GetAnimator ().GetBool ("ShootMode");
            }
            set
            {
                GetAnimator ().SetBool ("ShootMode", value);
            }
        }
        public bool IsDeath
        {
            get
            {
                return GetAnimator ().GetBool ("IsDeath");
            }
            set
            {
                GetAnimator ().SetBool ("IsDeath", value);
            }
        }
        public bool HurtMode
        {
            get
            {
                return GetAnimator ().GetBool ("HurtMode");
            }
            set
            {
                GetAnimator ().SetBool ("HurtMode", value);
            }
        }
        public bool IsPunch
        {
            set
            {
                GetAnimator ().SetTrigger ("IsPunch");
            }
        }
        public bool IsKick
        {
            set
            {
                GetAnimator ().SetTrigger ("IsKick");
            }
        }
        public bool Shoot
        {
            set
            {
                if (value)
                    GetAnimator ().SetTrigger ("Shoot");
            }
        }
        private Animator GetAnimator ()
        {
            if (m_Animator != null)
                return m_Animator;
            else
            {
                m_Animator = GetComponent<Animator> ();
                return m_Animator;
            }

        }
    }
}