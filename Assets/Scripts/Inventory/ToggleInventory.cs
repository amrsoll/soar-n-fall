using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventory : MonoBehaviour {

	bool isActive;
	public GameObject inventoryArrow;
	public GameObject inventoryMenu;
	public GameObject player;
	// Use this for initialization
	void Start () {
		isActive = false;
		inventoryMenu.SetActive(isActive);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.JoystickButton6))
		{
			Debug.Log("hej");
			isActive = !isActive;
			inventoryMenu.SetActive(isActive);
			inventoryArrow.SetActive(!isActive);
		}
		if(isActive) {
			player.GetComponent<playerMovement>().canMove = false;
		}
		if(!isActive) {
			player.GetComponent<playerMovement>().canMove = true;
		}
	}
}
