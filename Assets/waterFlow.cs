using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterFlow : MonoBehaviour {
    // Use this for initialization

    void Start () {
        BiomeController biomeController = transform.parent.GetComponent<BiomeController>();
        if (biomeController == null){
            return;
        }
        Vector3Int myCordinates = GetComponent<BlockController>().biomeCoords;

        if (biomeController.GetBlock(new Vector3Int(myCordinates.x, myCordinates.y + 1, myCordinates.z)) == null){
            Debug.Log("boi");
        }
        //ym = biomeController.GetBlock(new Vector3Int(myCordinates.x, myCordinates.y - 1, myCordinates.z));
        //xp = biomeController.GetBlock(new Vector3Int(myCordinates.x + 1, myCordinates.y, myCordinates.z));
        //xm = biomeController.GetBlock(new Vector3Int(myCordinates.x - 1, myCordinates.y, myCordinates.z));





    }
    // Update is called once per frame
    void Update () {
        
	}
}
