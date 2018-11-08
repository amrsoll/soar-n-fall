using System.Collections;
using System.Collections.Generic;
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
        switch (Random.Range(0, 3))
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
        if (x < 0 || y < 0 || z < 0 || x > Biome.XSize || y > Biome.YSize || z > Biome.ZSize) return false;
        return true;
    }

    public void SetBlock(int x, int y, int z, string Block)
    {
        if (!CheckCoords(x, y, z)) return;

        Transform t = GameObject.Instantiate(BiomeBlockPrefab, transform);
        t.name = new StringBuilder().Append(x).Append(y).Append(z).ToString();
        t.GetComponent<MeshRenderer>().material = Manager.GetBlockMaterial(Block);
        t.localPosition = new Vector3(x, y, z) * Biome.BlockSize;
    }

    public Transform GetBlock(int x, int y, int z)
    {
        if (!CheckCoords(x, y, z)) return null;

        return transform.Find(new StringBuilder().Append(x).Append(y).Append(z).ToString());
    }

    public void RemoveBlock(int x, int y, int z)
    {
        if (!CheckCoords(x, y, z)) return;

        Destroy(transform.Find(new StringBuilder().Append(x).Append(y).Append(z).ToString()));
    }
}
