using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UMSTPA
{
    public enum CharactorType
    {
        Fighter, Shooter, Both
    }
    public class FighterShooter : ThirdPersonCharacter
    {
        [SerializeField] private CharactorType m_Type;
        [SerializeField] private GameObject[] Weapons;
        private SelfAnimator animatorParametors;
        private float punchCounter = 0, kickCounter = 0;

        // Start is called before the first frame update
        protected override void Initialize ()
        {
            base.Initialize ();
            animatorParametors = GetComponent<SelfAnimator> ();
            if (m_Type == CharactorType.Fighter)
                foreach (var item in Weapons)
                {
                    item.SetActive (false);
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
                punchCounter -= Time.deltaTime;
                kickCounter -= Time.deltaTime;
                if (isPunch)
                {
                    if (punchCounter > 3)
                        punchCounter = 0;
                    punchCounter += 1;

                }
                if (isKick)
                {
                    if (kickCounter > 3)
                        kickCounter = 0;
                    kickCounter += 1;
                }
                punchCounter = Mathf.Clamp (punchCounter, 0, 3.5f);
                kickCounter = Mathf.Clamp (kickCounter, 0, 3.5f);
                animatorParametors.PunchType = punchCounter;
                animatorParametors.KickType = kickCounter;
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
            if (isShoot)
            {
                animatorParametors.Shoot = isShoot;
            }

        }
        public void Shoot ()
        {
            print ("s");
        }
    }
}