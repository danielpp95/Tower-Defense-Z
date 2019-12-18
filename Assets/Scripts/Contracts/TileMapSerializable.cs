namespace Assets.Scripts.Contracts
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TileMapSerializable
    {
        public (int, int) StartPoint { get; set; }

        public (int, int) EndPoint { get; set; }

        public int SizeX { get; set; }

        public int SizeY { get; set; }

        public int[,] MapData { get; set; }

        public List<(int, int)> FollowingPath { get; set; }
    }
}
