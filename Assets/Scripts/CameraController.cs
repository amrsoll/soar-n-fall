using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object
	public BiomeManager biomeManager;
	private Vector3 biomeCenterOffset = new Vector3((float)Biome.XSize/2,
													(float)Biome.YSize/2,
													(float)Biome.ZSize/2);         //Private variable to store the offset distance between the player and camera

	private Vector3 prevCamOffset;         //Private variable to store the offset distance between the player and camera
	private Vector3 curCamOffset;         //Private variable to store the offset distance between the player and camera
	private Vector3 nextCamOffset;         //Private variable to store the offset distance between the player and camera
	private Vector3 camTarget;
	private Vector3Int currentBiomePos;
	private float delta;
	//testing
	private bool is_done_rotating = false;


	private Vector3 rotateOffset90(Vector3 v, int direction) {
		Quaternion q;
		if(direction >= 0) {
			q = Quaternion.AngleAxis( 90, Vector3.up);
		} else {
			q = Quaternion.AngleAxis(-90, Vector3.up);
		}
		return q * v;
	}

	// Use this for initialization
	void Start () {
		currentBiomePos = new Vector3Int(0,0,0);
		BiomeController biomeController = biomeManager.GetBiome(currentBiomePos.x,
																currentBiomePos.y,
																currentBiomePos.z);
		prevCamOffset = new Vector3(5f,5f,5f);
		curCamOffset = prevCamOffset;
		nextCamOffset = rotateOffset90(prevCamOffset, 1);
		delta = 0;
		transform.position = currentBiomePos + biomeCenterOffset + curCamOffset;
	}

	// direction should either be 1 or -1 (but can take any positive or negative value with the same result)
	// Example :
	// 	if(!is_done_rotating)
	// 		is_done_rotating = rotateCamera(1, 1.2f);
	public bool rotateCamera(int direction = 1, float speed = 1f) {
		nextCamOffset = rotateOffset90(prevCamOffset, direction);
		camTarget = currentBiomePos + biomeCenterOffset;
		delta = Mathf.Min(1, delta + speed*Time.deltaTime);
		curCamOffset = Vector3.Slerp(prevCamOffset, nextCamOffset,delta);
		transform.position = currentBiomePos + biomeCenterOffset + curCamOffset;
		transform.LookAt(camTarget, Vector3.up);
		if (curCamOffset == nextCamOffset){
			prevCamOffset = nextCamOffset;
			delta = 0;
			return true;
		} else {
			return false;
		}
	}
	// Update is called once per frame
	// Example
	void Update () {
		if(!is_done_rotating)
			is_done_rotating = rotateCamera(1, 1.2f);
		Debug.Log(transform.position);
	}
}
