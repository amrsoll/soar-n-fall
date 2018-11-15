public abstract class Biome {
    public const int XSize = 5;
    public const int YSize = 5;
    public const int ZSize = 5;
    public const int BlockSize = 2;

    public abstract void Generate(BiomeController biome);
    public abstract void Update(BiomeController biome);
}
