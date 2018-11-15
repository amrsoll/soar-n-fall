using System.IO;
using UnityEngine;

public static class SaveUtils
{
    public static void Write(this BinaryWriter writer, Vector3 vector)
    {
        writer.Write(vector.x);
        writer.Write(vector.y);
        writer.Write(vector.z);
    }

    public static Vector3 ReadVector3(this BinaryReader reader)
    {
        return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
    }

    public static void Write(this BinaryWriter writer, Vector3Int vector)
    {
        writer.Write(vector.x);
        writer.Write(vector.y);
        writer.Write(vector.z);
    }

    public static Vector3Int ReadVector3Int(this BinaryReader reader)
    {
        return new Vector3Int(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
    }

    public static void Write(this BinaryWriter writer, Quaternion quat)
    {
        writer.Write(quat.x);
        writer.Write(quat.y);
        writer.Write(quat.z);
        writer.Write(quat.w);
    }

    public static Quaternion ReadQuaternion(this BinaryReader reader)
    {
        return new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
    }
}
