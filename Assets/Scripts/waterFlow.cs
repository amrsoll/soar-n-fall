using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterFlow : MonoBehaviour {
    BiomeController biomeController;
    Vector3Int myCordinates;
    Transform waterfall;

    void Start () {
        biomeController = transform.parent.GetComponent<BiomeController>();
        if (biomeController == null) return;
        myCordinates = GetComponent<BlockController>().biomeCoords; 
    }
    
    void Update () {
        if (biomeController == null) return;

        int blocksDown = HowManyBlocksDown(1, 0);
        if (blocksDown > 0)
        {
            if (waterfall == null)
            {
                waterfall = Instantiate(biomeController.Manager.GetBlockShape(BlockShape.Waterfall), transform);
                waterfall.localPosition = new Vector3(1, 0, 0);
            }
            Renderer re = waterfall.GetComponentInChildren<Renderer>();
            re.material.SetFloat("_FallPlacement", transform.position.x + Biome.BlockSize + 0.2f);
            re.material.SetFloat("_FallLength", blocksDown * 0.6f);
        } else
        {
            if (waterfall != null)
            {
                Destroy(waterfall.gameObject);
            }
        }
    }

    int HowManyBlocksDown(int xOffset, int zOffset)
    {
        int counter = 0;
        Vector3Int pos = new Vector3Int(myCordinates.x + xOffset, myCordinates.y, myCordinates.z + zOffset);
        while (pos.y >= 0)
        {
            if (biomeController.GetBlock(pos) == null)
            {
                counter++;
                pos.y--;
            } else
            {
                return counter;
            }
        }
        return counter + 5;
    }
}
