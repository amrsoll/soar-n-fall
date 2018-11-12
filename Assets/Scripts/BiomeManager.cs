using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BiomeManager : MonoBehaviour
{
    public Dictionary<BlockType, Material> Blocks;
    public Dictionary<BlockShape, Transform> Shapes;
    public BiomeController BiomePrefab;

    BiomeController _currentBiome;

    void LoadBlocks()
    {
        Blocks = new Dictionary<BlockType, Material>();
        foreach (BlockType bt in Enum.GetValues(typeof(BlockType)))
        {
            Material m = Resources.Load<Material>("Materials/Blocks/" + bt);
            if (m == null)
            {
                Blocks.TryGetValue(BlockType.Missing, out m);
            }
            Blocks.Add(bt, m);
        }
    }

    void LoadShapes()
    {
        Shapes = new Dictionary<BlockShape, Transform>();
        foreach (BlockShape bs in Enum.GetValues(typeof(BlockShape)))
        {
            Transform t = Resources.Load<Transform>("Materials/Shapes/" + bs);
            if (t == null)
            {
                Shapes.TryGetValue(BlockShape.Cube, out t);
            }
            Shapes.Add(bs, t);
        }
    }

    void Start () {
        LoadBlocks();
        LoadShapes();

        CreateBiome(Vector3Int.zero, typeof(BiomeDesert));
    }
	
	void Update () {
		
	}

    /** <summary>
     * Creates a new biome at the specified biome-space coordinates
     * <para />
     * TODO: Should be able to choose the biome type</summary>
     */
    public BiomeController CreateBiome(Vector3Int pos, Type biome = null)
    {
        BiomeController b = Instantiate<BiomeController>(BiomePrefab, transform);
        b.name = String.Format("{0}|{1}|{2}", pos.x, pos.y, pos.z);
        if (biome != null)
        {
            b.BiomeInstance = (Biome)Activator.CreateInstance(biome, new object[] { b });
        }
        b.Manager = this;
        Vector3 worldPos = new Vector3(5 * Biome.XSize, 5 * Biome.YSize, 5 * Biome.ZSize);
        worldPos.Scale(pos);
        b.transform.localPosition = worldPos;

        return b;
    }

    public BiomeController GetBiome(Vector3Int pos)
    {
        Transform t = transform.Find(String.Format("{0}|{1}|{2}", pos.x, pos.y, pos.z));
        if (t == null)
        {
            return null;
        }
        return t.GetComponent<BiomeController>();
    }

    public Material GetBlockMaterial(BlockType type)
    {
        Material m;
        Blocks.TryGetValue(type, out m);
        if (m == null)
        {
            Blocks.TryGetValue(BlockType.Missing, out m);
        }
        return m;
    }

    public Transform GetBlockShape(BlockShape shape)
    {
        Transform t;
        Shapes.TryGetValue(shape, out t);
        if (t == null)
        {
            Shapes.TryGetValue(BlockShape.Cube, out t);
        }
        return t;
    }

    public void SaveWorld(string worldName)
    {
        string path = Path.Combine(Application.persistentDataPath, worldName + ".map");
        using (
            BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create))
        )
        {
            foreach (Transform child in transform)
            {
                //child.GetComponent<BiomeController>().Save(writer);
            }
        }
    }
}
