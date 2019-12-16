using System.Collections.Generic;

public class TileMap
{
    public (int, int) startPoint;
    public (int, int) endPoint;
    public int sizeX { get; private set; }
    public int sizeY { get; private set; }

    public int[,] mapData;

    public List<(int, int)> FollowingPath;

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
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.FollowingPath = new List<(int, int)>();

        mapData = new int[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                mapData[x, y] = (int)TileEnum.Ground;
            }
        }

        mapData[startPoint.Item1, startPoint.Item2] = (int)TileEnum.Spawn;
        mapData[endPoint.Item1, endPoint.Item2] = (int)TileEnum.End;
    }

    public int GetTileAt(int x, int y)
    {
        return mapData[x, y];
    }
}
