using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterFlow : MonoBehaviour {
    // Use this for initialization
    BiomeController biomeController;
    Vector3Int myCordinates;
    BlockController blockController;
    List<GameObject> waterfalls;



    void Start () {
        biomeController = transform.parent.GetComponent<BiomeController>();
        if (biomeController == null){
            return;
        }
        blockController = GetComponent<BlockController>();
        myCordinates = blockController.biomeCoords;




        //ym = biomeController.GetBlock(new Vector3Int(myCordinates.x, myCordinates.y - 1, myCordinates.z));
        //xp = biomeController.GetBlock(new Vector3Int(myCordinates.x + 1, myCordinates.y, myCordinates.z));
        //xm = biomeController.GetBlock(new Vector3Int(myCordinates.x - 1, myCordinates.y, myCordinates.z));





    }
    // Update is called once per frame
    void Update () {
        
	}
    int HowManyBlocksDown(int xOffset, int zOffset)
    {
        if (biomeController.GetBlock(new Vector3Int(myCordinates.x + xOffset, myCordinates.y, myCordinates.z + zOffset)) == null)
        {
            if (biomeController.GetBlock(new Vector3Int(myCordinates.x + xOffset, myCordinates.y -1 , myCordinates.z + zOffset)) == null)
            {
                if (biomeController.GetBlock(new Vector3Int(myCordinates.x + xOffset, myCordinates.y - 2, myCordinates.z + zOffset)) == null)
                {
                    return -3;
                }
                return -2;
            }
            return -1;
        }
        return 0;
    }



    public BlockController SetBlock(Vector3Int pos, BlockShape shape, BlockType material)
    {
        BlockController block = SetBlock(pos, shape);
        if (block == null) return null;
        block.SetType(material);
        Material mat = Manager.GetBlockMaterial(material);
        MeshRenderer mr = block.GetComponent<MeshRenderer>();
        if (mr == null)
        {
            foreach (MeshRenderer mrchild in block.GetComponentsInChildren<MeshRenderer>())
            {
                if (mrchild != null) mrchild.material = mat;
            }
        }
        else
        {
            mr.material = mat;
        }
        return block;
    }



    public BlockController SetBlock(Vector3Int pos, BlockShape shape)
    {


        //CHECK if EXISTING
        if (existingBlock != null)
        {
            Destroy(existingBlock.gameObject);
        }
        //if X or Y
        Transform t = Instantiate(Manager.GetBlockShape(waterfall), transform);
        t.name = String.Format("{0}-{1}-{2}", pos.x, pos.y, pos.z);
        t.localPosition = (t.localPosition + pos) * Biome.BlockSize;
        t.localScale *= Biome.BlockSize;

        return t.gameObject.AddComponent<BlockController>().SetBiomeCoords(pos).SetShape(shape).SetType(BlockType.Missing);
    }

    public void WaterFallSpawn (int x, int y, int lengthOfFall){
        //CHECK if EXISTING
        if (existingBlock != null)
        {
            Destroy(existingBlock.gameObject);
        }
        //if X or Y change value of watefall

        //gameObject waterfall = Waterfall X;


        //shape 
        Transform t = Instantiate(Manager.GetBlockShape(waterfall), transform);
    }


}
