namespace Assets.Scripts.Contracts
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TileMap
    {
        public (int, int) StartPoint { get; set; }

        public (int, int) EndPoint { get; set; }

        public int SizeX { get; set; }

        public int SizeY { get; set; }


        public int[,] MapData { get; set; }

        public List<(int, int)> FollowingPath { get; set; }

        public TileMap(int sizeX, int sizeY, (int, int) startPoint, (int, int) endPoint)
        {
            this.InitializeTileMap(sizeX, sizeY, startPoint, endPoint);
        }

        public TileMap(int sizeX, int sizeY)
        {
            this.InitializeTileMap(sizeX, sizeY, (0, 0), (sizeX - 1, sizeY - 1));
        }

        private void InitializeTileMap(int sizeX, int sizeY, (int, int) startPoint, (int, int) endPoint)
        {
            this.SizeX = sizeX;
            this.SizeY = sizeY;
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.FollowingPath = new List<(int, int)> { startPoint };

            this.MapData = new int[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    this.MapData[x, y] = (int)TileEnum.Ground;
                }
            }

            this.MapData[startPoint.Item1, startPoint.Item2] = (int)TileEnum.Spawn;
            this.MapData[endPoint.Item1, endPoint.Item2] = (int)TileEnum.End;
        }

        public int GetTileAt(int x, int y)
        {
            return this.MapData[x, y];
        }
    }
}
