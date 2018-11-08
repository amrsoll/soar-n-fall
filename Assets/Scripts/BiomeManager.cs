using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class BiomeManager : MonoBehaviour
{
    public Dictionary<string, Material> Blocks;
    public BiomeController BiomePrefab;

    BiomeController _currentBiome;

    void LoadBlocks()
    {
        Blocks = new Dictionary<string, Material>();
        foreach (string bt in Enum.GetNames(typeof(BlockType)))
        {
            Material m = Resources.Load<Material>("Materials/Blocks/" + bt);
            if (m == null)
            {
                Blocks.TryGetValue("Missing", out m);
            }
            Blocks.Add(bt, m);
        }
    }

	void Start () {
        LoadBlocks();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    CreateBiome(x, y, z);
                }
            }
        }
	}
	
	void Update () {
		
	}

    public BiomeController CreateBiome(int x, int y, int z)
    {
        BiomeController b = Instantiate<BiomeController>(BiomePrefab, transform);
        b.name = String.Format("{0}|{1}|{2}", x, y, z);
        b.Manager = this;
        b.transform.localPosition = new Vector3(5 * x * Biome.XSize, 5 * y * Biome.YSize, 5 * z * Biome.ZSize);

        return b;
    }

    public BiomeController GetBiome(int x, int y, int z)
    {
        Transform t = transform.Find(String.Format("{0}|{1}|{2}", x, y, z));
        if (t == null)
        {
            return null;
        }
        return t.GetComponent<BiomeController>();
    }

    public Material GetBlockMaterial(string name)
    {
        Material m;
        Blocks.TryGetValue(name, out m);
        if (m == null)
        {
            Blocks.TryGetValue("Missing", out m);
        }
        return m;
    }
}
