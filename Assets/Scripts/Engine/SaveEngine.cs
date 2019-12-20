﻿namespace Assets.Scripts.Engine
{
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Assets.Scripts.Contracts;
    using UnityEngine;

    public static class SaveEngine
    {
        private static string LevelsFileName = "Levels";
        private static string WarriorsFileName = "Warriors";

        public static List<Level> LoadLevels()
        {
            return Load<List<LevelSerializable>>(LevelsFileName).Map();
        }

        public static void SaveNewLevel(Level level)
        {
            var levels = LoadLevels();

            levels.Add(level);

            SaveData(levels, LevelsFileName);
        }

        public static void SaveLevels(List<Level> levels)
        {
            SaveData(levels.Map(), LevelsFileName);
        }

        public static void SaveWarriors(List<Warrior> warriors)
        {
            SaveData(warriors, WarriorsFileName);
        }

        public static List<Warrior> LoadWarriors()
        {
            return Load<List<Warrior>>(WarriorsFileName);
        }

        private static string SavePath(string fileName)
        {
            return Application.persistentDataPath + $"/{fileName}.save";
        }

        private static T Load<T>(string fileName) where T : new()
        {
            if (File.Exists(SavePath(fileName)))
            {
                var bf = new BinaryFormatter();
                var file = File.Open(SavePath(fileName), FileMode.Open);

                var dataObject = (T)bf.Deserialize(file);

                file.Close();

                return dataObject;
            }

            return new T();
        }

        private static void SaveData<T>(T saveData, string fileName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(SavePath(fileName));
            bf.Serialize(file, saveData);
            file.Close();

            Debug.Log("Data Saved");
        }
    }
}