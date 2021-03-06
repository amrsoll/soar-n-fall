﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour {

	private float moveSpeed = 2.0f;
    private float jumpSpeed = 250.0f;
    public Rigidbody rigidBody;
    public Camera camera;
    private float interpolation = 10.0f;
    public float jumpLimit = 15f;

    Vector3 currentD = new Vector3(1.0f, 0.0f, 0.0f);
    float currentV = 0.0f;
    float currentH = 0.0f;

    public bool canMove;

    public Vector3 currentDirection = Vector3.zero;

    // Use this for initialization
    void Start () {
        canMove = true;
    }

    // Update is called once per frame
    void Update () {

        if(canMove)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
            currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation); 

            Vector3 rightLeft = Vector3.Cross(camera.transform.right, Vector3.up) * currentV;
            Vector3 forwardBack =  camera.transform.right * currentH;
            Vector3 direction = rightLeft + forwardBack;

            if (direction != Vector3.zero) {
                currentD = direction;
            }

            currentDirection = Vector3.Slerp(currentDirection, direction, Time.deltaTime * interpolation);
            if (currentDirection == Vector3.zero) {
                transform.rotation = Quaternion.LookRotation(currentD, Vector3.up);
            }
            else {
                transform.rotation = Quaternion.LookRotation(currentDirection, Vector3.up);
            }
            transform.position += currentDirection * moveSpeed * Time.deltaTime;


            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space)){
                //HARDCODED LIMIT FOR HOW HIGH YOU CAN JUMP
                if (transform.position.y < jumpLimit) {
                    rigidBody.AddForce(Vector3.up * jumpSpeed);
                }
            }
            /*if ( Input.GetKeyDown(KeyCode.H) )
            {
                Vector3 home = new Vector3(4.5f, 5.4f, 4.4f);
                transform.position = home;
            }*/   
        }
    }
}