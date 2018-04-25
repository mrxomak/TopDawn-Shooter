﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    //Animator anim;
    AudioSource enemyAudio;
    //ParticleSystem hitParticles;
    SphereCollider sphereCollider;
    bool isDead;
    bool isSinking;

    void Awake()
    {
        //anim = GetComponent<Animator> ();
        enemyAudio = GetComponent<AudioSource> ();
        //hitParticles = GetComponentInChildren<ParticleSystem> ();
        sphereCollider = GetComponent<SphereCollider>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking) 
        {
            transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play ();
        currentHealth -= amount;
//        hitParticles.transform.position = hitPoint;
//        hitParticles.Play ();

        if (currentHealth <= 0)
        {
            Death();
            StartSinking();
        }
    }
    void Death()
    {
        isDead = true;
        sphereCollider.isTrigger = true;
        //anim.SetTrigger ("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }
    public void StartSinking()
    {
        GetComponent<NavMeshAgent> ().enabled = false;
        GetComponent<Rigidbody> ().isKinematic = true;
        isSinking = true;
        Destroy (gameObject, 2f);
        //ManagerScript.score += scoreValue;
    }

}
