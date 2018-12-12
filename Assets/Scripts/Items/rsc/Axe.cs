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
		if (item.type == Item.Tree)
		{
			Manager.SpawnObject(Item.Plank, item.transform);
			Destroy(item.gameObject);
		}
		return false;
	}
	
	override public bool Place(BlockController block)
	{
		return false;
	}
}
