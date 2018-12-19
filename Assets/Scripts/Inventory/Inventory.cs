using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public const int numItemSlots = 4;
    public Image[] itemImagesInInventory = new Image[numItemSlots];
    public Item[] itemsInInventory = new Item[numItemSlots];
    public Dictionary<Item, Sprite> ItemImages;
    public Sprite m_EmptySlotSprite;
    public Image m_EmptySlotImg;
    public GameObject ItemManager;

    PlayerController player;

    void Start()
    {
        LoadItems();
        LoadItemList();
        player = GetComponent<PlayerController>();
    }

    public void Add(Item itemToAdd)
    {
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i] == Item.Undefined)
            {
                itemsInInventory[i] = itemToAdd;
                itemImagesInInventory[i].sprite = ItemImages[itemToAdd];
                itemImagesInInventory[i].enabled = true;
                return;
            }
        }
    }

    void Update()
    {
    }
    
    /*public void Remove(Item itemToRemove)
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
    }*/
    
    public void Remove(int index)
    {
        if (itemsInInventory[index] == Item.Undefined) return;
        if (ItemManager.GetComponent<ItemManager>().Place(itemsInInventory[index], player.BlockThePlayerWalksOn) == false) return;
        itemsInInventory[index] = Item.Undefined;
        itemImagesInInventory[index].sprite = null;
        itemImagesInInventory[index].enabled = false;
        
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

    private void LoadItemList()
    {
        for(int i = 0; i < itemsInInventory.Length; i++)
        {
            itemsInInventory[i] = Item.Undefined;
        }
    }
}

