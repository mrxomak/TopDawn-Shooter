using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (WeaponController))]

public class MyCapsuleController : MonoBehaviour {
    public float speed = 5f;
    int floorMask;
    CharacterController playerContr;
    Vector3 moveDirection;
    RaycastHit hit;
    public Vector3 lookPos;
    WeaponController weaponController;

    void Start () 
    {
        floorMask = LayerMask.GetMask("Enemy") | LayerMask.GetMask("Obsticle");
        playerContr = GetComponent<CharacterController>();
        weaponController = GetComponent<WeaponController>();
    }


    void FixedUpdate () 
    {
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        LookPosition();
        MovePlayer(zAxis,xAxis);

        if (Input.GetButton("Fire1"))
        {
            weaponController.Shoot();
        }
    }

    void LookPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 100f, floorMask))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;
        transform.LookAt(transform.position + lookDir,Vector3.up);
    }

    void MovePlayer(float zAxis, float xAxis)
    {       
        moveDirection = new Vector3(xAxis, 0f, zAxis);
        playerContr.SimpleMove(moveDirection.normalized * speed);
    }
}