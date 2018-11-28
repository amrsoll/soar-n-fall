using System;
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

    public static Vector3Int WorldToBiomePos(Vector3 worldPos)
    {
        worldPos /= Biome.BlockSize;
        Vector3Int biomePos = new Vector3Int(
            (int)Math.Floor((worldPos.x + Biome.BiomeSpacing / 2) / (Biome.XSize + Biome.BiomeSpacing - 1)),
            (int)Math.Floor((worldPos.y + Biome.BiomeSpacing / 2) / (Biome.YSize + Biome.BiomeSpacing - 1)),
            (int)Math.Floor((worldPos.z + Biome.BiomeSpacing / 2) / (Biome.ZSize + Biome.BiomeSpacing - 1))
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

    void RegisterBiomes()
    {
        Biomes = new Dictionary<BiomeType, Biome>();
        Biomes.Add(BiomeType.BaseEditor, new BiomeBaseEditor());
        Biomes.Add(BiomeType.Home, new BiomeHome());
        Biomes.Add(BiomeType.Forest, new BiomeForest());
        Biomes.Add(BiomeType.Ocean, new BiomeOcean());
        Biomes.Add(BiomeType.Desert, new BiomeDesert());
    }

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
        RegisterBiomes();
        LoadBlocks();
        LoadShapes();

        /*CreateBiome(Vector3Int.zero, BiomeType.Home);
        CreateBiome(new Vector3Int(0, 0, 1), BiomeType.Forest);
        CreateBiome(new Vector3Int(0, 1, 1), BiomeType.Forest);
        CreateBiome(new Vector3Int(1, 0, 1), BiomeType.Forest);
        CreateBiome(new Vector3Int(-1, 0, 1), BiomeType.Forest);*/
    }
	
	void Update () {
		
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
