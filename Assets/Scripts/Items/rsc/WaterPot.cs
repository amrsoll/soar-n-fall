using UnityEngine;
// TODO it's actually watering pot
public class WaterPot : ItemController
{
    Animator anim;
    Collider m_Collider;

    /*public void Start()
    {
        hasWater = false;
        hasWater = true;
    }*/

    public override bool InteractWith(ItemController item)
    {
        if (item.type == Item.Flower)
        {
            anim = item.GetComponent<Animator>();
            GameObject.Find("SoundManager").GetComponent<NarrationTrigger>().PlayClip("on-flowers-watered");
            anim.SetTrigger("Grow");
            m_Collider = item.GetComponent<Collider>();
            m_Collider.enabled = true;
        }
        return false;
    }
    
    /*public override bool InteractWith(BlockController block)
    {
        if (block.type == BlockType.Water)
        {
            // fill the watering pot

            hasWater = !hasWater;
            return true;
        }
        return false;
    }*/
    
    
}