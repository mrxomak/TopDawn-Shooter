using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;

    int floorMask;

    //Rigidbody playerRigidbody;
    CharacterController playerContr;
    Vector3 moveDirection;
    //Transform cam;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;
    RaycastHit hit;
    Vector3 lookPos;

    PlayerShooting playerShooting;

    float forwardAmount;
    float turnAmount;

    void Start () 
    {
        floorMask = LayerMask.GetMask("Enemy") | LayerMask.GetMask("Obsticle");

        //playerRigidbody = GetComponent<Rigidbody>();
        playerContr = GetComponent<CharacterController>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        //cam = Camera.main.transform;
    }

    void FixedUpdate () 
    {
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        LookPosition();
        MovePlayer(zAxis,xAxis);
    }

    void LookPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f, floorMask))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        playerShooting.shootPoint = lookPos;
        lookDir.y = 0;
        transform.LookAt(transform.position + lookDir,Vector3.up);
        //Quaternion newRotation = Quaternion.LookRotation(playerToMouse); //another way to do so
        //playerRigidbody.MoveRotation(newRotation);
    }

    void MovePlayer(float zAxis, float xAxis)
    {       
        moveDirection = new Vector3(xAxis, 0f, zAxis);

        //playerRigidbody.MovePosition(transform.position + moveDirection.normalized * speed * Time.deltaTime);
        playerContr.SimpleMove(moveDirection.normalized * speed);
    }

//    void MoveAnim(float zAxis, float xAxis)
//    {
//        if (cam != null)
//        {
//            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1));
//            move = zAxis * camForward + xAxis * cam.right;
//        }
//        else
//        {
//            move = zAxis * Vector3.forward + xAxis * Vector3.right;
//        }
//
//        if (move.magnitude > 1)
//            move.Normalize();
//
//        this.moveInput = move;
//
//        Vector3 localMove = transform.InverseTransformDirection(moveInput);
//        turnAmount = localMove.x;
//        forwardAmount = localMove.z;
//
//        anim.SetFloat("MoveZ",forwardAmount);
//        anim.SetFloat("MoveX",turnAmount);
//
//    }

}
