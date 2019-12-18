namespace Assets.Scripts.Contracts
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class LevelSerializable
    {
        public string Name { get; set; }

        public TileMapSerializable TileMap { get; set; }

        public List<Wave> Waves { get; set; }

        public int InitialCash { get; set; }

        public float InitialCountDown { get; set; }
    }
}