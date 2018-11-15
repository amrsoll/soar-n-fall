using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerMovement : MonoBehaviour {

	private float moveSpeed = 2.0f;
    private float jumpSpeed = 5.0f;
    public Rigidbody rigidBody;
    public Camera camera;
    private float interpolation = 10.0f;

    Vector3 currentD = new Vector3(1.0f, 0.0f, 0.0f);
    float currentV = 0.0f;
    float currentH = 0.0f;

    private Vector3 currentDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
            rigidBody.velocity += jumpSpeed * Vector3.up;
        }
	}
}
