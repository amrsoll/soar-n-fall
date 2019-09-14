using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterFlowMulti : MonoBehaviour {
    BiomeController biomeController;
    Vector3Int myCordinates;
    Transform waterfallXP;
    Transform waterfallZP;
    Transform waterfallXN;
    Transform waterfallZN;
    Transform waterfall;

    void Start () {
        biomeController = transform.parent.GetComponent<BiomeController>();
        if (biomeController == null) return;
        myCordinates = GetComponent<BlockController>().biomeCoords; 
    }
    
    void Update () {
        if (biomeController == null) return;


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
    void WaterFallUpdate(int xOffset, int zOffset)
    {
        if (zOffset*xOffset != 0){
            return;
        }
        //Transform waterfall = GetRightWaterfall(xOffset, zOffset);
        int blocksDown = HowManyBlocksDown(xOffset, zOffset);

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
            re.material.SetFloat("_isX", (float)(xOffset*xOffset));
            re.material.SetFloat("_isX", (float)(xOffset + zOffset));
        }
        else
        {
            if (waterfall != null)
            {
                Destroy(waterfall.gameObject);
            }
        }

       



    }
    Transform GetRightWaterfall(int xOffset, int zOffset)
    {
        if (xOffset == 1)
        {
            return waterfallXP;
        }
        else if ((xOffset == -1))
        {
            return waterfallXN;
        }
        if (xOffset == 1)
        {
            return waterfallZP;
        }
        else if (xOffset == -1)
        {
            return waterfallZN;
        }
        return null;
    }
}
