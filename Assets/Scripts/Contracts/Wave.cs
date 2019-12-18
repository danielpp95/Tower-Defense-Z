namespace Assets.Scripts.Contracts
{
    using System;

    [Serializable]
    public class Wave
    {
        public EnemyEnum Enemy { get; set; }

        public int Level { get; set; }

        public int Quantity { get; set; }

        public float InitialCountdown { get; set; }
    }
}
