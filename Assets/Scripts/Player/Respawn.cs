using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	public Vector3 startPosition;
	public Quaternion startRotation;
    private GameObject player;

    // Use this for initialization
    void Start () {

        player = GameObject.FindWithTag("Player");

		startPosition = player.transform.position;
		startRotation = player.transform.rotation;
		
	}
	
	// Update is called once per frame
	void Update () {
		//change to: look if you've changed biome
		if (player.transform.position.y < -50.0) {
			respawnPlayer();
			return;
		}
		
	}

	public void respawnPlayer() {
        player.transform.rotation = startRotation;
        player.transform.position = startPosition;

        if (ToggleInventory.GameIsPaused) {
            GetComponent<ToggleInventory>().Resume();
        }
    }
}