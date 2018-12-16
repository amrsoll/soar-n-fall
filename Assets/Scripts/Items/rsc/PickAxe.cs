using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : ItemController
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
        if (item.type == Item.Stone)
        {
            ItemController metal = Manager.SpawnObject(Item.Metal, item.transform.position + new Vector3(0.1f, 0.5f, 0f), Quaternion.identity);


            SoundEvent.PlayClip("pick-axe-sound");


            return true;
        }
        return false;
    }
}
