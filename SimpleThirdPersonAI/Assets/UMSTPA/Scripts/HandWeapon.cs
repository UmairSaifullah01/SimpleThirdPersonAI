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
}
