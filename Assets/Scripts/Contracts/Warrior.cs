namespace Assets.Scripts.Contracts
{
    using System;

    [Serializable]
    public class Warrior
    {
        public string Name { get; set; }

        public int Cost { get; set; }

        public int Level { get; set; }

        public bool Unlocked { get; set; }

        public int BaseAtack { get; set; }

        public float BaseRange { get; set; }

        public WarriorEnum Type { get; set; }

        public int TransformationUnlocked { get; set; }
    }
}
