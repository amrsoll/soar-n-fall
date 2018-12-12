using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankBridge : ItemController
{
	void Start()
	{
		Movable = false;
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	override public bool InteractWith(ItemController item)
	{
		return false;
	}
}
