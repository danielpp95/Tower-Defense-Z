namespace Assets.Scripts.Contracts
{

    using System.Collections.Generic;
    using UnityEngine;

    public static class Mappings
    {
        public static Contracts.Wave Map(this Scripts.LevelEditor.UI.WaveUI waveView)
        {
            return new Contracts.Wave
            {
                Enemy = waveView.Wave.Enemy,
                InitialCountdown = waveView.Wave.InitialCountdown,
                Level = waveView.Wave.Level,
                Quantity = waveView.Wave.Quantity
            };
        }

        public static List<Contracts.Wave> Map(this List<Scripts.LevelEditor.UI.WaveUI> waveViews)
        {
            var returnList = new List<Contracts.Wave>();

            foreach (var wave in waveViews)
            {
                returnList.Add(wave.Map());
            }

            return returnList;
        }

        public static (int, int) Map(this Vector2Int vector2Int)
        {
            return (vector2Int.x, vector2Int.y);
        }

        public static Vector2Int Map(this (int, int) value)
        {
            return new Vector2Int(value.Item1, value.Item2);
        }

        public static List<(int, int)> Map(this List<Vector2Int> list)
        {
            var returnList = new List<(int, int)>();

            foreach (var item in list)
            {
                returnList.Add(item.Map());
            }

            return returnList;
        }

        public static List<Vector2Int> Map(this List<(int, int)> list)
        {
            var returnList = new List<Vector2Int>();

            foreach (var item in list)
            {
                returnList.Add(item.Map());
            }

            return returnList;
        }

        public static TileMapSerializable Map(this TileMap tileMap)
        {
            return new TileMapSerializable
            {
                EndPoint = tileMap.EndPoint.Map(),
                FollowingPath = tileMap.FollowingPath.Map(),
                MapData = tileMap.MapData,
                SizeX = tileMap.SizeX,
                SizeY = tileMap.SizeY,
                StartPoint = tileMap.StartPoint.Map()
            };
        }

        public static TileMap Map(this TileMapSerializable tileMap)
        {
            return new TileMap
            {
                EndPoint = tileMap.EndPoint.Map(),
                FollowingPath = tileMap.FollowingPath.Map(),
                MapData = tileMap.MapData,
                SizeX = tileMap.SizeX,
                SizeY = tileMap.SizeY,
                StartPoint = tileMap.StartPoint.Map()
            };
        }

        public static Level Map(this LevelSerializable levelSerializable)
        {
            return new Level
            {
                InitialCash = levelSerializable.InitialCash,
                InitialCountDown = levelSerializable.InitialCountDown,
                Name = levelSerializable.Name,
                TileMap = levelSerializable.TileMap.Map(),
                Waves = levelSerializable.Waves
            };
        }

        public static LevelSerializable Map(this Level levelSerializable)
        {
            return new LevelSerializable
            {
                InitialCash = levelSerializable.InitialCash,
                InitialCountDown = levelSerializable.InitialCountDown,
                Name = levelSerializable.Name,
                TileMap = levelSerializable.TileMap.Map(),
                Waves = levelSerializable.Waves
            };
        }

        public static List<Level> Map(this List<LevelSerializable> list)
        {
            var returnList = new List<Level>();

            foreach (var item in list)
            {
                returnList.Add(item.Map());
            }

            return returnList;
        }

        public static List<LevelSerializable> Map(this List<Level> list)
        {
            var returnList = new List<LevelSerializable>();

            foreach (var item in list)
            {
                returnList.Add(item.Map());
            }

            return returnList;
        }
    }
}
