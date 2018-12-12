using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankBridge : ItemController
{
	private static bool Movable = false;
	void Start()
	{
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	override public bool InteractWith(ItemController item)
	{
		return false;
	}
	
	override public bool Place(BlockController block)
	{
		if ((block.biomeCoords.x == Biome.XSize - 1 ||
		     block.biomeCoords.x == 0 ||
		     block.biomeCoords.z == Biome.ZSize - 1 ||
		     block.biomeCoords.z == 0))
		{
			Transform player = Manager.Player.transform;
			Vector3 playerOrientation = Manager.Player.GetComponent<playerMovement>().currentDirection;
			Vector3 biomeCenter = BiomeManager.BiomeToWorldPos(BiomeManager.WorldToBiomePos(player.position)) 
			                      + CameraController.BiomeCenterOffset;
			Vector3 relativePosInBiome = biomeCenter - player.position;
			if (relativePosInBiome.x * playerOrientation.x >= 0 ||
			    relativePosInBiome.z * playerOrientation.z >= 0)
			{
				//build bridge in playerOrientation
				return false;
			}
		}
		return false;
	}
}
