using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	private float moveSpeed = 2;
    private float jumpSpeed = 5;
    public Rigidbody rigidBody;
    private float interpolation = 10;

    float currentV = 0;
    float currentH = 0;

    private Vector3 currentDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;


        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

        Vector3 direction = new Vector3(0,0,1) * currentV + new Vector3(1,0,0) * currentH;

        currentDirection = Vector3.Slerp(currentDirection, direction, Time.deltaTime * interpolation);
        transform.rotation = Quaternion.LookRotation(currentDirection);
        transform.position += currentDirection * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space)){
            rigidBody.velocity += jumpSpeed * Vector3.up;
        }
	}
}
