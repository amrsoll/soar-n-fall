﻿using UnityEngine;


public class CameraController : MonoBehaviour {

	public const byte LEFT = 1;
	public const byte RIGHT = 2;
	private static float DIST_TO_BIOME = (float)Biome.BlockSize*5;
	//load from JSON
	
	private const float PLAYER_CAM_SIZE = 2.7f;
	

	public GameObject player;       //Public variable to store a reference to the player game object
	public BiomeManager biomeManager;

	private readonly Vector3 BiomeCenterOffset = new Vector3((float) Biome.XSize / 2,
															 (float) Biome.YSize / 2,
															 (float) Biome.ZSize / 2);

	private Vector3 prevCamOffset;         //Private variable to store the offset distance between the player and camera
	private Vector3 curCamOffset;         //Private variable to store the offset distance between the player and camera
	private Vector3 nextCamOffset;         //Private variable to store the offset distance between the player and camera
	private Vector3 camTarget;
	private float camDistance;
	private Vector3Int _currentBiomePos;
	// private float delta;
	//testing
	private bool isDoneRotating = true;
	private byte rotationDirection = 0;
	private bool followThePlayer = false;

	// returns true if the camera is already centered on the position v
	private bool Follow(Vector3 v, float distance = 1f, float strength = 2f) {
		//TODO emulate distance with the size of the viewfield
		transform.position = Vector3.Slerp(transform.position, v+curCamOffset, strength*Time.deltaTime);
		return transform.position == v+distance*curCamOffset;
	}

	private Vector3 RotateBy90Up(Vector3 v, int direction) {
		Quaternion q;
		if(direction >= 0) {
			q = Quaternion.AngleAxis( 90, Vector3.up);
		} else {
			q = Quaternion.AngleAxis(-90, Vector3.up);
		}
		return q * v;
	}

	// direction should either be LEFT or RIGHT (but can take any positive or negative value with the same result)
	// Example :
	// 	if(!isDoneRotating)
	// 		isDoneRotating = rotateCamera(RIGHT, 1.2f);
	public void rotateCamera(byte direction = 1) {
		int dir = 0;
		if(direction == LEFT) dir = 1;
		if(direction == RIGHT) dir = -1;
		curCamOffset = RotateBy90Up(curCamOffset, dir);
	}

	public void SwitchView() {
		followThePlayer = !followThePlayer;
		if(followThePlayer) {
			camTarget = player.transform.position;
			camDistance = PLAYER_CAM_SIZE;
		} else {
			camTarget = _currentBiomePos + BiomeCenterOffset;
			camDistance = DIST_TO_BIOME;
		}
	}

	// Use this for initialization
	void Start () {
		_currentBiomePos = new Vector3Int(0,0,0);
		BiomeController biomeController = biomeManager.GetBiome(_currentBiomePos);
		prevCamOffset = new Vector3(5f,5f,5f);
		curCamOffset = prevCamOffset;
		nextCamOffset = RotateBy90Up(prevCamOffset, 1);
		camTarget = _currentBiomePos + BiomeCenterOffset;
		camDistance = DIST_TO_BIOME;
		transform.position = camTarget + curCamOffset;
		transform.LookAt(camTarget, Vector3.up);
		
		
	}

	void Update() {
    	if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5)){
			rotateCamera(RIGHT);
        }
		if(Input.GetKeyDown(KeyCode.JoystickButton4)) {
			rotateCamera(LEFT);
		}
		if(Input.GetKeyDown(KeyCode.V)) {
			SwitchView();
		}
		if(followThePlayer)
			camTarget = player.transform.position; //needs to be refreshed
		Follow(camTarget, camDistance);
		transform.LookAt(camTarget, Vector3.up);
	}
}
