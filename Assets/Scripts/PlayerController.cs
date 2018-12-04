using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public List<ItemController> inventory;
	public List<ItemController> collidingItems;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider c) {
		if(c.gameObject.GetComponent<ItemController>())
			collidingItems.Add(c.gameObject.GetComponent<ItemController>());
	}
	
	void OnTriggerExit(Collider c) {
		collidingItems.Remove(c.gameObject.GetComponent<ItemController>());
	}
}
