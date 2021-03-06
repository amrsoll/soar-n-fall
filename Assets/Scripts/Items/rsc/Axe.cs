﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : ItemController
{
    EventSoundTrigger SoundEvent;
	
	void Start()
	{
        SoundEvent = GameObject.Find("SoundManager").GetComponent<EventSoundTrigger>();
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	public override bool InteractWith(ItemController item)
	{
		if (item.type == Item.Tree)
		{
            ItemController plank = Manager.SpawnObject(Item.Plank, item.transform.position + new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity);

            Transform tree = item.transform.Find("Tree Model");
            Transform treeStump = item.transform.Find("TreeCut");
            
            tree.gameObject.SetActive(false);
            treeStump.gameObject.SetActive(true);

            SoundEvent.PlayClip("AxeSwing");


            return true;
		}
		return false;
	}
}
