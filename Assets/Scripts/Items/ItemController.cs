using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class attached to every item in the game
public class ItemController : MonoBehaviour {
	// Use if some object will interact with the environment
	public ItemManager Manager;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public GameObject Instantiate(Transform t)
	{
		// TODO check if object is inside a biome
		return Instantiate(gameObject, t);
	}
	
	public GameObject Instantiate(Vector3Int pos)
	{
		// TODO check if object is inside a biome
		return Instantiate(gameObject, pos, Quaternion.identity);
	}
	
	public ItemController Instantiate(ItemController i, Transform t)
	{
		// TODO check if object is inside a biome
		GameObject a = Instantiate(i.gameObject, t);
		ItemController item = a.AddComponent<ItemController>();
		item.Manager = Manager;
		return item;
	}
	
	public ItemController Instantiate(string itemName, Transform t)
	{
		// TODO check if object is inside a biome
		return Instantiate(Manager.Items[itemName], t);
	}
	
	public void PickUp() {
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Collider>().enabled = false;
		transform.position = Manager.Guide.position;
		transform.rotation = Quaternion.identity;
		transform.parent = Manager.Guide.transform;
		Manager.Player.inventory.Add(this);
	}

	public void Release() {
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Collider>().enabled = true;
		Debug.Log(transform.parent.name);
		transform.parent.DetachChildren();
		transform.parent = null;
		Manager.Player.inventory.Clear();
	}

	public virtual bool InteractWith(ItemController obj)
	{
		return false;
	}
	
	//TODO place bridges
}
