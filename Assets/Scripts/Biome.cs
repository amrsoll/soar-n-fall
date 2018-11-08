public abstract class Biome {
    public static int XSize = 5;
    public static int YSize = 5;
    public static int ZSize = 5;
    public static int BlockSize = 1;

    protected BiomeController _controller;
    public Biome(BiomeController Controller)
    {
        this._controller = Controller;
    }

    public abstract void Generate();
    public abstract void Update();
}
