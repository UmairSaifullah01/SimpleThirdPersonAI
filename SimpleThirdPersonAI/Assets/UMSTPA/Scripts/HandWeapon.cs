using System.Collections.Generic;
using UnityEngine;

public class HandWeapon : WeaponBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private float shootDelay;
    public GameObject ShootParticle;

    public override void DoUpdate ()
    {
        shootDelay -= Time.deltaTime;
    }

    public override void Initialize ()
    {

    }

    public override void Shoot ()
    {
        if (shootDelay <= 0)
        {
            var bullet = (GameObject) Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            var shootParticle = (GameObject) Instantiate (ShootParticle, bulletSpawn.position, bulletSpawn.rotation);
            Destroy (shootParticle, 3);
            shootDelay = 2;
        }
    }

    public override void SetAnimations (ref AnimatorOverrideController animController)
    {
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>> ();
        foreach (AnimationClip a in animController.animationClips)
        {
            switch (a.name)
            {
                case "Grounded":
                    anims.Add (new KeyValuePair<AnimationClip, AnimationClip> (a, ( animations.grounded ) ? animations.grounded : a));
                    break;
                case "Crouching":
                    anims.Add (new KeyValuePair<AnimationClip, AnimationClip> (a, ( animations.crouching ) ? animations.crouching : a));
                    break;
                case "Shoot":
                    anims.Add (new KeyValuePair<AnimationClip, AnimationClip> (a, ( animations.shoot ) ? animations.shoot : a));
                    break;
                case "IdleGun":
                    anims.Add (new KeyValuePair<AnimationClip, AnimationClip> (a, ( animations.idle ) ? animations.idle : a));
                    break;
                case "Reload":
                    anims.Add (new KeyValuePair<AnimationClip, AnimationClip> (a, ( animations.reload ) ? animations.reload : a));
                    break;
                default:
                    anims.Add (new KeyValuePair<AnimationClip, AnimationClip> (a, a));
                    break;

            }

        }
        animController.ApplyOverrides (anims);

    }
}
