using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : ItemController
{
    EventSoundTrigger SoundEvent;
    
    void Start()
    {
        SoundEvent = GameObject.Find("SoundManager").GetComponent<EventSoundTrigger>();
    }
    // Update is called once per frame
    void Update () {
        
    }

    override public bool InteractWith(ItemController item)
    {
        Debug.Log(item.name.ToString());
        if (item.type == Item.Fire)
        {
            ItemController nails = Manager.SpawnObject(Item.Nails, item.transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity);

            Destroy(this.gameObject);
            //SoundEvent.PlayClip("AxeSwing");


            return true;
        }
        return false;
    }
}
