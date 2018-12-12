using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public List<ItemController> Inventory;
	public List<ItemController> CollidingItems;
	public Transform Guide;
	
	// Use this for initialization
	void Start () {
		Guide = GameObject.Find("Guide").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider c)
	{
		ItemController item = c.gameObject.GetComponent<ItemController>();
		if(item)
			CollidingItems.Add(item);
	}
	
	void OnTriggerExit(Collider c) {
		CollidingItems.Remove(c.gameObject.GetComponent<ItemController>());
	}
	
	public void PickUp(ItemController item)
	{
		if (item.CanPickUp())
		{
			item.GetComponent<Rigidbody>().useGravity = false;
			item.GetComponent<Rigidbody>().isKinematic = true;
			item.GetComponent<Collider>().enabled = false;
			item.transform.position = Guide.position;
			item.transform.rotation = Quaternion.identity;
			item.transform.parent = Guide.transform;
			Inventory.Add(item);
		}
	}

	public void Release(ItemController item) {
		Inventory.Clear();
		CollidingItems.Clear();
		item.GetComponent<Rigidbody>().useGravity = true;
		item.GetComponent<Rigidbody>().isKinematic = false;
		item.GetComponent<Collider>().enabled = true;
		item.transform.parent.DetachChildren();
		item.transform.parent = null;
	}
}
