using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public Image[] itemImagesInInventory = new Image[numItemSlots];
    public Item[] itemsInInventory = new Item[numItemSlots];
    public const int numItemSlots = 4;

    public void Add(ItemController itemToAdd)
    {
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i] == null)
            {
                itemsInInventory[i] = itemToAdd;
                itemImagesInInventory[i].sprite = itemToAdd.sprite;
                itemImagesInInventory[i].enabled = true;
                return;
            }
        }
    }
    
    public void Remove(ItemController itemToRemove)
    {
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i] == itemToRemove)
            {
                itemsInInventory[i] = null;
                itemImagesInInventory[i].sprite = null;
                itemImagesInInventory[i].enabled = false;
                return;
            }
        }
    }
}