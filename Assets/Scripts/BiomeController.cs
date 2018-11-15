using System;
using System.IO;
using System.Text;
using UnityEngine;

public class BiomeController : MonoBehaviour
{
    public BiomeType Type;
    public Biome BiomeInstance;
    public BiomeManager Manager;

    int blockCount = 0;

	void Start () {}

	void Update () {
        BiomeInstance.Update(this);
	}

    bool CheckCoords(Vector3Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.z < 0 || pos.x >= Biome.XSize || pos.y >= Biome.YSize || pos.z >= Biome.ZSize) return false;
        return true;
    }

    public BlockController SetBlock(Vector3Int pos, BlockType material)
    {
        return this.SetBlock(pos, BlockShape.Cube, material);
    }

    public BlockController SetBlock(Vector3Int pos, BlockShape shape)
    {
        if (!CheckCoords(pos)) return null;
        blockCount++;

        BlockController existingBlock = GetBlock(pos);
        if (existingBlock != null)
        {
            Destroy(existingBlock.gameObject);
        }

        Transform t = Instantiate(Manager.GetBlockShape(shape), transform);
        t.name = String.Format("{0}|{1}|{2}", pos.x, pos.y, pos.z);
        t.localPosition = (t.localPosition + pos) * Biome.BlockSize;
        t.localScale *= Biome.BlockSize;

        return t.gameObject.AddComponent<BlockController>().SetBiomeCoords(pos).SetShape(shape).SetType(BlockType.Missing);
    }

    public BlockController SetBlock(Vector3Int pos, BlockShape shape, BlockType material)
    {
        BlockController block = SetBlock(pos, shape).SetType(material);
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

    public BlockController GetBlock(Vector3Int pos)
    {
        if (!CheckCoords(pos)) return null;

        Transform t = transform.Find(String.Format("{0}|{1}|{2}", pos.x, pos.y, pos.z));
        if (t != null)
            return t.GetComponent<BlockController>();
        return null;
    }

    public void RemoveBlock(Vector3Int pos)
    {
        BlockController block = GetBlock(pos);
        if (block != null)
        {
            Destroy(block.gameObject);
            blockCount--;
        }
    }

    public void RemoveBlock(string xyz)
    {
        Transform block = transform.Find(xyz);
        if (block != null)
        {
            Destroy(block.gameObject);
            blockCount--;
        }
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(name);
        writer.Write((byte)Type);
        writer.Write(transform.localPosition);
        writer.Write(blockCount);
        for (int x = 0; x < Biome.XSize; x++)
        {
            for (int y = 0; y < Biome.YSize; y++)
            {
                for (int z = 0; z < Biome.ZSize; z++)
                {
                    BlockController block = GetBlock(new Vector3Int(x, y, z));
                    if (block != null) block.Save(writer);
                }
            }
        }
    }

    public void Read(BinaryReader reader)
    {
        int childCount = reader.ReadInt32();
        for (int i = 0; i < childCount; i++)
        {
            string name = reader.ReadString();
            BlockShape shape = (BlockShape)reader.ReadByte();
            BlockType material = (BlockType)reader.ReadByte();
            BlockController block;
            if (material == BlockType.Missing)
            {
                block = SetBlock(reader.ReadVector3Int(), shape);
            } else
            {
                block = SetBlock(reader.ReadVector3Int(), shape, material);
            }

            block.name = name;
            block.transform.localRotation = reader.ReadQuaternion();
        }
    }
}
