using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BlockController : MonoBehaviour {
    public BlockShape shape;
    public BlockType type;
    public Vector3Int biomeCoords;

    public void Save(BinaryWriter writer)
    {
        writer.Write(name);
        writer.Write((byte)shape);
        writer.Write((byte)type);
        writer.Write(biomeCoords);
        writer.Write(transform.localRotation);
    }

    public BlockController SetBiomeCoords(Vector3Int pos)
    {
        this.biomeCoords = pos;
        return this;
    }

    public BlockController SetShape(BlockShape shape)
    {
        this.shape = shape;
        return this;
    }

    public BlockController SetType(BlockType type)
    {
        this.type = type;
        return this;
    }

    public BlockController SetRotation(Vector3 eulerAngles)
    {
        transform.localEulerAngles = eulerAngles;
        return this;
    }

    public BlockController Rotate(Vector3 eulerAngles)
    {
        transform.Rotate(eulerAngles);
        return this;
    }
}
