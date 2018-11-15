using UnityEngine;

public class BiomeForest : Biome
{
    public override void Generate(BiomeController biome)
    {
        for(int x = 0; x < Biome.XSize; x++)
        {
            for (int y = 0; y < Biome.YSize; y++)
            {
                for (int z = 0; z < Biome.ZSize; z++)
                {
                    if (y == 0 && Random.value > 0.3f) continue;
                    if (y == 1 && Random.value > 0.6f) continue;
                    if (y == 2 && Random.value > 0.8f) continue;

                    biome.SetBlock(new Vector3Int(x, y, z), BlockType.Grass);
                }
            }
        }
    }

    public override void Update(BiomeController biome)
    {

    }
}