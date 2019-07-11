using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UMSTPA
{
    public enum CharactorType
    {
        Fighter, Shooter
    }
    public class FighterShooter : ThirdPersonCharacter
    {
        [Header ("Fight OR Shoot")]
        [SerializeField] public CharactorType m_Type;
        [SerializeField] private WeaponBehaviour[] Weapons;
        private SelfAnimator animatorParametors;
        private float punchCounter = 0, kickCounter = 0, kicksAndPunches = 0;
        private bool IsShooting = false;
        // Start is called before the first frame update
        protected override void Initialize ()
        {
            base.Initialize ();
            animatorParametors = GetComponent<SelfAnimator> ();
            if (m_Type == CharactorType.Fighter)
                foreach (var item in Weapons)
                {
                    item.gameObject.SetActive (false);
                }
        }
        public void Reset ()
        {
            animatorParametors.FightMode = false;
            animatorParametors.ShootMode = false;
        }
        public void Fight (bool isFight)
        {

            kicksAndPunches = ( isFight ) ? kicksAndPunches > 4 ? 0 : kicksAndPunches + Time.deltaTime : 0;

            if (kicksAndPunches <= 2)
            {
                kickCounter = 0;
                Fight (isFight, true, false);
            }
            else
            {
                punchCounter = 0;
                Fight (isFight, false, true);
            }

        }
        public void Fight (bool isFight, bool isPunch, bool isKick)
        {
            if (isFight)
                animatorParametors.FightMode = isFight;

            if (animatorParametors.FightMode)
            {
                animatorParametors.IsKick = isPunch ? false : isKick;
                animatorParametors.IsPunch = isKick ? false : isPunch;
                punchCounter -= Time.deltaTime / 3;
                kickCounter -= Time.deltaTime / 3;
                if (isPunch)
                {
                    if (punchCounter > 1f)
                        punchCounter = 0;
                    punchCounter += Time.deltaTime / 2;

                }
                if (isKick)
                {
                    if (kickCounter > 1f)
                        kickCounter = 0;
                    kickCounter += Time.deltaTime / 2;
                }
                animatorParametors.PunchType = Mathf.Clamp (punchCounter, 0, 1f);
                animatorParametors.KickType = Mathf.Clamp (kickCounter, 0, 1f);
            }
            else
            {
                punchCounter = 0;
                animatorParametors.PunchType = punchCounter;
                animatorParametors.KickType = kickCounter;
            }
            if (punchCounter == 0 && kickCounter == 0)
                animatorParametors.FightMode = false;
        }

        public void ShootEnemy (Transform target, bool isShoot)
        {

            animatorParametors.ShootMode = isShoot;
            IsShooting = true;
            if (isShoot)
            {
                animatorParametors.Shoot = isShoot;
            }

        }
        public override void Move (Vector3 move, bool crouch, bool jump)
        {
            base.Move (move, crouch, jump);

        }
        public Coroutine AfterWait (Action action, float seconds, bool realTime = false)
        {
            return StartCoroutine (AfterWaitCoroutine (action, seconds, realTime));
        }
        IEnumerator AfterWaitCoroutine (Action action, float seconds, bool realTime)
        {
            if (realTime)
                yield return new WaitForSecondsRealtime (seconds);
            else
                yield return new WaitForSeconds (seconds);
            action.Invoke ();
        }
        public void Shoot ()
        {
            Weapons[0].Shoot ();
            if (IsShooting)
            {
                IsShooting = false;
                animatorParametors.ShootMode = IsShooting;
            }

        }
        public void EnableDamage ()
        {

        }
    }
}