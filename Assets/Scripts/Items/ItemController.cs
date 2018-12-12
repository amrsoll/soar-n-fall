using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class attached to every item in the game
public class ItemController : MonoBehaviour {
	// Use if some object will interact with the environment
	public ItemManager Manager;
	public bool Movable = true;
	public Item type;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	
	public GameObject Instantiate(Vector3Int pos)
	{
		// TODO check if object is inside a biome
		return Instantiate(gameObject, pos, Quaternion.identity);
	}

	public bool CanPickUp()
	{
		return Movable;
	}

	public virtual bool InteractWith(ItemController obj)
	{
		return false;
	}
	
	
	//TODO place bridges
}
