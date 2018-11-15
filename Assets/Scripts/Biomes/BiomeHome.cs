using UnityEngine;

public class BiomeHome : Biome
{
    public override void Generate(BiomeController biome)
    {
        for(int x = 0; x < Biome.XSize; x++)
        {
            for (int y = 0; y < Biome.YSize-2; y++)
            {
                for (int z = 0; z < Biome.ZSize; z++)
                {
                    

                    biome.SetBlock(new Vector3Int(x, y, z), BlockType.Grass);
                }
            }
        }

        biome.SetBlock(new Vector3Int(2, 3, 2), BlockShape.Tent);
        biome.SetBlock(new Vector3Int(3, 3, 4), BlockShape.Tree);
    }


    public override void Update(BiomeController biome)
    {

    }
}