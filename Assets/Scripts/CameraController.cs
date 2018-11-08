using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object
	public BiomeManager biomeManager;
	private Vector3 biomeCenterOffset = new Vector3((float)Biome.XSize/2,
													(float)Biome.YSize/2,
													(float)Biome.ZSize/2);         //Private variable to store the offset distance between the player and camera
	private Vector3 camOffset;         //Private variable to store the offset distance between the player and camera
	private Vector3Int currentBiomePos;

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
		camOffset = new Vector3(5f,5f,5f);
		StartCoroutine(waiter());
	}

	IEnumerator waiter()
	{
		Vector3 camTarget = currentBiomePos + biomeCenterOffset;
		transform.position = currentBiomePos + biomeCenterOffset + camOffset;
		transform.LookAt(camTarget, Vector3.up);
		Debug.Log(camOffset);
		//Rotate 90 deg
		camOffset = rotateOffset90(camOffset, 1);
		Debug.Log(camOffset);
		transform.position = currentBiomePos + biomeCenterOffset + camOffset;
		transform.LookAt(camTarget, Vector3.up);
		//Wait for 1 second
		yield return new WaitForSeconds(1);
		//Rotate 90 deg
		camOffset = rotateOffset90(camOffset, 1);
		Debug.Log(camOffset);
		transform.position = currentBiomePos + biomeCenterOffset + camOffset;
		transform.LookAt(camTarget, Vector3.up);
	}
	// Update is called once per frame
	void Update () {
	    //Wait for 1 second
	    // yield return new WaitForSeconds(1);
		// transform.position = currentBiomePos + biomeCenterOffset + camOffset;
		// transform.LookAt(camTarget, Vector3.up);
	}
}
