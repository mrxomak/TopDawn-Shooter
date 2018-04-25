using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform barrel;
    public Projectile projectile;
    public float bulletSpeed = 30f;
    public float weaponDamage = 1f;
    public float fireRate = 0.5f;
    public float lifetime = 10f;
    float timer;

    AudioSource shootSound;

    void FixedUpdate()
    {
        shootSound = GetComponent<AudioSource>();
        timer += Time.deltaTime;
    }

    public void Shoot()
    {
        if (timer >= fireRate)
        {
            timer = 0;
            Projectile newProjectile = Instantiate(projectile, barrel.position, barrel.rotation);
            newProjectile.SetOptions(bulletSpeed,weaponDamage, lifetime);

            shootSound.Play();
        }
    }
}
