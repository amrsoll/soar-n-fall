using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour {

	public GameObject tempParent;
	public Transform guide;
	public bool pickedUpState;
	public GameObject item;

	// Use item for initialization
	void Start () {
		//Make sure the item has a rigidbody
		//item.GetComponent<Rigidbody>().useGravity = true;
		item = null;
		pickedUpState = false;
		tempParent = GameObject.FindGameObjectWithTag("guide"); //.GetComponent<GameObject>();
		guide = tempParent.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.E)){
            if (!pickedUpState && item.GetComponent<BlockController>()==null) {
				pickUp();
				Debug.Log("Picking up");
			}
			else {
				if (item.GetComponent<BlockController>() == null) {
					release();
					Debug.Log("Releasing");
				}
				
			}
		}   
	}

	void OnCollisionEnter(Collision collision) {
		if (!pickedUpState) {
			item = collision.gameObject;
		}

	}

	void OnCollisionExit(Collision collision) {
		if (!pickedUpState) {
			item = null;
		}
	}

	void pickUp() {
		item.GetComponent<Rigidbody>().useGravity = false;
		item.GetComponent<Rigidbody>().isKinematic = true;
		item.GetComponent<Collider>().enabled = false;
		item.transform.position = guide.transform.position;
		item.transform.rotation = guide.transform.rotation;
		item.transform.parent = tempParent.transform;
		pickedUpState = true;
	}

	void release() {
		item.GetComponent<Rigidbody>().useGravity = true;
		item.GetComponent<Rigidbody>().isKinematic = false;
		item.GetComponent<Collider>().enabled = true;
		item.transform.parent = null;
		item.transform.position = guide.transform.position;
		item = null;
		pickedUpState = false;
		
	}
}
