namespace Assets.Scripts.LevelEditor.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.Contracts;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class WaveView : MonoBehaviour
    {
        public Dropdown EnemyDropdown;

        public Wave Wave { get; private set; }

        private void Start()
        {
            InitializeEnemyDropdown();

            this.Wave = new Wave
            {
                Level = 0,
                Quantity = 0,
                InitialCountdown = 0,
            };
        }

        public void SetLevel(string text)
        {
            this.Wave.Level = int.Parse(text);
        }

        public void SetQuantity(string text)
        {
            this.Wave.Quantity = int.Parse(text);
        }

        public void SetInitialCountDown(string text)
        {
            this.Wave.InitialCountdown = float.Parse(text);
        }

        public void SetEnemy(int enemy)
        {
            this.Wave.Enemy = (EnemyEnum)enemy;
        }

        private void InitializeEnemyDropdown()
        {
            this.EnemyDropdown.ClearOptions();


            var optionData = new List<Dropdown.OptionData>();

            optionData.Add(new Dropdown.OptionData
            {
                text = "-- Character"
            });

            var enumList = Enum.GetNames(typeof(EnemyEnum))
            .Where(x => x != "None");
            //.ToList()

            foreach (var enemy in enumList)
            {
                optionData.Add(new Dropdown.OptionData
                {
                    text = enemy.ToString()
                });
            }

            this.EnemyDropdown.AddOptions(optionData);
        }
    }
}