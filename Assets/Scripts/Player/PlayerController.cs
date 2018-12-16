using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public List<ItemController> Inventory;
	public List<ItemController> CollidingItems;
	public BlockController BlockThePlayerWalksOn;
	public Transform Guide;
	private BiomeManager _biomeManager;
	
	// Use this for initialization
	void Start () {
		Guide = GameObject.Find("Guide").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision c)
	{
		BlockController block = c.gameObject.GetComponent<BlockController>();
		if (block && transform.localScale.y - BiomeManager.BlockToWorldPos(CurrentBiomePos(), block.biomeCoords).y < Biome.BlockSize*.9)
		{
			// TODO check if the Player is inside the x and z boundaries of the BlockThePlayerWalksOn.
			BlockThePlayerWalksOn = block;
		}
	}

	void OnTriggerEnter(Collider c)
	{
		ItemController item = c.gameObject.GetComponent<ItemController>();
		if (item)
		{
			CollidingItems.Last().Selected = false;
			// TODO stop applying transparency shader to the last item
			item.Selected = true;
			// TODO start applying transparency shader to this item
			CollidingItems.Add(item);
		}
	}
	
	void OnTriggerExit(Collider c)
	{
		ItemController item = c.gameObject.GetComponent<ItemController>();
		CollidingItems.Remove(item);
		item.Selected = false;
		// TODO stop applying transparency shader to this item
		CollidingItems.Last().Selected = true;
		// TODO start applying transparency shader to the last item
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

	public Vector3Int CurrentBiomePos()
	{
		return new Vector3Int(
			Mathf.FloorToInt(transform.position.x / (Biome.XSize * Biome.BlockSize + Biome.BiomeSpacing)),
			Mathf.FloorToInt(transform.position.y / (Biome.YSize * Biome.BlockSize + Biome.BiomeSpacing)),
			Mathf.FloorToInt(transform.position.z / (Biome.ZSize * Biome.BlockSize + Biome.BiomeSpacing)));
	}
}
