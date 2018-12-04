using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	public Vector3 startPosition;
	public Quaternion startRotation;

	// Use this for initialization
	void Start () {

		startPosition = this.transform.position;
		startRotation = this.transform.rotation;
		
	}
	
	// Update is called once per frame
	void Update () {
		//change to: look if you've changed biome
		if (this.transform.position.y < -50.0) {
			respawnPlayer();
			return;
		}
		
	}

	public void respawnPlayer() {
		this.transform.rotation = startRotation;
		this.transform.position = startPosition;
	}
}