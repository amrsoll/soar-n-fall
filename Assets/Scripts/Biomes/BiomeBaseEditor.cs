using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeBaseEditor : Biome
{
    public override void Generate(BiomeController biome)
    {
        biome.SetBlock(new Vector3Int(0, 0, 0), BlockType.Dirt);
    }

    public override void Update(BiomeController biome)
    {
        if (biome.GetBlockCount() == 0)
        {
            biome.SetBlock(new Vector3Int(0, 0, 0), BlockType.Dirt);
        }
    }
}