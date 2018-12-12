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
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.C)){
			if (Player.Inventory.Count > 0 && Player.CollidingItems.Count > 0) {
				ItemController result = Craft(Player.Inventory.First(), Player.CollidingItems.First());
				Player.Inventory.Add(result);
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
			|| (Player.Inventory.Count > 0 && Player.CollidingItems.Count > 0))
		{
			Player.CollidingItems.First().InteractWith(Player.CollidingItems.Last());
		}
	}
	
	
	private ItemController Craft(params ItemController[] items) {
		List<Item> ingredients = new List<Item>();
		foreach (ItemController item in items)
			ingredients.Add(item.type);
		ingredients.Sort();
		if (Recipees.Keys.Contains(ingredients))
		{
			foreach (ItemController item in items)
				Destroy(item.gameObject);
			return Instantiate(Items[Recipees[ingredients]], Player.Guide.transform);
		}
		return null;
	}
	
	public void Interact(ItemController tool, ItemController item) {
		tool.InteractWith(item);
	}

	private void AddRecipee(Item result, params Item[] ingredients)
	{
		List<Item> ing = new List<Item>();
		foreach (var i in ingredients)
			ing.Add(i);
		ing.Sort();
		Recipees.Add(ing, result);
	}
	
	private void FillRecipeeBook()
	{
		Recipees = new Dictionary<List<Item>, Item>();
		AddRecipee(Item.PlankBridge, Item.Rope, Item.Plank);
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
		return Instantiate(item, Vector3.zero, Quaternion.identity, parent);
	}

	public static ItemController Instantiate(ItemController item)
	{
		GameObject a = Instantiate(item.gameObject);
		return a.GetComponent<ItemController>();
	}
	
	public ItemController SpawnObject(Item itemType, Transform parent)
	{
		ItemController item = Instantiate(Items[itemType]);
		item.Manager = this;
		return item;
	}
	
}
