namespace Assets.Scripts.Contracts
{
    using System.Collections.Generic;
    using UnityEngine;

    public class TileMap
    {
        public Vector2Int StartPoint { get; set; }

        public Vector2Int EndPoint { get; set; }

        public int SizeX { get; set; }

        public int SizeY { get; set; }

        public int[,] MapData { get; set; }

        public List<Vector2Int> FollowingPath { get; set; }

        public TileMap()
        {
        }

        public TileMap(int sizeX, int sizeY, Vector2Int startPoint, Vector2Int endPoint)
        {
            this.InitializeTileMap(sizeX, sizeY, startPoint, endPoint);
        }

        public TileMap(int sizeX, int sizeY)
        {
            this.InitializeTileMap(sizeX, sizeY, Vector2Int.zero, new Vector2Int(sizeX - 1, sizeY - 1));
        }

        private void InitializeTileMap(int sizeX, int sizeY, Vector2Int startPoint, Vector2Int endPoint)
        {
            this.SizeX = sizeX;
            this.SizeY = sizeY;
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.FollowingPath = new List<Vector2Int> { startPoint };

            this.MapData = new int[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    this.MapData[x, y] = (int)TileEnum.Ground;
                }
            }

            this.MapData[startPoint.x, startPoint.y] = (int)TileEnum.Spawn;
            this.MapData[endPoint.x, endPoint.y] = (int)TileEnum.End;
        }

        public int GetTileAt(int x, int y)
        {
            return this.MapData[x, y];
        }
    }
}
