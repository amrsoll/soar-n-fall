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
            ItemController plank = Manager.SpawnObject(Item.Plank, item.transform.position + new Vector3(0.1f, 0.5f, 0f), Quaternion.identity);

            Transform tree = item.transform.Find("Tree (1)");
            Transform treeStump = item.transform.Find("tree-cut");
            
            tree.gameObject.SetActive(false);
            treeStump.gameObject.SetActive(true);

			return true;
		}
		return false;
	}
	
	override public bool Place(BlockController block)
	{
		return false;
	}
}
