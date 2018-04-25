using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

    NavMeshAgent pathFinder;
    Transform target;
    EnemyCapsuleHealth enemyCapHealth;

    public float speed = 4f;

    void Start()
    {
        enemyCapHealth = GetComponent<EnemyCapsuleHealth>();
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        while (target != null && enemyCapHealth.isDead == false)
        {
            pathFinder.SetDestination(target.position);
            pathFinder.speed = speed;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
