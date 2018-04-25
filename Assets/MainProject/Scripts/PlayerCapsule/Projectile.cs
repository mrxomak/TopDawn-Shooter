using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    float speed = 30f;
    float lifetime = 10f;
    public float damage = 1f;

    public GameObject bulletHitEnemy;
    public GameObject bulletMissed;

    public void SetOptions(float _speed,float _damage, float _lifetime)
    {
        speed = _speed;
        lifetime = _lifetime;
        damage = _damage;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        transform.Rotate(0,180,0); //inverse spawn of particles opposite bullet direction
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyCapsuleHealth>().health -= damage;
            Instantiate(bulletHitEnemy,transform.position,transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(bulletMissed, transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
        
}
