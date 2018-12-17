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
        if (item.type == Item.Flower)//&& hasWater)
        {
            GameObject.Find("SoundManager").GetComponent<NarrationTrigger>().PlayClip("on-flowers-watered");
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
        if (block.type == BlockType.Water)
        {
            // fill the watering pot

            hasWater = !hasWater;
            return true;
        }
        return false;
    }
    
    
}