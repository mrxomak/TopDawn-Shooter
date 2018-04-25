using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public int damage = 20;
	public float timeBetweenBullets = 0.15f;
	public float range = 100f;
    public Transform gunEnd;
    public float spreadRange = 1;

	float timer;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
    LineRenderer gunLine;
	AudioSource gunAudio;
	//Light gunLight;
	float effectsDisplayTime = 0.2f;

    public GameObject shootFlash;
    public Vector3 shootPoint;

    Random randomHitPoint;

	void Awake()
	{
        shootableMask = LayerMask.GetMask ("Targeted") | LayerMask.GetMask("Shootable");
		gunLine = GetComponent<LineRenderer> ();
		//gunLight = GetComponent<Light> ();
		gunAudio = GetComponent<AudioSource> ();

	}

	void Update()
	{
		timer += Time.deltaTime;

		if (Input.GetButton("Fire1") && timeBetweenBullets <= timer) 
		{
			ShootRay ();
		}
		if (timer >= timeBetweenBullets * effectsDisplayTime) 
		{
			DisableEffects ();
		}
	}

	void DisableEffects()
	{
		gunLine.enabled = false;
        shootFlash.SetActive(false);
	}

	void ShootRay() // need to refactor bullet spray
	{
		timer = 0f;
        shootFlash.SetActive(true);
		gunAudio.Play ();

		gunLine.enabled = true;
        gunLine.SetPosition (0, gunEnd.position);
        Vector3 randomShootDirection = Random.insideUnitSphere.normalized * spreadRange; 

        shootRay.origin = transform.position;
        shootRay.direction = shootPoint - transform.position + randomShootDirection;

		if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) 
		{
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth> ();
			if (enemyHealth != null) 
			{
				enemyHealth.TakeDamage (damage, shootHit.point);
			}
            gunLine.SetPosition(1, shootHit.point);

		} 
		else 
				{
            gunLine.SetPosition(1, shootRay.direction * range);
				}
	}
}
