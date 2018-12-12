using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : ItemController
{
	private static bool Movable = true;
	
	
	void Start()
	{
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	
	override public bool InteractWith(ItemController item)
	{
		Debug.Log(item.name.ToString());
		if (item.type == Item.Tree)
		{
			ItemController plank = Manager.SpawnObject(Item.Plank, item.transform.position, Quaternion.identity);
			Destroy(item.gameObject);
			return true;
		}
		return false;
	}
	
	override public bool Place(BlockController block)
	{
		return false;
	}
}
