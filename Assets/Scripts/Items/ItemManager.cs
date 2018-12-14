using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

public class ItemManager : MonoBehaviour {
	public PlayerController Player;

	// The recipe ingredients should be listed in alphabetical order #performance
	public Dictionary<List<Item>, Item> Recipees;
	public Dictionary<Item, ItemController> Items;
	
	// Use this for initialization
	void Start ()
	{
		FillRecipeeBook();
		LoadItems();
		SpawnInItems();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.C)){
			if (Player.Inventory.Count > 0 && Player.CollidingItems.Count > 0) {
				Item result = Craft(Player.Inventory.First(), Player.CollidingItems.Last());
				if(result != Item.Undefined)
					Player.Inventory.Add(SpawnObject(result, Player.Guide.transform.position, Quaternion.identity));
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.E)){
			if (Player.Inventory.Count == 0 && Player.CollidingItems.Count > 0) {
				Player.PickUp(Player.CollidingItems.First());
			}
			else if(Player.Inventory.Count > 0) {
				Player.Release(Player.Inventory.First());
			}
		}

		if ((/*Input.GetKeyDown(KeyCode.JoystickButton4) || */Input.GetKeyDown(KeyCode.Q))
		    && Player.CollidingItems.Count > 0)
		{
			bool res = Player.CollidingItems.Last().InteractWith();
			Debug.Log("Trying to interact with " + Player.CollidingItems.Last().type);
			if (! res &&
			    Player.Inventory.Count > 0)
				Player.Inventory.First().InteractWith(Player.CollidingItems.Last());
		}
	}
	
	
	private Item Craft(params ItemController[] items) {
		List<Item> ingredients = new List<Item>();
		foreach (ItemController item in items)
			ingredients.Add(item.type);
		// sort the present ingredients before comparing to the recipees
		ingredients.Sort(delegate(Item x, Item y) { return (""+x).CompareTo(""+y); });
		// compare each ingredients individualy
		Item recipeeRes = Item.Undefined;
		foreach (KeyValuePair<List<Item>, Item> recipee in Recipees)
		{
			bool found = true;
			if (recipee.Key.Count != ingredients.Count)
				continue;
			int k = 0;
			foreach (var i in ingredients)
			{
				if (i != recipee.Key[k])
				{
					found = false;
					break;
				}
				k++;
			}
			if (found)
			{
				recipeeRes = recipee.Value;
				break;
			}
		}
		if (recipeeRes != Item.Undefined)
		{
//			ItemController res = SpawnObject(recipeeRes, Player.Guide.transform.position, Quaternion.identity);
			foreach (ItemController item in items)
			{
				Player.Inventory.Remove(item);
				Destroy(item.gameObject);
			}
			return recipeeRes;
		}
		return recipeeRes;
	}
	
	public void Interact(ItemController tool, ItemController item) {
		tool.InteractWith(item);
	}

	private void AddRecipee(Item result, params Item[] ingredients)
	{
		List<Item> ing = new List<Item>();
		foreach (var i in ingredients)
			ing.Add(i);
		ing.Sort(delegate(Item x, Item y)
		{
			return (""+x).CompareTo(""+y);
		});
		Recipees.Add(ing, result);
	}
	
	private void FillRecipeeBook()
	{
		Recipees = new Dictionary<List<Item>, Item>();
		AddRecipee(Item.PlankBridge, Item.Rope, Item.Plank);
        AddRecipee(Item.Ladder, Item.Nails, Item.Plank);
	}
	
	private void LoadItems()
	{
		Items = new Dictionary<Item, ItemController>();
		foreach (Item i in Enum.GetValues(typeof(Item)))
		{
			ItemController item = Resources.Load<ItemController>("Items/"+i);
			Items.Add(i, item);
		}
	}
	
	public static ItemController Instantiate(ItemController item, Vector3 position, Quaternion rotation, Transform parent) 
	{
		// TODO check if object is inside a biome
		GameObject a = Instantiate(item.gameObject, position, rotation, parent);
		return a.GetComponent<ItemController>();
	}
	
	public static ItemController Instantiate(ItemController item, Transform parent)
	{
		// TODO check if object is inside a biome
		GameObject a = Instantiate(item.gameObject, parent);
		return a.GetComponent<ItemController>();
	}

	public static ItemController Instantiate(ItemController item)
	{
		GameObject a = Instantiate(item.gameObject);
		return a.GetComponent<ItemController>();
	}
	
	public ItemController SpawnObject(Item itemType, Vector3 pos, Quaternion q)
	{
		ItemController item = Instantiate(Items[itemType], pos, q, this.transform);
		item.Manager = this;
		return item;
	}
	
	// place an object on top of a block
	public bool Place(ItemController item, BlockController block)
	{
		bool youMayPlaceThis = false;
		Vector3 pos = Vector3.zero;
		Quaternion q = Quaternion.identity;
		// case of the bridge
		if (item.type == Item.PlankBridge)
		{
			int bx = block.biomeCoords.x;
			int by = block.biomeCoords.y;
			int bz = block.biomeCoords.z;
			if (bx == Biome.XSize - 1 ||
			    bx == 0)
			{
				q = Quaternion.identity * Quaternion.AngleAxis(90,Vector3.up);
				if (bx == 0) pos.x -= (float)Biome.BiomeSpacing / 2; 
				else         pos.x += (float)Biome.BiomeSpacing / 2; 
				youMayPlaceThis = true;
			}
			else if (bz == Biome.ZSize - 1 ||
			         bz == 0)
			{
				q = Quaternion.identity;
				if (bz == 0) pos.z -= (float)Biome.BiomeSpacing / 2; 
				else         pos.z += (float)Biome.BiomeSpacing / 2; 
				youMayPlaceThis = true;
			}
			if(youMayPlaceThis)
			{
				Vector3Int blockPos = new Vector3Int(bx, by, bz);
				Vector3Int biomePos = BiomeManager.WorldToBiomePos(Player.transform.position);
				pos += BiomeManager.BiomeToWorldPos(biomePos) + blockPos * Biome.BlockSize;
			}
			return true;
		}
		
		if(youMayPlaceThis)
			SpawnObject(item.type, pos, q);
		return false;
	}

	private bool SpawnInItems()
	{
		SpawnObject(Item.Axe, new Vector3(7.8f, 5.4f, 6.1f), Quaternion.identity);
        SpawnObject(Item.Tree, new Vector3(6f, 5f, 8f), Quaternion.AngleAxis(-90f, new Vector3(1, 0, 0)));
		return false;
	}
	
}
