using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createStuff : MonoBehaviour {

	public GameObject bridge;
	public GameObject plank;
	private bool contact;
	public GameObject collidingItem;

	// Use this for initialization
	void Start () {
		contact = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.C)){
			if (this.GetComponent<moveObject>().pickedUpState && contact) {
				combine(this.GetComponent<moveObject>().item, collidingItem);
			}
		}
	}

	void combine(GameObject item1, GameObject item2) {
		if (item1.tag == "plank") {
			if (item2.tag == "rope") {
				Destroy(item1);
				Destroy(item2);
				this.GetComponent<moveObject>().item = null;
				this.GetComponent<moveObject>().pickedUpState = false;


                Instantiate(bridge, new Vector3(3.0f, 5f, 12f), Quaternion.identity);

            }
		}
		if (item1.tag == "rope") {
			if (item2.tag == "plank") {
				Destroy(item1);
				Destroy(item2);
				this.GetComponent<moveObject>().item = null;
				this.GetComponent<moveObject>().pickedUpState = false;

                Instantiate(bridge, new Vector3(3.0f, 5f, 12f), Quaternion.identity);
                //GameObject bridgex = GameObject.FindWithTag("bridge");
                //bridgex.SetActive(true);
                //Instantiate(bridge, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
            }
		}

		if (item1.tag == "tree") {
			if (item2.tag == "axe") {

				Instantiate(plank, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
			}
		}
		if (item1.tag == "axe") {
			if (item2.tag == "tree") {

				Instantiate(plank, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
			}
		}

		else {
			Debug.Log("Can't combine elements");
			return;
		}
	}

	void OnCollisionEnter(Collision collision) {
		collidingItem = collision.gameObject;
		contact = true;
	}

	void OnCollisionExit(Collision collision) {
		collidingItem = null;
		contact = false;
	}
}
