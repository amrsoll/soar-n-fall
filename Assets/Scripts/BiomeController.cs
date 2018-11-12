using System;
using System.Text;
using UnityEngine;

public class BiomeController : MonoBehaviour
{
    public Biome BiomeInstance;
    public BiomeManager Manager;

	void Start () {
        if (BiomeInstance == null)
        {
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    BiomeInstance = new BiomeForest(this);
                    break;
                case 1:
                    BiomeInstance = new BiomeDesert(this);
                    break;
                case 2:
                    BiomeInstance = new BiomeOcean(this);
                    break;
            }
        }
        BiomeInstance.Generate();
	}
	
	void Update () {
        BiomeInstance.Update();
	}

    bool CheckCoords(Vector3Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.z < 0 || pos.x >= Biome.XSize || pos.y >= Biome.YSize || pos.z >= Biome.ZSize) return false;
        return true;
    }

    public Transform SetBlock(Vector3Int pos, BlockType material)
    {
        return this.SetBlock(pos, BlockShape.Cube, material);
    }

    public Transform SetBlock(Vector3Int pos, BlockShape prefab)
    {
        if (!CheckCoords(pos)) return null;

        Transform existingBlock = GetBlock(pos);
        if (existingBlock != null)
        {
            Destroy(existingBlock.gameObject);
        }

        Transform t = Instantiate(Manager.GetBlockShape(prefab), transform);
        t.name = String.Format("{0}|{1}|{2}", pos.x, pos.y, pos.z);
        t.localPosition = pos * Biome.BlockSize;
        t.localScale = Biome.BlockSize * Vector3.one;
        return t;
    }

    public Transform SetBlock(Vector3Int pos, BlockShape shape, BlockType material)
    {
        Transform block = SetBlock(pos, shape);
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

    public Transform GetBlock(Vector3Int pos)
    {
        if (!CheckCoords(pos)) return null;

        return transform.Find(String.Format("{0}|{1}|{2}", pos.x, pos.y, pos.z));
    }

    public void RemoveBlock(Vector3Int pos)
    {
        Transform block = GetBlock(pos);
        if (block != null)
        {
            Destroy(block.gameObject);
        }
    }

    public void RemoveBlock(string xyz)
    {
        Transform block = transform.Find(xyz);
        if (block != null)
        {
            Destroy(block.gameObject);
        }
    }
}
