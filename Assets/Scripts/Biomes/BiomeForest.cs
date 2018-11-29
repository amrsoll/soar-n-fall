using UnityEngine;

public class BiomeForest : Biome
{
    public override void Generate(BiomeController biome)
    {
        for(int x = 0; x < Biome.XSize; x++)
        {
            for (int y = 0; y < Biome.YSize-2; y++)
            {
                for (int z = 0; z < Biome.ZSize; z++)
                {
                    if (y == 0 && Random.value > 0.3f) continue;
                    if (y == 1 && Random.value > 0.6f) continue;

                    biome.SetBlock(new Vector3Int(x, y, z), BlockType.Grass);
                }
            }
        }
        biome.SetBlock(new Vector3Int(1, 3, 1), BlockShape.Tree);
        biome.SetBlock(new Vector3Int(1, 3, 3), BlockShape.Tree);
        biome.SetBlock(new Vector3Int(2, 3, 4), BlockShape.Tree);
        biome.SetBlock(new Vector3Int(5, 3, 5), BlockShape.Tree);
        biome.SetBlock(new Vector3Int(2, 3, 5), BlockShape.Tree);
        biome.SetBlock(new Vector3Int(1, 3, 0), BlockShape.Tree);
        biome.SetBlock(new Vector3Int(2, 3, 0), BlockShape.Tree);
    }

    public override void Update(BiomeController biome)
    {

    }
}