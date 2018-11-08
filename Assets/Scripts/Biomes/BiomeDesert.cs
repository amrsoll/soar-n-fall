using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeDesert : Biome
{
    public BiomeDesert(BiomeController Controller) : base(Controller)
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
                    if (y == YSize - 2 && (z == 0 || z == ZSize - 1 || x == 0 || x == XSize - 1)) continue;
                    if (y == YSize - 1 && (z <= 1 || z >= ZSize - 2 || x <= 1 || x >= XSize - 2)) continue;
                    _controller.SetBlock(x, y, z, "Sand");
                }
            }
        }
    }

    public override void Update()
    {

    }
}