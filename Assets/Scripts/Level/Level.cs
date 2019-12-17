using System.Collections.Generic;

public class Level
{
    public string Name { get; set; }

    public TileMap TileMap { get; set; }

    public List<Wave> Waves { get; set; }

    public int InitialCash { get; set; }

    public float TimeToFirtsWave { get; set; }
}
