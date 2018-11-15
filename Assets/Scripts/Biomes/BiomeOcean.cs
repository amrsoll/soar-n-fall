using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeOcean : Biome
{
    public override void Generate(BiomeController biome)
    {
        for(int x = 0; x < Biome.XSize; x++)
        {
            for (int y = 0; y < Biome.YSize; y++)
            {
                for (int z = 0; z < Biome.ZSize; z++)
                {
                    biome.SetBlock(new Vector3Int(x, y, z), BlockType.Water);
                }
            }
        }
    }

    public override void Update(BiomeController biome)
    {

    }
}