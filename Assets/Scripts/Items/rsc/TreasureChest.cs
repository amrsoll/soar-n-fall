using UnityEngine;

public class TreasureChest : ItemController
{
    private void Start()
    {
        type = Item.TreasureChest;
    }

    public override bool InteractWith()
    {
        Transform closedChest = gameObject.transform.Find("TreasureChestClosed");
        Transform openChest = gameObject.transform.Find("TreasureChestOpen");
        if (closedChest.gameObject.activeInHierarchy)
        {
            closedChest.gameObject.SetActive(false);
            openChest.gameObject.SetActive(true);
        } else
        {
            openChest.gameObject.SetActive(false);
            closedChest.gameObject.SetActive(true);
        }
            
        return false;
    }
}