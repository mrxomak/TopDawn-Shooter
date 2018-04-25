using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCapsuleHealth : MonoBehaviour 
{
    public float health = 10f;
    public GameObject deathEffect;
    AudioSource deathSound;
    CapsuleCollider capCollider;
    MeshRenderer meshRender;
    NavMeshAgent nav;
    public bool isDead = false;

    public static event System.Action OnDeath;

    void Start()
    {
        capCollider = GetComponent<CapsuleCollider>();
        meshRender = GetComponent<MeshRenderer>();
        deathSound = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if(health <= 0f && isDead == false)
        {
            isDead = true;
            deathSound.Play();
            Instantiate(deathEffect,transform.position,transform.rotation);
            nav.enabled = false;
            capCollider.enabled = false;
            meshRender.enabled = false;

            if (OnDeath != null)
            {
                OnDeath();
            }

            Invoke("_DestroyGameObject",2f);
        }
    }


    void _DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
