namespace Assets.Scripts.Engine
{
    using System.Collections.Generic;
    using Assets.Scripts.Contracts;
    using UnityEngine;

    public class GenerateWarriorsData : MonoBehaviour
    {
        private List<Warrior> warriors;

        private void Start()
        {
            warriors = new List<Warrior>();

            this.AddWarrior("Goku Base", WarriorEnum.GokuBase, 5, 15f, 3, 1, true, 0);
            this.AddWarrior("Goku SSJ1", WarriorEnum.GokuSSJ1, 10, 15f, 7, 1, true, 0);
            this.AddWarrior("Goku SSJ2", WarriorEnum.GokuSSJ2, 15, 15f, 13, 1, true, 0);
            this.AddWarrior("Goku SSJ3", WarriorEnum.GokuSSJ3, 20, 20f, 20, 1, true, 0);

            SaveEngine.SaveWarriors(this.warriors);
        }

        private void AddWarrior(
            string name,
            WarriorEnum type,
            int cost,
            float range,
            int attack,
            int level,
            bool unlocked,
            int transformationUnlocked)
        {
            var w = new Warrior
            {
                Name = name,
                Type = type,
                Cost = cost,
                BaseRange = range,
                BaseAtack = attack,
                Level = level,
                Unlocked = unlocked,
                TransformationUnlocked = transformationUnlocked
            };

            warriors.Add(w);
        }
    }
}
