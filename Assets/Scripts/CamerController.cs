﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object


	private Vector3 offset;         //Private variable to store the offset distance between the player and camera

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + offset;
	}
}
