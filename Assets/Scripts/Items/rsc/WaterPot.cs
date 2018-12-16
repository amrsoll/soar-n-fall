using UnityEngine;
// TODO it's actually watering pot
public class WaterPot : ItemController
{
    private bool hasWater;

    public void Start()
    {
        hasWater = false;
        hasWater = true;
    }
    
    public override bool InteractWith(ItemController item)
    {
        Debug.Log(item.name.ToString());
        if (item.type == Item.Flower && hasWater)
        {
            item.GetComponent<Animator>().SetTrigger("Active");
            item.GetComponent<Collider>().enabled = true;
            // water the flower
            // delete the water in the waterpot
            hasWater = !hasWater;
            return true;
        }
        return false;
    }
    
    public override bool InteractWith(BlockController block)
    {
        Debug.Log(block.name.ToString());
        if (block.type == BlockType.Water)
        {
            // fill the watering pot

            hasWater = !hasWater;
            return true;
        }
        return false;
    }
    
    
}