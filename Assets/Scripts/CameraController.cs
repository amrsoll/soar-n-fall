using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object
	public BiomeManager biomeManager;
	private Vector3 biomeCenterOffset = new Vector3((float)Biome.XSize/2,
													(float)Biome.YSize/2,
													(float)Biome.ZSize/2);         //Private variable to store the offset distance between the player and camera
	private Vector3 camOffset = new Vector3(5f,5f,5f);         //Private variable to store the offset distance between the player and camera
	private Vector3Int currentBiomePos;

	private Vector3 rotateOffset90(Vector3 offset, int direction) {
		if(direction >= 0) {
			return Quaternion.AngleAxis( 90, Vector3.up) * offset;
		} else {
			return Quaternion.AngleAxis(-90, Vector3.up) * offset;
		}
	}

	// Use this for initialization
	void Start () {
		currentBiomePos = new Vector3Int(0,0,0);
		BiomeController biomeController = biomeManager.GetBiome(currentBiomePos.x,
																currentBiomePos.y,
																currentBiomePos.z);
		Debug.Log(biomeCenterOffset);
	}

	// Update is called once per frame
	void Update () {

		Vector3 camTarget = currentBiomePos + biomeCenterOffset;
		transform.position = currentBiomePos + biomeCenterOffset + camOffset;
		transform.LookAt(camTarget, Vector3.up);
	}
}
