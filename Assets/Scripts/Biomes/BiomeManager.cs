﻿using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BiomeManager : MonoBehaviour
{
    public Dictionary<BiomeType, Biome> Biomes;
    public Dictionary<BlockType, Material> Blocks;
    public Dictionary<BlockShape, Transform> Shapes;
    public BiomeController BiomePrefab;
    public bool isEditor = false;

    public static Vector3Int WorldToBiomePos(Vector3 worldPos)
    {
        worldPos /= Biome.BlockSize;
        Vector3Int biomePos = new Vector3Int(
            Mathf.FloorToInt((worldPos.x + Biome.BiomeSpacing / 2) / (Biome.XSize + Biome.BiomeSpacing - 1)),
            Mathf.FloorToInt((worldPos.y + Biome.BiomeSpacing / 2) / (Biome.YSize + Biome.BiomeSpacing - 1)),
            Mathf.FloorToInt((worldPos.z + Biome.BiomeSpacing / 2) / (Biome.ZSize + Biome.BiomeSpacing - 1))
        );
        return biomePos;
    }

    public static Vector3 BiomeToWorldPos(Vector3Int biomePos)
    {
        biomePos *= Biome.BlockSize;
        Vector3 worldPos = new Vector3(
            biomePos.x * (Biome.XSize + Biome.BiomeSpacing - 1),
            biomePos.y * (Biome.YSize + Biome.BiomeSpacing - 1),
            biomePos.z * (Biome.ZSize + Biome.BiomeSpacing - 1)
        );
        return worldPos;
    }
    
    public static Vector3 BlockToWorldPos(Vector3Int biomePos, Vector3Int blockPos)
    {
        blockPos *= Biome.BlockSize;
        return blockPos + BiomeToWorldPos(biomePos);
    }

    void RegisterBiomes()
    {
        Biomes = new Dictionary<BiomeType, Biome>();
        Biomes.Add(BiomeType.BaseEditor, new BiomeBaseEditor());
        Biomes.Add(BiomeType.Home, new BiomePremade("home"));
        Biomes.Add(BiomeType.Forest, new BiomeForest());
        Biomes.Add(BiomeType.Ocean, new BiomeOcean());
        Biomes.Add(BiomeType.Desert, new BiomeDesert());
        Biomes.Add(BiomeType.Volcano, new BiomePremade("volcano"));
    }

    void LoadBlocks()
    {
        Blocks = new Dictionary<BlockType, Material>();
        foreach (BlockType bt in Enum.GetValues(typeof(BlockType)))
        {
            Material m = Resources.Load<Material>("Blocks/" + bt);
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
            Transform t = Resources.Load<Transform>("Shapes/" + bs);
            if (t == null)
            {
                Shapes.TryGetValue(BlockShape.Cube, out t);
            }
            Shapes.Add(bs, t);
        }
    }

    void Awake () {
        RegisterBiomes();
        LoadBlocks();
        LoadShapes();
    }
	
	void Start () {
        if (!isEditor)
        {
            //CreateBiome(Vector3Int.zero, BiomeType.Home);
            //CreateBiome(new Vector3Int(0, 0, -1), BiomeType.Volcano);
            CreatePremadeBiome(new Vector3Int(0, 0, 0), "first");
            CreatePremadeBiome(new Vector3Int(0, 0, 1), "second");
            CreatePremadeBiome(new Vector3Int(0, 1, 2), "third");
            CreatePremadeBiome(new Vector3Int(0, 1, 3), "quickisland");
            CreatePremadeBiome(new Vector3Int(0, 0, 3), "volcano");
            CreatePremadeBiome(new Vector3Int(1, 0, 3), "last");

            for (int i = 0; i < 15; i++)
            {
                float val = UnityEngine.Random.value;
                if (val < 0.25f)
                {
                    CreatePremadeBiome(new Vector3Int(UnityEngine.Random.Range(5, 7), UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(5, 7)), "background");
                } else if (val < 0.5f)
                {
                    CreatePremadeBiome(new Vector3Int(UnityEngine.Random.Range(-7, -5), UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(-7, -5)), "background");
                } else if (val < 0.75f)
                {
                    CreatePremadeBiome(new Vector3Int(UnityEngine.Random.Range(-7, -5), UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(5, 7)), "background");
                } else
                {
                    CreatePremadeBiome(new Vector3Int(UnityEngine.Random.Range(5, 7), UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(-7, -5)), "background");
                }
            }

            /*CreatePremadeBiome(new Vector3Int(2, 0, 1), "biome0-0");
            CreatePremadeBiome(new Vector3Int(0, 0, 0), "biome0-2");
            CreatePremadeBiome(new Vector3Int(1, 0, 1), "biome1-1");
            CreatePremadeBiome(new Vector3Int(0, 0, 1), "biome1-2");
            CreatePremadeBiome(new Vector3Int(2, 0, 2), "biome2-2");*/
        }
    }

    public BiomeController CreatePremadeBiome(Vector3Int pos, string prefabName)
    {
        BiomeController b = Instantiate<BiomeController>(BiomePrefab, transform);
        b.name = String.Format("{0}-{1}-{2}", pos.x, pos.y, pos.z);

        b.Manager = this;
        Vector3 worldPos = Biome.BlockSize * (new Vector3(Biome.XSize, Biome.YSize, Biome.ZSize) + Biome.BiomeSpacing * Vector3.one);
        worldPos.Scale(pos);
        b.transform.localPosition = worldPos;

        b.Type = BiomeType.QuickPremade;
        b.BiomeInstance = new BiomePremade(prefabName);
        b.BiomeInstance.Generate(b);
        return b;
    }

    /** <summary>
     * Creates a new biome at the specified biome-space coordinates with the specified type
     * <para />
     * If biome is set to null then it picks a random biome except Premade
     */
    public BiomeController CreateBiome(Vector3Int pos, BiomeType? type = null, bool generate = true)
    {
        BiomeController b = Instantiate<BiomeController>(BiomePrefab, transform);
        b.name = String.Format("{0}-{1}-{2}", pos.x, pos.y, pos.z);

        b.Manager = this;
        Vector3 worldPos = Biome.BlockSize * (new Vector3(Biome.XSize, Biome.YSize, Biome.ZSize) + Biome.BiomeSpacing * Vector3.one);
        worldPos.Scale(pos);
        b.transform.localPosition = worldPos;

        Biome instance = null;
        if (type != null)
        {
            Biomes.TryGetValue(type.Value, out instance);
        } else
        {
            List<BiomeType> biomes = new List<BiomeType>(Biomes.Keys);
            type = biomes[UnityEngine.Random.Range(2, biomes.Count - 1)];
            Biomes.TryGetValue(type.Value, out instance);
        }
        b.Type = type.Value;
        b.BiomeInstance = instance;
        if (generate)
        {
            instance.Generate(b);
        }

        return b;
    }

    public BiomeController GetBiome(Vector3Int pos)
    {
        Transform t = transform.Find(String.Format("{0}-{1}-{2}", pos.x, pos.y, pos.z));
        if (t == null)
        {
            return null;
        }
        return t.GetComponent<BiomeController>();
    }

    public void RemoveBiome(Vector3Int pos)
    {
        Transform t = transform.Find(String.Format("{0}-{1}-{2}", pos.x, pos.y, pos.z));
        if (t != null)
        {
            Destroy(t.gameObject);
        }
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

    public void SaveBiomes(string worldName)
    {
        string path = Path.Combine(Application.persistentDataPath, worldName + ".map");
        using (
            BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create))
        )
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<BiomeController>().Save(writer);
            }
        }
    }

    public void LoadBiomes(string worldName)
    {
        string path = Path.Combine(Application.persistentDataPath, worldName + ".map");
        using (
            BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open))
        )
        {
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                BiomeController b = Instantiate<BiomeController>(BiomePrefab, transform);
                b.Manager = this;
                b.name = reader.ReadString();
                b.Type = (BiomeType)reader.ReadByte();
                Biomes.TryGetValue(b.Type, out b.BiomeInstance);
                b.transform.localPosition = reader.ReadVector3();
                b.Read(reader);
            }
        }
    }
}
