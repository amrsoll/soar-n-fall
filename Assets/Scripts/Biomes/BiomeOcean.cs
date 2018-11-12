using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeOcean : Biome
{
    public BiomeOcean(BiomeController Controller) : base(Controller)
    {

    }

    public override void Generate()
    {
        for(int x = 0; x < Biome.XSize; x++)
        {
            for (int y = 0; y < Biome.YSize; y++)
            {
                for (int z = 0; z < Biome.ZSize; z++)
                {
                    _controller.SetBlock(new Vector3Int(x, y, z), BlockType.Water);
                }
            }
        }
    }

    public override void Update()
    {

    }
}