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
                    if (y == YSize - 2 && (z == 0 || z == ZSize - 1 || x == 0 || x == XSize - 1))
                    {
                        BlockShape shape = BlockShape.Slope;
                        // If we're on a corner
                        if ((z == 0 && x == 0) || (z == 0 && x == XSize - 1) || (z == ZSize - 1 && x == 0) || (z == ZSize - 1 && x == XSize - 1))
                            shape = BlockShape.SlopeAngle;

                        float yAngle = 0;
                        if (z == ZSize - 1) yAngle = 90;
                        if (x == XSize - 1) yAngle = 180;
                        if (z == 0 && x != 0) yAngle = 270;
                        _controller.SetBlock(new Vector3Int(x, y, z), shape, BlockType.Sand).Rotate(new Vector3(0, yAngle, 0));
                        continue;
                    }
                    if (y == YSize - 1)
                    {
                        if (z == 0 || z == ZSize - 1 || x == 0 || x == XSize - 1) continue;
                        if (z == 1 || z == ZSize - 2 || x == 1 || x == XSize - 2)
                        {
                            BlockShape shape = BlockShape.Slope;
                            // If we're on a corner
                            if ((z == 1 && x == 1) || (z == 1 && x == XSize - 2) || (z == ZSize - 2 && x == 1) || (z == ZSize - 2 && x == XSize - 2))
                                shape = BlockShape.SlopeAngle;

                            float yAngle = 0;
                            if (z == ZSize - 2) yAngle = 90;
                            if (x == XSize - 2) yAngle = 180;
                            if (z == 1 & x != 1) yAngle = 270;
                            _controller.SetBlock(new Vector3Int(x, y, z), shape, BlockType.Sand).Rotate(new Vector3(0, yAngle, 0));
                            continue;
                        }
                    }
                    _controller.SetBlock(new Vector3Int(x, y, z), BlockType.Sand);
                }
            }
        }
    }

    public override void Update()
    {

    }
}