using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public Image[] itemImagesInInventory = new Image[numItemSlots];
    public Item[] itemsInInventory = new Item[numItemSlots];
    public const int numItemSlots = 4;
    public Dictionary<Item, Sprite> ItemImages;

    void Start()
    {
        //ItemImages.Add(Item.Axe, Axe);
        /*ItemImages.Add(Item.PickAxe, PickAxe);
        ItemImages.Add("Plank", Plank);
        ItemImages.Add("Stone", Stone);
        ItemImages.Add("Rope", Rope);*/
        LoadItems();
        Add(Item.PickAxe);
    }

    public void Add(Item itemToAdd)
    {
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i] == Item.Undefined)
            {
                
                itemsInInventory[i] = itemToAdd;
                //Debug.Log("the sprite1: " + itemImagesInInventory[i].typeOf());
                itemImagesInInventory[i].sprite = ItemImages[itemToAdd];
                itemImagesInInventory[i].enabled = true;
                Debug.Log("the sprite2: " + itemImagesInInventory[i]);
                return;
            }
        }
    }
    
    public void Remove(Item itemToRemove)
    {
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i] == itemToRemove)
            {
                itemsInInventory[i] = Item.Undefined;
                itemImagesInInventory[i].sprite = null;
                itemImagesInInventory[i].enabled = false;
                return;
            }
        }
    }

    private void LoadItems()
    {
        ItemImages = new Dictionary<Item, Sprite>();
        foreach (Item i in Enum.GetValues(typeof(Item)))
        {
            Sprite sprite = Resources.Load<Sprite>("ItemSprites/"+i);
            ItemImages.Add(i, sprite);
        }
    }
}

