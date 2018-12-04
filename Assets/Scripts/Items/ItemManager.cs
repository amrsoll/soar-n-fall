using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

public class ItemManager : MonoBehaviour {
	public PlayerController Player;
	public Transform Guide;

	// The recipe ingredients should be listed in alphabetical order #performance
	public Dictionary<List<string>, string> Recipees;
	public Dictionary<string, ItemController> Items;
	
	

	// Use this for initialization
	void Start ()
	{
		Guide = GameObject.Find("Guide").transform;
		FillRecipeeBook();
		LoadItems();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.C)){
			if (Player.inventory.Count > 0 && Player.collidingItems.Count > 0) {
				ItemController result = Craft(Player.inventory.First(), Player.collidingItems.First());
				Player.inventory.Add(result);
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.E)){
			if (Player.inventory.Count == 0 && Player.collidingItems.Count > 0) {
				Debug.Log(Player.collidingItems[0]);
				Player.collidingItems.First().PickUp();
				Debug.Log("Picking up");
			}
			else if(Player.inventory.Count > 0) {
				Player.inventory.First().Release();
				Debug.Log("Releasing");
			}
		} 
	}
	
	public ItemController Craft(params ItemController[] items) {
		List<string> ingredients = new List<string>();
		foreach (ItemController item in items)
			ingredients.Add(item.name);
		ingredients.Sort();
		if (Recipees.Keys.Contains(ingredients))
		{
			foreach (ItemController item in items)
				Destroy(item.gameObject);
			return Instantiate(Items[Recipees[ingredients]], Guide.transform);
		}
		return null;
	}
	
	public void Interact(ItemController tool, ItemController item) {
		tool.InteractWith(item);
	}

	private void AddRecipee(string result, params string[] ingredients)
	{
		List<string> ing = new List<string>();
		foreach (var i in ingredients)
			ing.Add(i);
		ing.Sort();
		Recipees.Add(ing, result);
	}
	
	private void FillRecipeeBook()
	{
		Recipees = new Dictionary<List<string>, string>();
		AddRecipee("Plank bridge", "Rope", "Plank");
	}
	
	private void LoadItems()
	{
		Items = new Dictionary<string, ItemController>();

		ItemController[] items = Resources.LoadAll<ItemController>("Items/");
		foreach (ItemController i in items)
		{
			Items.Add(i.name, i);
		}
	}
}
