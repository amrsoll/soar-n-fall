using System;
using System.Text;
using UnityEngine;

public class BiomeController : MonoBehaviour
{
    public Transform BiomeBlockPrefab;
    public Biome BiomeType;

    BiomeManager _manager;
    public BiomeManager Manager
    {
        get { return _manager; }
        set { _manager = value; }
    }

	void Start () {
        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0:
                BiomeType = new BiomeForest(this);
                break;
            case 1:
                BiomeType = new BiomeDesert(this);
                break;
            case 2:
                BiomeType = new BiomeOcean(this);
                break;
        }
        BiomeType.Generate();
	}
	
	void Update () {
        BiomeType.Update();
	}

    bool CheckCoords(int x, int y, int z)
    {
        if (x < 0 || y < 0 || z < 0 || x >= Biome.XSize || y >= Biome.YSize || z >= Biome.ZSize) return false;
        return true;
    }

    public void SetBlock(int x, int y, int z, string Block)
    {
        if (!CheckCoords(x, y, z)) return;

        Transform existingBlock = GetBlock(x, y, z);
        if (existingBlock != null)
        {
            Destroy(existingBlock.gameObject);
        }

        Transform t = Instantiate(BiomeBlockPrefab, transform);
        t.name = String.Format("{0}|{1}|{2}", x, y, z);
        t.GetComponent<MeshRenderer>().material = Manager.GetBlockMaterial(Block);
        t.localPosition = new Vector3(x, y, z) * Biome.BlockSize;
    }

    public Transform GetBlock(int x, int y, int z)
    {
        if (!CheckCoords(x, y, z)) return null;

        return transform.Find(String.Format("{0}|{1}|{2}", x, y, z));
    }

    public void RemoveBlock(int x, int y, int z)
    {
        Transform block = GetBlock(x, y, z);
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
